using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayerSeek : MonoBehaviour
{
    public float timer = 0;

    public Rigidbody targetRb;
    public int force = 0;

    public float seekDuration = 3f;

    public float projectileSpeed = 70.0f;

    public float scaleChangeRate = 0;
    public float scaleChangeSpeed = 0.5f;

    public AudioClip spawn;
    public AudioClip fire;

    public bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(spawn);

        targetRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        targetRb.AddForce(Vector3.right * force, ForceMode.Impulse);

    }

    void Update()
    {
        timer += Time.deltaTime;

        if(gameObject.transform.localScale.y < 8f)
        {
            scaleChangeRate += scaleChangeSpeed * Time.deltaTime;
            Vector3 scaleChange = new Vector3(0, scaleChangeRate, 0);
            transform.localScale += scaleChange;
        }

        if (GameObject.FindWithTag("Player") != null && timer < seekDuration)
        {
            Vector2 direction = new Vector2(
            GameObject.FindWithTag("Player").transform.position.x - transform.position.x,
            GameObject.FindWithTag("Player").transform.position.y - transform.position.y
            );

            transform.up = direction;
        }

        if (timer > seekDuration)
        {
            if(flag == false)
            {
                GetComponent<AudioSource>().PlayOneShot(fire);
                flag = true;
            }
            transform.Translate(Vector2.up * Time.deltaTime * projectileSpeed);
        }
    }
}
