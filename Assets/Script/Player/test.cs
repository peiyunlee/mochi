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
    private bool isStick = false;
    public float force = 20;
    public GameObject item = null;
    public GameObject stickItem = null;
    public float push = 0;
    private bool GetKeyJump = false;
    public bool canBomb = false;
    public GameObject stickPlayer = null;
    bool isPlayer = false;
    // Use this for initialization
    void Start()
    {
        jellySprite = GetComponent<UnityJellySprite>();
        item = null;
    }
    void Update()
    {
        GetKeyJump = Input.GetButtonDown("Jump_" + this.tag);
        if (Input.GetButtonDown("Stick_" + this.tag))
        {
            isStick = !isStick;
        }
        jellySprite.SetStick(isStick);
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
                // jellySprite.AddForce(new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag) * push, 0.0f));

                if (item != null && (item.gameObject.tag == "player1" || item.gameObject.tag == "player2" || item.gameObject.tag == "player3" || item.gameObject.tag == "player4" || item.gameObject.tag == "player5"))
                {
                    Vector2 turn=new Vector2(0.0f,0.0f);
                    // Debug.Log(GameObject.Find(item.gameObject.tag.ToString()));
                    // stickPlayer=GameObject.Find(item.gameObject.tag.ToString());
                    // jellySprite.AddVelocity(new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag) * moveSpeed, 0.0f));
                    if (Input.GetAxisRaw("Horizontal_" + this.tag) < 0)
                    {
                        turn.x=Vector2.left.x * moveSpeed;
                        // jellySprite.AddVelocity(new Vector2(Vector2.left.x * moveSpeed, Vector2.left.y * moveSpeed));
                    }
                    if (Input.GetAxisRaw("Horizontal_" + this.tag) > 0)
                    {
                        turn.x=Vector2.right.x * moveSpeed;
                        // jellySprite.AddVelocity(new Vector2(Vector2.right.x * moveSpeed, Vector2.right.y * moveSpeed));
                    }
                    if (Input.GetAxisRaw("Vertical_" + this.tag) > 0)
                    {
                        turn.y=Vector2.up.y * moveSpeed;
                        // jellySprite.AddVelocity(new Vector2(Vector2.up.x * moveSpeed, Vector2.up.y * moveSpeed));
                    }
                    if (Input.GetAxisRaw("Vertical_" + this.tag) < 0)
                    {
                        turn.y=Vector2.down.y * moveSpeed;
                        // jellySprite.AddVelocity(new Vector2(Vector2.down.x * moveSpeed, Vector2.down.y * moveSpeed));
                    }
                    jellySprite.AddVelocity(turn,false);
                }
                else
                {
                    jellySprite.AddForce(new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag) * push, 0.0f));
                }


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
                jellySprite.SetStick(isStick);
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
