using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnBounds : MonoBehaviour
{
    public string tagName;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagName) || other.CompareTag("Bounds"))
        {
            Destroy(gameObject);
        }
    }
}
