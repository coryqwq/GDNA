using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutBGM : MonoBehaviour
{
    AudioSource BGM;
    float currentTime = 0;
    public float duration = 0.8f;
    public bool fade = false;
    public void FadeOut()
    {
        BGM = gameObject.GetComponent<AudioSource>();
        fade = true;
    }

    public void Update()
    {
        if (fade == true)
        {
            if (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                BGM.volume = Mathf.Lerp(1.0f, 0.0f, currentTime / duration);
            }
        }
    }
}
