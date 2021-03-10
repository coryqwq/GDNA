using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pod153Controller : MonoBehaviour
{
    public float spawnInterval = 0.05f;
    public float bulletDamage = 2.0f;

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
        podAnim = gameObject.GetComponent<Animator>();

        //assign audio source to reference
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        //fire pod153
        if (Input.GetMouseButton(1) && timer > spawnInterval)
        {
            if (flag0 == false)
            {
                //trigger starting fire animation, play fire start audio clip
                podAnim.SetBool("FireStart", true);

                if(flag3 == false)
                {
                    audioSource.PlayOneShot(fireStartClip);
                    flag3 = true;
                }

                if(flag4 == false)
                {
                    podAnim.SetBool("Firing", true);
                    flag4 = true;
                }

                flag0 = true;
                flag1 = false;
            }

            //wait for firing animation, play audio clip, create projectile
            if (podAnim.GetCurrentAnimatorStateInfo(0).IsName("Pod153Firing"))
            {
                if(flag2 == false)
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
            if (flag1 == false)
            {
                podAnim.SetBool("FireStart", false);
                podAnim.SetBool("Firing", false);

                flag1 = true;
                flag0 = false;
            }
        }

        if (podAnim.GetCurrentAnimatorStateInfo(0).IsName("Pod153FireEnd"))
        {
            flag2 = false;
            flag3 = false;
            flag4 = false;
        }
    }
}
