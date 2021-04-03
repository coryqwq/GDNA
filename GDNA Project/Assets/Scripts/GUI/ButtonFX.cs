using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFX : MonoBehaviour
{
    public Animator buttonAnim;

    private AudioSource buttonAudio;
    public AudioClip[] buttonAudioClips;
    public GameObject player;
    //assign AudioSource component
    private void Start()
    {
        buttonAudio = GetComponent<AudioSource>();
    }

    //assign and play hover audio clip
    public void HoverSFX()
    {
        buttonAudio.PlayOneShot(buttonAudioClips[0]);
    }

    //animate button for mouse enter
    public void HoverOn()
    {
        buttonAnim.SetBool("HoverOn", true);
    }

    //animate button for mouse exit
    public void HoverOff()
    {
        buttonAnim.SetBool("HoverOn", false);
    }

    //animate button for button selected
    public void DifficultySelected()
    {
        buttonAnim.SetBool("DifficultySelected", true);
    }

    //animate button for button selected
    public void DifficultyDeselected()
    {
        buttonAnim.SetBool("DifficultySelected", false);
    }

    //assign and play click audio clip
    public void ClickSFX()
    {
        buttonAudio.PlayOneShot(buttonAudioClips[1]);
    }

    //assign and play esc audio clip
    public void BackSFX()
    {
        buttonAudio.PlayOneShot(buttonAudioClips[2]);
    }
}
