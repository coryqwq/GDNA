using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool paused = false;

    public Canvas pauseMenu;
    public GameObject mainCamera;
    public AudioClip[] clip;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                StartCoroutine(DelayPause());
            }
            else if(paused)
            {
                StartCoroutine(DelayUnpause());
            }
        }
    }
    
    IEnumerator DelayUnpause()
    {
        mainCamera.GetComponent<AudioSource>().UnPause();
        GetComponent<AudioSource>().PlayOneShot(clip[0]);
        pauseMenu.GetComponent<Animator>().SetBool("Pause", false);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.1f);
        pauseMenu.gameObject.SetActive(false);
        paused = false;
    }

    IEnumerator DelayPause()
    {
        mainCamera.GetComponent<AudioSource>().Pause();
        GetComponent<AudioSource>().PlayOneShot(clip[1]);
        pauseMenu.gameObject.SetActive(true);
        pauseMenu.GetComponent<Animator>().SetBool("Pause", true);
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0f;
        paused = true;
    }
}
