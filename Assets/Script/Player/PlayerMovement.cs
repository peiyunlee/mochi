using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float moveSpeed;
    public float jumpSpeed;

    //Turn
    public Rigidbody2D earRb;//围绕的物体

    public float force = 0;
    //Use this for initialization
    bool isTouch;
    private bool canJump=true;
    private bool isGround=true;
    private int jumpCount=0;

    void Start()
    {
        moveSpeed = 3;
        jumpSpeed = 250;
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
        trans = new Vector2(Input.GetAxisRaw("Horizontal_"+this.tag) * moveSpeed, rb.velocity.y);
        rb.velocity = trans;
        earRb.velocity = trans;
        Debug.Log(Input.GetAxisRaw("Horizontal_"+this.tag));
        //Debug.Log(Input.GetAxisRaw("Vertical_"+this.tag));
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump_"+this.tag)&&canJump)
        {
            canJump=false;
            rb.AddForce(new Vector2(0, jumpSpeed));
            //earRb.AddForce(new Vector2(0, jumpSpeed));
        }
    }
    void Turn()
    {
        if (isTouch)
        {
            if (Input.GetAxisRaw("Vertical_"+this.tag)>0)
            {
                //earRb.AddForce (Vector2.right * 200,0);
                earRb.velocity = new Vector3(Vector2.up.x * force, Vector2.up.y * force, 0);
            }
            if (Input.GetAxisRaw("Vertical_"+this.tag)<0)
            {
                //earRb.AddForce (Vector2.right * 200,0);
                earRb.velocity = new Vector3(Vector2.down.x * force, Vector2.down.y * force, 0);
            }
            if (Input.GetAxisRaw("Horizontal_"+this.tag)<0)
            {
                earRb.velocity = new Vector3(Vector2.left.x * force, Vector2.left.y * force, 0);
            }
            if (Input.GetAxisRaw("Horizontal_"+this.tag)>0)
            {
                earRb.velocity = new Vector3(Vector2.right.x * force, Vector2.right.y * force, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D floor) {
        if(floor.gameObject.tag!="wall"){
            canJump=true;
            isGround=true;
        }
    }

    // private void OnCollisionStay2D(Collision2D floor) {
    //     if(floor.gameObject.tag=="ground"){
    //         canJump=true;
    //         isGround=true;
    //     }
    // }
    
    // private void OnCollisionExit2D(Collision2D floor) {
    //     if(floor.gameObject.tag=="ground"){
    //         canJump=false;
    //         isGround=false;
    //     }
    // }
}
