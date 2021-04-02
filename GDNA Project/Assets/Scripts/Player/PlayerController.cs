using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public Vector3 movement = new Vector3();
    public float runSpeed = 5.0f;
    public float walkSpeed = 2.0f;

    public GameObject selfDestructBlast;
    public AudioSource playerAudio;

    public Rigidbody playerRb;
    public Animator playerAnim;
    public Animator playerSword;

    public bool flag = false;
    public bool flag1 = false;
    public bool flag2 = false;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) == true)
        {
            //slowed movement speed
            GetInput(walkSpeed);
        }
        else
        {
            //normal movement speed
            GetInput(runSpeed);
        }

        //move character
        playerRb.velocity = new Vector3(movement.x, movement.y, 0);

        if(Input.GetKey(KeyCode.Space) == true && flag2 == false)
        {
            playerSword.SetTrigger("Fire");
            flag2 = true;
        }
        
        if(!Input.GetKey(KeyCode.Space))
        {
            flag2 = false;
        }

        //self destruct
        if (Input.GetKey(KeyCode.LeftAlt) == true)
        {
            playerAnim.SetBool("SelfDestruct", true);

            if (flag == false)
            {
                playerAudio.Play();
                flag = true;
            }
        }
        //stop start self destuct audio, cancel self destruct anim
        else
        {
            if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("PlayerStartSelfDestruct"))
            {
                playerAudio.Stop();
            }

            playerAnim.SetBool("SelfDestruct", false);
            flag = false;
        }

        //create blast object
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("PlayerSelfDestruct") && flag1 == false)
        {
            Instantiate(selfDestructBlast, transform.position, transform.rotation);
            flag1 = true;
        }

        //reset flags
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("PlayerSelfDestructEnd"))
        {
            flag = false;
            flag1 = false;

            playerAnim.SetBool("Damaged", false);
        }
    }

    void GetInput(float speed)
    {
        // setting input variables
        movement.x = Input.GetAxisRaw("Horizontal") * speed;
        movement.y = Input.GetAxisRaw("Vertical") * speed;
    }
}
