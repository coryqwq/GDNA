using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoveDamagePopup : MonoBehaviour
{
    public float elapsed = 0.0f;
    public float duration = 1.0f;
    public float t = 0;
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime);

        elapsed += Time.deltaTime;
        if(elapsed > duration)
        {
            t += Time.deltaTime; 
            GetComponent<TextMeshPro>().color = new Color(255,255,255,(byte)(Mathf.Lerp(255, 0, t/duration)));
        }

        if (GetComponent<TextMeshPro>().color.a == 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DelayedAlphaLerp()
    {
        yield return new WaitForSeconds(duration);

    }
}
