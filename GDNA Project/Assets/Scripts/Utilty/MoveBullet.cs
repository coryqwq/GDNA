using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    GameState gameStateScript;

    public float speed = 3.0f;

    private void Start()
    {
        gameStateScript = GameObject.FindWithTag("GameState").GetComponent<GameState>();
        speed *= gameStateScript.projectileSpeedOffset;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }
}
