using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float moveSpeed;
    float jumpSpeed;

    //Turn
    public Rigidbody2D earRb;//围绕的物体

    public float force = 0;
    //Use this for initialization
    bool isTouch;

    void Start()
    {
        moveSpeed = 3;
        jumpSpeed = 100;
        rb = GetComponent<Rigidbody2D>();

        isTouch = false;

        Check();
    }

    void Check()
    {
        string[] temp = Input.GetJoystickNames();
        if (temp.Length > 0)
        {
            for (int i = 0; i < temp.Length; ++i)
            {
                if (!string.IsNullOrEmpty(temp[i]))
                {
                    //Not empty, controller temp[i] is connected
                    Debug.Log("Controller " + i + " is connected using: " + temp[i]);
                }
                else
                {
                    //If it is empty, controller i is disconnected
                    //where i indicates the controller number
                    Debug.Log("Controller: " + i + " is disconnected.");

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) isTouch = !isTouch;
    }

    void FixedUpdate()
    {
        if (!isTouch)
        {
            Move();

            Jump();
        }

        Turn();

    }

    void Move()
    {
        Vector2 trans;
        trans = new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag) * moveSpeed, rb.velocity.y);
        rb.velocity = trans;
        earRb.velocity = trans;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump_" + this.tag))
        {
            rb.AddForce(new Vector2(0, jumpSpeed));
            earRb.AddForce(new Vector2(0, jumpSpeed));
        }
    }
    void Turn()
    {
        if (isTouch)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //earRb.AddForce (Vector2.right * 200,0);
                earRb.velocity = new Vector3(Vector2.right.x * force, Vector2.right.y * force, 0);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                earRb.velocity = new Vector3(Vector2.left.x * force, Vector2.left.y * force, 0);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                earRb.velocity = new Vector3(Vector2.up.x * force, Vector2.up.y * force, 0);
            }
        }
    }
}
