using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJellyTest : MonoBehaviour
{

    // Use this for initialization
    GameObject center;
    Rigidbody2D rb;
    void Start()
    {
        center = GameObject.Find("body (1) Central Ref Point").gameObject;
        rb = center.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (center == null)
        {
            center = GameObject.Find("body (1) Central Ref Point").gameObject;
            rb = center.GetComponent<Rigidbody2D>();
        }
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {

        Vector2 trans;
        trans = new Vector2(Input.GetAxisRaw("Horizontal_player3") * 4.0f, rb.velocity.y);
        rb.velocity = trans;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump_player3"))
        {
            // rb.AddForce(new Vector2(0, 200f));
			rb.velocity = new Vector2(0, 50f);
        }
    }
}
