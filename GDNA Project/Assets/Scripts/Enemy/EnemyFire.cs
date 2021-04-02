using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject enemyCollider;
    EnemyStatus enemyStatusScript;

    public Animator enemyAnim;
    public GameObject[] gun;
    public AudioClip[] clip;
    public bool[] phase = new bool[3] { false, false, false };
    public GameObject[] bulletType;
    public GameObject laser;
    public float fireBulletWedgeInterval = 0.2f;
    public float fireBulletOmniInterval = 0.5f;
    public float fireLaserDuration = 1.0f;
    public float fireLaserInterval = 1.0f;

    public float rotateSpeed = 1.0f;
    public float maxAngle = 0;

    public bool flag = false;
    public float timer = 0;

    private void Start()
    {
        enemyStatusScript = enemyCollider.GetComponent<EnemyStatus>();
    }
    public void StartEnemyPhase(int index)
    {
        flag = false;
        maxAngle = 0;
        phase[index] = true;
    }

    public void FireWedge()
    {
        gun[0].GetComponent<AudioSource>().PlayOneShot(clip[0]);

        int angle = 0;

        //angle between projectiles
        int angleIncrement = Random.Range(5, 30);

        //spawn some number (3 to 6) of projectiles at the angle apart
        for (int i = 0; i < Random.Range(3, 7); i++)
        {
            Instantiate(bulletType[Random.Range(0,2)], gun[0].transform.position, gun[0].transform.rotation * Quaternion.AngleAxis(angle, Vector3.forward));
            Instantiate(bulletType[Random.Range(0, 2)], gun[1].transform.position, gun[1].transform.rotation * Quaternion.AngleAxis(-angle, Vector3.forward));
            angle += angleIncrement;
        }
    }

    public void FireOmni()
    {
        gun[0].GetComponent<AudioSource>().PlayOneShot(clip[0]);

        int angle = 0;
        int[] angleArray = new int[] { 10, 15, 20, 25, 30, 35, 40, 45 };

        //angle between projectiles
        int angleIncrement = angleArray[Random.Range(0, angleArray.Length)];

        //assign bullet type
        int bulletIndex = Random.Range(0, 2);

        //spawn the number of projectiles to complete 360 degrees at the angle apart
        for (int i = 0; i < 360 / angleIncrement; i++)
        {
            Instantiate(bulletType[bulletIndex], gun[0].transform.position, gun[0].transform.rotation * Quaternion.AngleAxis(angle, Vector3.forward));
            Instantiate(bulletType[bulletIndex], gun[1].transform.position, gun[1].transform.rotation * Quaternion.AngleAxis(angle, Vector3.forward));

            angle += angleIncrement;
        }
    }

    public void FireLaser()
    {
        GetComponent<AudioSource>().PlayOneShot(clip[1]);

        Instantiate(laser, transform.position + new Vector3(Random.Range(-3, 3), -5.3f, -0.1f), laser.transform.rotation);
    }

    private void Update()
    {
        if(enemyStatusScript.isAlive == false)
        {
            for(int i = 0; i < phase.Length; i++)
            {
                phase[i] = false;
            }

            enemyAnim.SetBool("Firing", false);
            CancelInvoke();
        }

        //phase 1 spin 360 degrees firing directionally
        if (phase[0] == true)
        {
            if (maxAngle <= 360)
            {
                maxAngle += Time.deltaTime * rotateSpeed;

                enemyAnim.SetBool("Firing", true);

                gun[0].transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
                gun[1].transform.Rotate(Vector3.forward * Time.deltaTime * -rotateSpeed);

                if (flag == false)
                {
                    InvokeRepeating("FireWedge", 0, fireBulletWedgeInterval);
                    flag = true;
                }
            }
            else
            {
                enemyAnim.SetBool("Firing", false);
                CancelInvoke("FireWedge");
                phase[0] = false;
            }
        }

        //phase 2 spin 180 degrees firing omnidirectionally
        if (phase[1] == true)
        {
            if (maxAngle <= 180)
            {
                maxAngle += Time.deltaTime * rotateSpeed;

                enemyAnim.SetBool("Firing", true);

                gun[0].transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
                gun[1].transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);

                if (flag == false)
                {
                    InvokeRepeating("FireOmni", 0, fireBulletOmniInterval);
                    flag = true;
                }
            }
            else
            {
                enemyAnim.SetBool("Firing", false);
                CancelInvoke("FireOmni");
                phase[1] = false;
            }
        }

        //phase 3 spawn laser compound
        if (phase[2] == true)
        {
            timer += Time.deltaTime;

            if(timer <= fireLaserDuration)
            {
                if (flag == false)
                {
                    InvokeRepeating("FireLaser", 0, fireLaserInterval);
                    flag = true;
                }
            }
            else
            {
                timer = 0;
                CancelInvoke("FireLaser");
                phase[2] = false;
            }
        }
    }

}
