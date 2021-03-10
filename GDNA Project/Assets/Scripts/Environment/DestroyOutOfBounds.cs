using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    //declaring and initializing absolute ranges
    public float xRange = 8.9f;
    public float yRange = 5;

    // Update is called once per frame
    void Update()
    {
        //check if position of projectile exceeds bounds on y axis
        if (transform.position.x > xRange - 1.33f || transform.position.x < -xRange + 6.63f)
        {
            //destroy the projectile
            Destroy(gameObject);
        }

        //check if position of projectile exceeds boudns on x axisd
        if (transform.position.y > yRange + 1 || transform.position.y < -yRange - 1)
        {
            //destroy the projectile
            Destroy(gameObject);
        }
    }
}
