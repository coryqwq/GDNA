using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletForward : MonoBehaviour
{
    public int speed = 100;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }
}
