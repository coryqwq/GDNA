using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pod042Controller : MonoBehaviour
{
    public GameObject podCompound;

    public GameObject projectilePrefab;
    public float spawnInterval = 0.05f;
    public float projectileDamage = 2.0f;

    private Animator podAnim;
    private AudioSource audioSource;
    private AudioClip clip;

    public float timer = 0;
    private float timer1 = 0;
    public bool flag0 = false;
    public bool flag1 = false;
    public void Start()
    {
        //assign animator to reference
        podAnim = gameObject.GetComponent<Animator>();

        //assign audio source and audio clip to reference
        audioSource = gameObject.GetComponent<AudioSource>();
        clip = gameObject.GetComponent<AudioSource>().clip;
    }

    void Update()
    {
        projectileDamage = Random.Range(10, 14);
        timer += Time.deltaTime;
        timer1 = Time.deltaTime;

        //fire pod042
        if (Input.GetMouseButton(0) && timer > spawnInterval)
        {
            if (flag0 == false)
            {
                //slow rotate speed
                podCompound.GetComponent<FollowPoint>().rotateSpeed *= 0.75f;
               
                //trigger starting fire animation
                podAnim.SetBool("FireStart", true);

                //trigger firing animation
                podAnim.SetBool("Firing", true);

                flag0 = true;
                flag1 = false;
            }

            //wait for firing animation, play audio clip, create projectile
            if (podAnim.GetCurrentAnimatorStateInfo(0).IsName("Pod042Firing"))
            {
                if (gameObject.name == "Pod042")
                {
                    audioSource.PlayOneShot(clip);

                    timer1 = 0;
                }

                Instantiate(projectilePrefab, transform.position
                           , transform.rotation * Quaternion.AngleAxis(90, Vector3.forward));
            }
  
            timer = 0;
        }

       

        if (!Input.GetMouseButton(0))
        {
            //trigger closing fire animation, reset flags and animation parameter values
            if (flag1 == false)
            {
                //reset rotate speed
                podCompound.GetComponent<FollowPoint>().rotateSpeed /= 0.75f;

                podAnim.SetBool("FireStart", false);
                podAnim.SetBool("Firing", false);

                flag1 = true;
                flag0 = false;
            }
        }
    }
}
