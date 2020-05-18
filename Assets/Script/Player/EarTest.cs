using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarTest : MonoBehaviour
{
    //Ear相關
    public GameObject ear;
    public Rigidbody2D earJo;
    private Rigidbody2D earRb;
    private Rigidbody2D otherRb;
    private Collider2D earCollider;
    private HingeJoint2D earJoint;
    private EarStick earStick;
    // //Body相關
     public GameObject body;
    // //public Rigidbody2D bodyJo;
     private Rigidbody2D bodyRb;
    private HingeJoint2D bodyJoint;
    private bodyStick bodyStick;
    private Collider2D bodyCollider;
    //Player
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private Vector2 jointAnchor=new Vector2(0.0f, 0.975f);
    //Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        earRb = ear.GetComponent<Rigidbody2D>();
        earCollider = ear.GetComponent<Collider2D>();
        earJoint = ear.GetComponent<HingeJoint2D>();
        bodyJoint = body.GetComponent<HingeJoint2D>();
        earStick = ear.GetComponent<EarStick>();
        bodyStick = body.GetComponent<bodyStick>();
        bodyRb = body.GetComponent<Rigidbody2D>();
        bodyCollider = body.GetComponent<Collider2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // rb.velocity = new Vector2(0, 0);

        //otherRb = earStick.otherRb;
         rb.isKinematic = true;
        earRb.isKinematic = false;
        bodyRb.isKinematic = false;

        playerCollider.isTrigger = true;
        bodyCollider.isTrigger = false;

        //earJoint.anchor = jointAnchor;
        //earJoint.connectedBody = earStick.otherRb;

        earJoint.useLimits = true;
        earJoint.enabled = true;
        bodyJoint.enabled = true;


        if (Input.GetAxisRaw("Vertical_" + this.tag) > 0)
        {
            //earRb.AddForce (Vector2.right * 200,0);
            bodyRb.velocity = new Vector3(Vector2.up.x * 5, Vector2.up.y * 5, 0);
        }
        if (Input.GetAxisRaw("Vertical_" + this.tag) < 0)
        {
            //earRb.AddForce (Vector2.right * 200,0);
            bodyRb.velocity = new Vector3(Vector2.down.x * 5, Vector2.down.y * 5, 0);
        }
        if (Input.GetAxisRaw("Horizontal_" + this.tag) < 0)
        {
            bodyRb.velocity = new Vector3(Vector2.left.x * 5, Vector2.left.y * 5, 0);
        }
        if (Input.GetAxisRaw("Horizontal_" + this.tag) > 0)
        {
            bodyRb.velocity = new Vector3(Vector2.right.x * 5, Vector2.right.y * 5, 0);
        }

    }
}
