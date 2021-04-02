using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pod153Controller : MonoBehaviour
{
    public float spawnInterval = 0.05f;
    public float projectileDamage = 20.0f;

    public GameObject podMovementReference;
    public GameObject projectilePrefab;

    public float timer = 0;

    private Animator podAnim;
    private AudioSource audioSource;
    public AudioClip fireStartClip;
    public AudioClip firingClip;

    public bool flag0 = false;
    public bool flag1 = false;
    public bool flag2 = false;
    public bool flag3 = false;
    public bool flag4 = false;

    public void Start()
    {
        //assign animator to reference
        podAnim = GetComponent<Animator>();

        //assign audio source to reference
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        projectileDamage = Random.Range(50, 60);

        timer += Time.deltaTime;

        //fire pod153
        if (Input.GetMouseButton(1) && timer > spawnInterval)
        {
            if (flag0 == false)
            {
                //trigger starting fire animation, play fire start audio clip
                podAnim.SetBool("FireStart", true);

                if (podAnim.GetBool("Fired") == true && flag4 == false)
                {
                    podAnim.SetBool("Firing", true);
                    audioSource.PlayOneShot(fireStartClip);
                    flag4 = true;
                }

                flag0 = true;
                flag1 = false;
            }

            //wait for firing animation, play audio clip, create projectile
            if (podAnim.GetCurrentAnimatorStateInfo(0).IsName("Pod153Firing"))
            {
                if (flag2 == false)
                {
                    //play firing audio clip
                    audioSource.PlayOneShot(firingClip);
                    podAnim.SetBool("FireStart", false);
                    podAnim.SetBool("Firing", false);

                    flag2 = true;
                }

                Instantiate(projectilePrefab, transform.position, podMovementReference.transform.rotation);
            }
            timer = 0;
        }

        if (!Input.GetMouseButton(1))
        {
            //trigger closing fire animation, reset flags and animation parameter values

            podAnim.SetBool("FireStart", false);
            podAnim.SetBool("Firing", false);
            podAnim.SetBool("Fired", true);

            flag0 = false;

        }

        if (podAnim.GetCurrentAnimatorStateInfo(0).IsName("Pod153FireEnd"))
        {
            podAnim.SetBool("Fired", false);

            flag2 = false;
            flag3 = false;
            flag4 = false;
        }
    }
}
