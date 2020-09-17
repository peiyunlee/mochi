using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Demo1
    //Ear相關
    enum State
    {
        Left,
        Right
    }
    State state = 0;
    State preState = 0;
    public GameObject ear;
    public Rigidbody2D earJo;
    private Rigidbody2D earRb;
    private Rigidbody2D otherRb;
    private Collider2D earCollider;
    private HingeJoint2D earJoint;
    private EarStick earStick;
    private Animator earAnim;
    public Rigidbody2D earPos;
    //Body相關
    public GameObject body;
    //public Rigidbody2D bodyJo;
    private Rigidbody2D bodyRb;
    JointAngleLimits2D bodyLimits;
    JointAngleLimits2D earLimits;
    private HingeJoint2D bodyJoint;
    //private bodyStick bodyStick;
    private Collider2D bodyCollider;
    private Animator bodyAnim;
    private bool earTouch = true;
    private bool bodyTouch = true;
    public bodyStick bodyStick;
    private Vector2 anchor;
    //Player
    private Rigidbody2D rb;
    private CircleCollider2D playerCollider;
    public float moveSpeed;
    public float jumpSpeed;
    private bool touchWall = false;
    //耳朵轉的力
    public float force = 0;
    private bool canJump = true;
    private int jumpCount = 0;
    private Vector3 dumpf = new Vector3(0, 0, 0);
    //黏
    private bool bodyIsTouch = false;
    private bool earIsTouch = false;
    //判斷碰到物體可黏
    private bool canTouch = true;
    public bool earCanTouch = false;
    //判斷按著黏按鍵
    private bool bodyIsStick = false;
    private bool earIsStick = false;
    private bool isThrow = true;

    private bool isDead = false;
    //new
    public Vector2 ropeHook;
    public float swingForce = 4f;

    LayerMask playerLayer;
    SpriteRenderer e_SR, b_SR;

    void Start()
    {
        // body = GameObject.Find("body (1) Central Ref Point").gameObject;
        //moveSpeed = 3;
        //jumpSpeed = 250;
        anchor = new Vector2(0, 0.58f);
        bodyLimits.max = 16.36f;
        bodyLimits.min = -17.22f;
        //earLimits.max = 17.57535;
        //earLimits.min = -25.63127;
        rb = GetComponent<Rigidbody2D>();
        earRb = ear.GetComponent<Rigidbody2D>();
        earCollider = ear.GetComponent<Collider2D>();
        earJoint = ear.GetComponent<HingeJoint2D>();
        bodyJoint = body.GetComponent<HingeJoint2D>();
        earStick = ear.GetComponent<EarStick>();
        //bodyStick = body.GetComponent<bodyStick>();
        bodyRb = body.GetComponent<Rigidbody2D>();
        bodyCollider = body.GetComponent<Collider2D>();
        playerCollider = GetComponent<CircleCollider2D>();
        earAnim = ear.GetComponent<Animator>();
        bodyAnim = body.GetComponent<Animator>();

        Check();

        //new
        e_SR = ear.GetComponent<SpriteRenderer>();
        b_SR = body.GetComponent<SpriteRenderer>();
        playerLayer = this.gameObject.layer;
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
        if (!isDead)
        {
            earCanTouch = earStick.earCanTouch;
            canTouch = bodyStick.canTouch;
            canJump = bodyStick.canJump;
            if (Input.GetButtonDown("BodyStick_" + this.tag))
            {
                //bodyIsStick = true;
                bodyIsStick = !bodyIsStick;
            }
            // else if (Input.GetButtonUp("BodyStick_" + this.tag))
            // {
            //     bodyIsStick = false;
            // }

            if (Input.GetButtonDown("EarStick_" + this.tag))
            {
                //earIsStick = true;
                earIsStick = !earIsStick;
            }
            // else if (Input.GetButtonUp("EarStick_" + this.tag))
            // {
            //     earIsStick = false;
            // }
        }


    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            if (!bodyIsTouch && !earIsTouch)
            {

                Move();

                Jump();
            }
            Touch();


            EarTurn();

            BodyTurn();
            // Debug.Log(isThrow);
        }
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
            // earJoint.useLimits = false;
            rb.velocity = new Vector2(0, 0);
            rb.isKinematic = true;
            playerCollider.isTrigger = true;
            earCollider.isTrigger = false;
            bodyCollider.isTrigger = false;
            earRb.isKinematic = false;

            earJoint.enabled = true;
            playerCollider.radius = 0.7f;


            if (bodyTouch)
            {
                bodyRb.velocity = new Vector2(0, 0);
                earRb.velocity = new Vector2(0, 0);
                bodyAnim.SetBool("isBodyStick", true);
                earAnim.SetBool("isBodyStick", true);

                //earRb.position=new Vector3(earRb.position.x,earRb.position.y-0.1f);
                bodyTouch = false;
            }
            if (!earAnim.applyRootMotion)
            {
                earAnim.applyRootMotion = earStick.rootMotion;
            }

        }
        else
        {
            playerCollider.radius = 0.96f;
            earCollider.isTrigger = true;

            if (!bodyTouch)
            {

                bodyAnim.SetBool("isBodyStick", false);
                earAnim.SetBool("isBodyStick", false);
                earStick.rootMotion = false;
                earAnim.applyRootMotion = earStick.rootMotion;
                bodyTouch = true;
            }

            //rb.isKinematic = false;

        }

        if (earIsTouch)
        {
            if (earStick.otherRb != null && earStick.otherRb != earJo)
            {
                if (earTouch)
                {
                    // earJoint.useLimits = false;
                    // earJoint.useLimits = true;
                    bodyRb.velocity = new Vector2(0, 0);
                    earRb.velocity = new Vector2(0, 0);

                    earJoint.connectedBody = earStick.otherRb;
                    earJoint.anchor = new Vector2(0.0f, 0.975f);

                    bodyJoint.anchor = anchor;
                    //bodyJoint.connectedAnchor = earPos.position;
                    bodyJoint.limits = bodyLimits;
                    earJoint.enabled = true;
                    bodyJoint.enabled = true;

                    earTouch = false;
                }
                rb.velocity = new Vector2(0, 0);
                rb.isKinematic = true;
                //otherRb = earStick.otherRb;


                earCollider.isTrigger = true;
                playerCollider.isTrigger = true;
                bodyCollider.isTrigger = false;

                earRb.isKinematic = false;
                bodyRb.isKinematic = false;
                // bodyRb.freezeRotation=false;
                // earRb.freezeRotation=false;
            }
        }
        else
        {

            //earJoint.useLimits = false;
            //earJoint.enabled = false;

            bodyJoint.enabled = false;
            bodyRb.isKinematic = true;


            if (!earTouch)
            {
                // Debug.Log(dumpf);
                rb.velocity = dumpf;
                // rb.AddForce(dumpf);
                earJoint.connectedBody = earJo;
                earJoint.anchor = new Vector2(-0.02f, -1.9f);
                isThrow = true;
                rb.isKinematic = false;

                //Debug.Log(f);
                //rb.AddForce(new Vector2(f.x, f.y));
                //bodyRb.rotation = 0;
                earTouch = true;
            }
            // bodyRb.freezeRotation=true;
            // earRb.freezeRotation=true;
            // bodyRb.rotation=0;
            //rb.isKinematic = false;
        }

        if (!bodyIsTouch && !earIsTouch)
        {
            // earJoint.useLimits = true;
            rb.isKinematic = false;
            // earJoint.useLimits = true;
            // earJoint.useLimits=true;

            // if (earJoint.jointAngle < 1 && earJoint.jointAngle > -1)
            // {
            //     earJoint.enabled = false;
            // }
            earJoint.enabled = false;
            // Debug.Log("angle" + earJoint.jointAngle);
            // Debug.Log("enabled" + earJoint.enabled);

            earRb.isKinematic = true;
            bodyCollider.isTrigger = true;
            playerCollider.isTrigger = false;
        }
    }

    void Move()
    {
        if (!isThrow)
        {
            Vector2 trans;
            trans = new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag) * moveSpeed, rb.velocity.y);
            rb.velocity = trans;
        }
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

                //earRb.AddForce(new Vector3(Vector3.up.x * force, Vector3.up.y * force, 0));
                earRb.velocity = new Vector3(Vector2.up.x * force, Vector2.up.y * force, 0);
            }
            if (Input.GetAxisRaw("Vertical_" + this.tag) < 0)
            {
                //earRb.AddForce(new Vector3(Vector3.down.x * force, Vector3.down.y * force, 0));
                earRb.velocity = new Vector3(Vector2.down.x * force, Vector2.down.y * force, 0);
            }
            if (Input.GetAxisRaw("Horizontal_" + this.tag) < 0)
            {

                //earRb.AddForce(new Vector3(Vector3.left.x * force, Vector3.left.y * force, 0));
                earRb.velocity = new Vector3(Vector2.left.x * force, Vector2.left.y * force, 0);
            }
            if (Input.GetAxisRaw("Horizontal_" + this.tag) > 0)
            {

                //earRb.AddForce(new Vector3(Vector3.right.x * force, Vector3.right.y * force, 0));
                earRb.velocity = new Vector3(Vector2.right.x * force, Vector2.right.y * force, 0);
            }
            if (Input.GetAxisRaw("Horizontal_" + this.tag) == 0 && Input.GetAxisRaw("Vertical_" + this.tag) == 0)
            {
                //earRb.AddForce(new Vector3(0, 0, 0));
                //earRb.velocity = new Vector3(0, 0, 0);
            }

            if (bodyRb.rotation < 90 && bodyRb.rotation > -90)
            {
                //anchor = new Vector2(((earRb.position.x + (earPos.position.x - earRb.position.x) / 12 * 29) - bodyRb.position.x) * Mathf.Cos(bodyRb.rotation-360), ((earRb.position.y + (earPos.position.y - earRb.position.y) / 12 * 29) - bodyRb.position.y) * Mathf.Sin(bodyRb.rotation-360));
                anchor = new Vector2(body.transform.InverseTransformPoint(new Vector2(earPos.position.x, earPos.position.y)).x * Mathf.Cos(bodyRb.rotation), body.transform.InverseTransformPoint(new Vector2(earPos.position.x, earPos.position.y)).y * Mathf.Sin(bodyRb.rotation));
                bodyLimits.max = 16.36f;
                bodyLimits.min = -17.22f;
            }
            else if (bodyRb.rotation < -90)
            {
                // anchor = new Vector2(body.transform.InverseTransformPoint(new Vector2(earPos.position.x, earPos.position.y)).x * Mathf.Cos(bodyRb.rotation + 180), body.transform.InverseTransformPoint(new Vector2(earPos.position.x, earPos.position.y)).y * Mathf.Sin(bodyRb.rotation + 180));
                anchor = new Vector2(body.transform.InverseTransformPoint(new Vector2(earPos.position.x, earPos.position.y)).x * Mathf.Cos(bodyRb.rotation + 90), body.transform.InverseTransformPoint(new Vector2(earPos.position.x, earPos.position.y)).y * Mathf.Sin(bodyRb.rotation + 180));
                bodyLimits.max = 164.06f;
                bodyLimits.min = 131.50f;
            }
            else if (bodyRb.rotation > 90)
            {
                anchor = new Vector2(body.transform.InverseTransformPoint(new Vector2(earPos.position.x, earPos.position.y)).x * Mathf.Cos(bodyRb.rotation + 180), body.transform.InverseTransformPoint(new Vector2(earPos.position.x, earPos.position.y)).y * Mathf.Sin(bodyRb.rotation + 180));
                bodyLimits.max = 16.36f;
                bodyLimits.min = -17.22f;
            }
            
            //Debug.Log(anchor);
            //bodyJoint.anchor = anchor;
        }
        else
        {
            //earRb.AddForce (new Vector3(0, 0, 0));
            earRb.velocity = new Vector3(0, 0, 0);
            //bodyJoint.anchor = anchor;
        }
    }
    bool isTurn = false;
    void BodyTurn()
    {
        if (earIsTouch)
        {
            ropeHook=earJoint.connectedAnchor;
            var playerToHookDirection = (ropeHook - (Vector2)transform.position).normalized;
            Vector2 perpendicularDirection;
            if (Input.GetAxisRaw("Vertical_" + this.tag) > 0)
            {
                //if(f.y<10.0f)f += new Vector3(Vector3.up.x * force, Vector3.up.y * force, 0)*Time.deltaTime*10f;

                // bodyRb.AddForce(new Vector3(Vector3.up.x * force, Vector3.up.y * force, 0));
                bodyRb.velocity = new Vector3(Vector2.up.x * force, Vector2.up.y * force, 0);
                // bodyRb.velocity = f;
                //rb.velocity = new Vector3(Vector2.up.x * force, Vector2.up.y * force, 0);
            }
            if (Input.GetAxisRaw("Vertical_" + this.tag) < 0)
            {
                //if(f.y>-10.0f)f += new Vector3(Vector3.down.x * force, Vector3.down.y * force, 0)*Time.deltaTime*10f;
                // bodyRb.AddForce(new Vector3(Vector3.down.x * force, Vector3.down.y * force, 0));
                bodyRb.velocity = new Vector3(Vector2.down.x * force, Vector2.down.y * force, 0);
                // bodyRb.velocity = f;
                //rb.velocity = new Vector3(Vector2.down.x * force, Vector2.down.y * force, 0);
            }
            if (Input.GetAxisRaw("Horizontal_" + this.tag) < 0)
            {
                // state = State.Left;
                // if (state != preState)
                // {
                //     dumpf = new Vector3(0, 0, 0);
                //     preState = state;
                // }
                // if (dumpf.x > -10.0f) dumpf += new Vector3(Vector3.left.x * force, 0, 0) * Time.deltaTime * 10f;
                // perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
                perpendicularDirection = new Vector2(-playerToHookDirection.y, 0);
                dumpf=perpendicularDirection*10.0f;
                // bodyRb.AddForce(new Vector3(Vector3.left.x * force, Vector3.left.y * force, 0));
                bodyRb.velocity = new Vector3(Vector2.left.x * force, Vector2.left.y * force, 0);
                // bodyRb.velocity = f;
                //rb.velocity = new Vector3(Vector2.left.x * force, Vector2.left.y * force, 0);
            }
            if (Input.GetAxisRaw("Horizontal_" + this.tag) > 0)
            {
                // state = State.Right;
                // if (state != preState)
                // {
                //     dumpf = new Vector3(0, 0, 0);
                //     preState = state;
                // }
                // if (dumpf.x < 10.0f) dumpf += new Vector3(Vector3.right.x * force, 0, 0) * Time.deltaTime * 10f;
                // perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                perpendicularDirection = new Vector2(playerToHookDirection.y, 0);
                dumpf=perpendicularDirection*10.0f;
                // bodyRb.AddForce(new Vector3(Vector3.right.x * force, Vector3.right.y * force, 0));
                bodyRb.velocity = new Vector3(Vector2.right.x * force, Vector2.right.y * force, 0);
                // bodyRb.velocity = f;
                //rb.velocity = new Vector3(Vector2.right.x * force, Vector2.right.y * force, 0);
            }
            if (Input.GetAxisRaw("Horizontal_" + this.tag) == 0 && Input.GetAxisRaw("Vertical_" + this.tag) == 0)
            {
                //f = new Vector3(0, 0, 0);
                //bodyRb.AddForce (new Vector3(0, 0, 0));
                //bodyRb.velocity = new Vector3(0, 0, 0);
                //rb.velocity = new Vector3(0, 0, 0);
            }
            
            rb.position = bodyRb.position;
            Debug.Log(dumpf);
        }
        else
        {
            bodyRb.velocity = new Vector3(0, 0, 0);
            bodyRb.angularVelocity = 0;
            earRb.angularVelocity = 0;

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ColorDetect(other);
        isThrow = false;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        //isThrow = false;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
    }

    //判斷顏色
    private void ColorDetect(Collision2D other)
    {

        if (other.gameObject.layer == 12)
        {
            Debug.Log("ok");
        }
        else if (playerLayer == other.gameObject.layer)
        {
            Debug.Log("ok");
        }
        else
        {
            if ((other.gameObject.tag != "player1")
            && (other.gameObject.tag != "player2")
            && (other.gameObject.tag != "player3")
            && (other.gameObject.tag != "player4"))
            {
                Debug.Log(playerLayer);
                Die();
            }
        }
    }

    public void Die()
    {
        e_SR.color = new Color(0, 0, 0, 0.5f);
        b_SR.color = new Color(0, 0, 0, 0.5f);
        rb.velocity = new Vector2(0, 0);
        rb.isKinematic = true;
        playerCollider.isTrigger = true;
        isDead = true;
        Debug.Log("die");
    }
}
