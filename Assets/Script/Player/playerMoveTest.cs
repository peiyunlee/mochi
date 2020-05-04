using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveTest : MonoBehaviour
{
    //Ear相關
    public GameObject ear;
    public Rigidbody2D earJo;
    private Rigidbody2D earRb;
    private Rigidbody2D otherRb;
    private Collider2D earCollider;
    private HingeJoint2D earJoint;
    private EarStick earStick;
    //Body相關
    public GameObject body;
    public Rigidbody2D bodyJo;
    private Rigidbody2D bodyRb;
    private HingeJoint2D bodyJoint;
    private bodyStick bodyStick;

    //Player
    private Rigidbody2D rb;
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
        earCollider = ear.GetComponent<Collider2D>();
        earJoint = ear.GetComponent<HingeJoint2D>();
        bodyJoint = body.GetComponent<HingeJoint2D>();
        earStick = ear.GetComponent<EarStick>();
        bodyStick = body.GetComponent<bodyStick>();
        bodyRb = body.GetComponent<Rigidbody2D>();

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
        earCanTouch = earStick.earCanTouch;
        canTouch = bodyStick.canTouch;
        canJump = bodyStick.canJump;
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

            Move();

            Jump();
        }
        Touch();

        EarTurn();

        BodyTurn();

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
            Debug.Log("earIsTouch");
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
            earCollider.isTrigger = false;
        }
        else
        {
            earCollider.isTrigger = true;
            //rb.isKinematic = false;
        }

        if (earIsTouch)
        {
            rb.velocity = new Vector2(0, 0);
            rb.isKinematic = true;
            otherRb = earStick.otherRb;
            earJoint.connectedBody = otherRb;
            earJoint.anchor=new Vector2(0.0f,0.97f);
            earJoint.useLimits = true;
            earJoint.enabled = true;
            earRb.isKinematic = false;
            bodyRb.isKinematic = false;
            bodyJoint.enabled = true;
            // bodyRb.freezeRotation=false;
            // earRb.freezeRotation=false;
        }
        else
        {
            earJoint.connectedBody = earJo;
            earJoint.anchor=new Vector2(-0.02f,-1.9f);
            earJoint.useLimits = false;
            //earJoint.enabled = false;
            bodyJoint.enabled = false;
            bodyRb.isKinematic = true;
            bodyJoint.enabled = false;
            // bodyRb.freezeRotation=true;
            // earRb.freezeRotation=true;
            // bodyRb.rotation=0;
            //rb.isKinematic = false;
        }

        if (!bodyIsTouch && !earIsTouch)
        {
            rb.isKinematic = false;
            earJoint.enabled = false;
            earRb.isKinematic = true;
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
            earStick.earCanTouch = false;
            canTouch = false;
            bodyStick.canTouch = false;
            canJump = false;
            bodyStick.canJump = false;
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
                //earRb.AddForce (new Vector3(Vector3.up.x * force, Vector3.up.y * force, 0));
                earRb.velocity = new Vector3(Vector2.up.x * force, Vector2.up.y * force, 0);
            }
            if (Input.GetAxisRaw("Vertical_" + this.tag) < 0)
            {
                //earRb.AddForce (new Vector3(Vector3.down.x * force, Vector3.down.y * force, 0));
                earRb.velocity = new Vector3(Vector2.down.x * force, Vector2.down.y * force, 0);
            }
            if (Input.GetAxisRaw("Horizontal_" + this.tag) < 0)
            {
                Debug.Log("Left");
                //earRb.AddForce (new Vector3(Vector3.left.x * force, Vector3.left.y * force, 0));
                earRb.velocity = new Vector3(Vector2.left.x * force, Vector2.left.y * force, 0);
            }
            if (Input.GetAxisRaw("Horizontal_" + this.tag) > 0)
            {
                //earRb.AddForce (new Vector3(Vector3.right.x * force, Vector3.right.y * force, 0));
                earRb.velocity = new Vector3(Vector2.right.x * force, Vector2.right.y * force, 0);
            }
            if (Input.GetAxisRaw("Horizontal_" + this.tag) == 0 && Input.GetAxisRaw("Vertical_" + this.tag) == 0)
            {
                //earRb.AddForce (new Vector3(0, 0, 0));
                earRb.velocity = new Vector3(0, 0, 0);
            }
        }
        else
        {
            //earRb.AddForce (new Vector3(0, 0, 0));
            earRb.velocity = new Vector3(0, 0, 0);
        }
    }

    void BodyTurn()
    {
        if (earIsTouch)
        {
            if (Input.GetAxisRaw("Vertical_" + this.tag) > 0)
            {
               // bodyRb.AddForce (new Vector3(Vector3.up.x * force, Vector3.up.y * force, 0));
                bodyRb.velocity = new Vector3(Vector2.up.x * force, Vector2.up.y * force, 0);
            }
            if (Input.GetAxisRaw("Vertical_" + this.tag) < 0)
            {
                //bodyRb.AddForce (new Vector3(Vector3.down.x * force, Vector3.down.y * force, 0));
                bodyRb.velocity = new Vector3(Vector2.down.x * force, Vector2.down.y * force, 0);
            }
            if (Input.GetAxisRaw("Horizontal_" + this.tag) < 0)
            {
                //bodyRb.AddForce (new Vector3(Vector3.left.x * force, Vector3.left.y * force, 0));
                bodyRb.velocity = new Vector3(Vector2.left.x * force, Vector2.left.y * force, 0);
            }
            if (Input.GetAxisRaw("Horizontal_" + this.tag) > 0)
            {
                //bodyRb.AddForce (new Vector3(Vector3.right.x * force, Vector3.right.y * force, 0));
                bodyRb.velocity = new Vector3(Vector2.right.x * force, Vector2.right.y * force, 0);
            }
            if (Input.GetAxisRaw("Horizontal_" + this.tag) == 0 && Input.GetAxisRaw("Vertical_" + this.tag) == 0)
            {
                //bodyRb.AddForce (new Vector3(0, 0, 0));
                bodyRb.velocity = new Vector3(0, 0, 0);
            }
        }
        else
        {
            bodyRb.velocity = new Vector3(0, 0, 0);
        }
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.tag != "wall")
    //     {
    //         canJump = true;

    //     }
    //     canTouch = true;
    // }

    // private void OnCollisionStay2D(Collision2D other)
    // {
    // }

    // private void OnCollisionExit2D(Collision2D other)
    // {
    // }
}
