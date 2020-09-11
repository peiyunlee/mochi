using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    //public Rigidbody2D rb;
    public Rigidbody2D bodyRb;
    //public Rigidbody2D ear1;
    //public Rigidbody2D ear2;
    public float force;
    // Use this for initialization
    void Start()
    {
        bodyRb = GameObject.Find("body Central Ref Point").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BodyTurn();
    }
    void BodyTurn()
    {
        if (Input.GetAxisRaw("Vertical_" + this.tag) > 0)
        {
            //f = new Vector3(Vector3.up.x * force * 10.0f, Vector3.up.y * force * 10.0f, 0);
            bodyRb.AddForce(new Vector3(Vector3.up.x * force, Vector3.up.y * force, 0));
            //bodyRb.velocity = new Vector3(Vector2.up.x * force, Vector2.up.y * force, 0);
            //rb.velocity = new Vector3(Vector2.up.x * force, Vector2.up.y * force, 0);
        }
        if (Input.GetAxisRaw("Vertical_" + this.tag) < 0)
        {
            //f = new Vector3(Vector3.down.x * force * 10.0f, Vector3.down.y * force * 10.0f, 0);
            bodyRb.AddForce(new Vector3(Vector3.down.x * force, Vector3.down.y * force, 0));
            //bodyRb.velocity = new Vector3(Vector2.down.x * force, Vector2.down.y * force, 0);
            //rb.velocity = new Vector3(Vector2.down.x * force, Vector2.down.y * force, 0);
        }
        if (Input.GetAxisRaw("Horizontal_" + this.tag) < 0)
        {
            //f = new Vector3(Vector3.left.x * force * 10.0f, Vector3.left.y * force * 10.0f, 0);
            bodyRb.AddForce(new Vector3(Vector3.left.x * force, Vector3.left.y * force, 0));
            //bodyRb.velocity = new Vector3(Vector2.left.x * force, Vector2.left.y * force, 0);
            //rb.velocity = new Vector3(Vector2.left.x * force, Vector2.left.y * force, 0);
        }
        if (Input.GetAxisRaw("Horizontal_" + this.tag) > 0)
        {
            //f = new Vector3(Vector3.right.x * force * 10.0f, Vector3.right.y * force * 10.0f, 0);
            bodyRb.AddForce(new Vector3(Vector3.right.x * force, Vector3.right.y * force, 0));
            //bodyRb.velocity = new Vector3(Vector2.right.x * force, Vector2.right.y * force, 0);
            //rb.velocity = new Vector3(Vector2.right.x * force, Vector2.right.y * force, 0);
        }
        if (Input.GetAxisRaw("Horizontal_" + this.tag) == 0 && Input.GetAxisRaw("Vertical_" + this.tag) == 0)
        {
            //f = new Vector3(0, 0, 0);
            //bodyRb.AddForce (new Vector3(0, 0, 0));
            //bodyRb.velocity = new Vector3(0, 0, 0);
            //rb.velocity = new Vector3(0, 0, 0);
        }
        //rb.position = bodyRb.position;
    }

}
