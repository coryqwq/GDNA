using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public Vector3 movement = new Vector3();
    Rigidbody rb;

    public float runSpeed = 5.0f;
    public float walkSpeed = 2.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

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

        rb.velocity = new Vector3(movement.x, movement.y, 0);
    }

    void GetInput(float speed)
    {
        // setting input variables
        movement.x = Input.GetAxisRaw("Horizontal") * speed;
        movement.y = Input.GetAxisRaw("Vertical") * speed;
    }
}
