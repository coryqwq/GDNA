using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterElapsedTime : MonoBehaviour
{

    private float elapsed = 0;
    public float duration = 0.05f;

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed > duration)
        {
            Destroy(gameObject); 
        }
    }
}
