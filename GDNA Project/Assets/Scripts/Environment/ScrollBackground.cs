using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public GameObject background;

    public bool spawned = false;
    public float speed = 10;

    // Update is called once per frame
    void Update()
    {
        //spawn the background at reset position
        if (transform.position.y <= 0 && spawned == false)
        {
            Instantiate(background, new Vector3(1.8f, 56.9f, 8), transform.rotation);
            spawned = true;
        }

        //move image down
        transform.Translate(Vector2.down * Time.deltaTime * speed);

        //destroy the background at passed position
        if(transform.position.y <= -60)
        {
            Destroy(gameObject);
        }
    }
}
