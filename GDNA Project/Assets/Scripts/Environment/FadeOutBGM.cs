using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutBGM : MonoBehaviour
{
    public void FadeOut()
    {
        StartCoroutine(StartFade());
    }

    IEnumerator StartFade()
    {
        AudioSource audio = gameObject.GetComponent<AudioSource>();

        float start = audio.volume;
        float currentTime = 0;
        float duration = 0.8f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audio.volume = Mathf.Lerp(start, 0f, currentTime / duration);
            yield return null;
        }
        yield break;
    }

}
