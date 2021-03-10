using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Scale(player.transform.position, new Vector3(0.4f,0.4f,0)) + new Vector3(0,0,-10);    
    }
}
