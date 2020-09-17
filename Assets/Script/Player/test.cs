using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    //public Rigidbody2D rb;
    //public Rigidbody2D ear1;
    //public Rigidbody2D ear2;
    public float moveSpeed;
    public float jumpSpeed;
    public bool canJump = true;
    private int jumpCount = 0;
    private UnityJellySprite jellySprite;
    private Collider2D jellyCollider;
    public FloorDetect floorDetect;
    // Use this for initialization
    void Start()
    {
        jellySprite = GetComponent<UnityJellySprite>();
    }
    void Update()
    {
        //canJump = floorDetect.canJump;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

        Jump();
    }
    void Move()
    {
        jellySprite.AddVelocity(new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag) * moveSpeed, 0.0f));
        // jellySprite.AddForce(new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag) * moveSpeed, 0));
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump_" + this.tag))
        {
            canJump = false;
            jellySprite.AddForce(new Vector2(0, jumpSpeed));
        }
    }

}
