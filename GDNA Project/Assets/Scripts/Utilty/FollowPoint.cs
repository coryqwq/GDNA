using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPoint : MonoBehaviour
{
    public GameObject point;
    public Vector3 offsetPos = new Vector3(0, 0, 0);

    public bool rotate = false;
    public float rotateSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        if(point != null)
        {
            transform.position = point.transform.position + offsetPos;
        }

        if (rotate == true)
        {
            transform.Rotate(Vector3.forward, rotateSpeed);
        }
    }
}
