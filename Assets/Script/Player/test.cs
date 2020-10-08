using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public bool canJump = true;
    public bool canStick = false;
    private int jumpCount = 0;
    private UnityJellySprite jellySprite;
    UnityJellySprite.JellyCollision2D jellyCollision;
    private bool isStick = false;
    public float force = 20;
    public GameObject item = null;
    private GameObject stickItem = null;
    public float push = 0;
    private bool GetKeyJump=false;
    // Use this for initialization
    void Start()
    {
        jellySprite = GetComponent<UnityJellySprite>();
        item = null;
    }
    void Update()
    {
        GetKeyJump=Input.GetButtonDown("Jump_" + this.tag);
        if (Input.GetButtonDown("Stick_" + this.tag))
        {
            isStick = !isStick;

            // if (isStick&&!canStick)
            // {
            //     isStick=false;
            // }
            if (item != null && isStick)
            {
                stickItem = item;
                // Debug.Log(stickItem.name);
            }
            else
            {
                stickItem = null;
            }

            // jellySprite.SetItemStick(isStick, stickItem);

            //Item.transform.parent=this.transform;
        }
        // else if (Input.GetButtonUp("Stick_" + this.tag))
        // {
        //     jellySprite.PullItem(true);
        // }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isStick)
        {
            Move();

            Jump();

        }
        else
        {
            if (jellySprite.haveTouch)
            {
                jellySprite.AddForce(new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag) * push, 0.0f));
                // jellySprite.AddVelocity(new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag)*push,0.0f));

                // if (stickItem != null)
                // {
                //     stickItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag), 0.0f)*push);
                //     // stickItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag) * push, 0.0f));
                // }

            }
            else
            {
                isStick = !isStick;
                // jellySprite.SetStick(isStick, null);
            }

        }

    }
    void Move()
    {
        // jellySprite.AddForceAtPosition(new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag) * force, 0), new Vector2(this.transform.position.x + Input.GetAxisRaw("Horizontal_" + this.tag)*Time.deltaTime, this.transform.position.y));
        jellySprite.AddVelocity(new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag) * moveSpeed, 0.0f));
        //jellySprite.AddForce(new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag) * moveSpeed, 0));
    }

    void Jump()
    {
        if (GetKeyJump)
            Debug.Log("C");
        if (GetKeyJump && canJump)
        {
            canJump = false;
            Debug.Log(new Vector2(0, jumpSpeed));
            jellySprite.AddForce(new Vector2(0, jumpSpeed));
        }
    }

}
