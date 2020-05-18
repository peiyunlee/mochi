using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject ear;
    private Rigidbody2D rb;
    private Rigidbody2D earRb;
    private HingeJoint2D earJoint;
    private EarStick earStick; 


    //Use this for initialization
    public float moveSpeed;
    public float jumpSpeed;
    //耳朵轉的力
    public float force = 0;
    private bool canJump = true;
    private int jumpCount = 0;

    //黏
    private bool bodyIsTouch = false;
    private bool earIsTouch = false;
    //判斷碰到物體可黏
    private bool canTouch = true;
    public bool earCanTouch = false;
    //判斷按著黏按鍵
    private bool bodyIsStick = false;
    private bool earIsStick = false;

    void Start()
    {
        moveSpeed = 3;
        jumpSpeed = 250;
        rb = GetComponent<Rigidbody2D>();
        earRb = ear.GetComponent<Rigidbody2D>();
        earJoint = ear.GetComponent<HingeJoint2D>();
        earStick=ear.GetComponent<EarStick>();

        Check();
    }

    //偵測連接的搖桿
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
        earCanTouch=earStick.earCanTouch;
        if (Input.GetButtonDown("BodyStick_" + this.tag))
        {
            bodyIsStick = true;
        }
        else if (Input.GetButtonUp("BodyStick_" + this.tag))
        {
            bodyIsStick = false;
        }

        if (Input.GetButtonDown("EarStick_" + this.tag))
        {
            earIsStick = true;
        }
        else if (Input.GetButtonUp("EarStick_" + this.tag))
        {
            earIsStick = false;
        }
        
    }

    void FixedUpdate()
    {
        if (!bodyIsTouch && !earIsTouch)
        {
            Debug.Log("earIsTouch");
            Move();

            Jump();
        }
        Touch();

        EarTurn();

        //BodyTurn();

    }

    void Touch()
    {
        //如果碰到物體and按著黏
        if (canTouch && bodyIsStick)
        {
            bodyIsTouch = true;

        }
        else if (!bodyIsStick)
        {
            bodyIsTouch = false;
        }

        if (earCanTouch && earIsStick)
        {
            earIsTouch = true;
        }
        else if (!earIsStick)
        {
            earIsTouch = false;
        }

        //身體黏住的功能調整
        if (bodyIsTouch)
        {
            rb.velocity = new Vector2(0, 0);
            rb.isKinematic = true;
            earRb.isKinematic = false;
            earJoint.enabled = true;
        }
        else
        {
            earJoint.enabled = false;
            earRb.isKinematic = true;
            rb.isKinematic = false;
        }
    }

    void Move()
    {
        Vector2 trans;
        trans = new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag) * moveSpeed, rb.velocity.y);
        rb.velocity = trans;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump_" + this.tag) && canJump)
        {
            earCanTouch = false;
            earStick.earCanTouch=false;
            canTouch = false;
            canJump = false;
            rb.AddForce(new Vector2(0, jumpSpeed));
        }
    }
    //耳朵轉
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
            if (Input.GetAxisRaw("Horizontal_" + this.tag) == 0 && Input.GetAxisRaw("Vertical_" + this.tag) == 0)
            {
                earRb.velocity = new Vector3(0, 0, 0);
            }
        }
        else
        {
            earRb.velocity = new Vector3(0, 0, 0);
        }
    }

    void BodyTurn()
    {
        if (earIsTouch)
        {
            if (Input.GetAxisRaw("Vertical_" + this.tag) > 0)
            {
                //earRb.AddForce (Vector2.right * 200,0);
                rb.velocity = new Vector3(Vector2.up.x * force, Vector2.up.y * force, 0);
            }
            if (Input.GetAxisRaw("Vertical_" + this.tag) < 0)
            {
                //earRb.AddForce (Vector2.right * 200,0);
                rb.velocity = new Vector3(Vector2.down.x * force, Vector2.down.y * force, 0);
            }
            if (Input.GetAxisRaw("Horizontal_" + this.tag) < 0)
            {
                rb.velocity = new Vector3(Vector2.left.x * force, Vector2.left.y * force, 0);
            }
            if (Input.GetAxisRaw("Horizontal_" + this.tag) > 0)
            {
                rb.velocity = new Vector3(Vector2.right.x * force, Vector2.right.y * force, 0);
            }
            if (Input.GetAxisRaw("Horizontal_" + this.tag) == 0 && Input.GetAxisRaw("Vertical_" + this.tag) == 0)
            {
                rb.velocity = new Vector3(0, 0, 0);
            }
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "wall")
        {
            canJump = true;

        }
        canTouch = true;
    }

    // private void OnCollisionStay2D(Collision2D other)
    // {
    // }

    // private void OnCollisionExit2D(Collision2D other)
    // {
    // }
}
