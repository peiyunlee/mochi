using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float moveSpeed;
    public float jumpSpeed;

    //Turn
    public GameObject ear;
    private Rigidbody2D earRb;//围绕的物体
    private HingeJoint2D earJoint;

    public float force = 0;
    //Use this for initialization
    private bool bodyIsTouch = false;
    private bool earIsTouch = false;
    private bool canJump = true;
    private int jumpCount = 0;
    private bool canTouch = true;
    private bool isStick = false;
    private float touchDir = 0f;

    void Start()
    {
        moveSpeed = 3;
        jumpSpeed = 250;
        rb = GetComponent<Rigidbody2D>();
        earRb = ear.GetComponent<Rigidbody2D>();
        earJoint = ear.GetComponent<HingeJoint2D>();

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
        if(Input.GetButtonDown("BodyStick_" + this.tag)){
            isStick=true;
        }
        else if(Input.GetButtonUp("BodyStick_" + this.tag)){
            isStick=false;
        }
        
    }

    void FixedUpdate()
    {
        if (!bodyIsTouch && !earIsTouch)
        {
            Move();

            Jump();
        }
        Touch();

        EarTurn();

    }

    void Touch()
    {
        if (canTouch && isStick)
        {
            bodyIsTouch = true;
            
        }
        else if(!isStick)
        {
            bodyIsTouch = false;
        }
        if (bodyIsTouch)
        {
            rb.velocity=new Vector2(0,0);
            rb.isKinematic=true;
            earRb.isKinematic=false;
            earJoint.enabled=true;
            // if(touchDir>0){
            //     JointAngleLimits2D jointAngle=earJoint.limits;
            //     jointAngle.min=-180;
            //     jointAngle.max=0;
            //     earJoint.limits=jointAngle;
            // }
            // else if(touchDir<0){
            //     JointAngleLimits2D jointAngle=earJoint.limits;
            //     jointAngle.min=0;
            //     jointAngle.max=180;
            //     earJoint.limits=jointAngle;
            // }
        }
        else{
            earJoint.enabled=false;
            earRb.isKinematic=true;
            rb.isKinematic=false;
        }
        //Debug.Log(bodyIsTouch);
    }

    void Move()
    {
        Vector2 trans;
        trans = new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag) * moveSpeed, rb.velocity.y);
        rb.velocity = trans;
        //earRb.velocity = trans;

        //Debug.Log(Input.GetAxisRaw("Vertical_"+this.tag));
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump_" + this.tag) && canJump)
        {
            canTouch=false;
            canJump = false;
            rb.AddForce(new Vector2(0, jumpSpeed));
            //earRb.AddForce(new Vector2(0, jumpSpeed));
        }
    }
    void EarTurn()
    {
        if (bodyIsTouch)
        {
            if (Input.GetAxisRaw("Vertical_" + this.tag) > 0)
            {
                //earRb.AddForce (Vector2.right * 200,0);
                earRb.velocity = new Vector3(Vector2.up.x * force, Vector2.up.y * force, 0);
            }
            if (Input.GetAxisRaw("Vertical_" + this.tag) < 0)
            {
                //earRb.AddForce (Vector2.right * 200,0);
                earRb.velocity = new Vector3(Vector2.down.x * force, Vector2.down.y * force, 0);
            }
            if (Input.GetAxisRaw("Horizontal_" + this.tag) < 0)
            {
                earRb.velocity = new Vector3(Vector2.left.x * force, Vector2.left.y * force, 0);
            }
            if (Input.GetAxisRaw("Horizontal_" + this.tag) > 0)
            {
                earRb.velocity = new Vector3(Vector2.right.x * force, Vector2.right.y * force, 0);
            }
        }
        else{
            earRb.velocity = new Vector3(0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "wall")
        {
            canJump = true;
            
        }
        canTouch=true;
        // if (other.gameObject.tag == "wall")
        // {
        //     canTouch = true;
            
        // }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        // if (other.gameObject.tag == "wall")
        // {
        //     canTouch = true;
        // }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        // if (other.gameObject.tag == "wall")
        // {
        //     canTouch = false;
        // }
    }
}
