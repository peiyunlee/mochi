using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;

    public float pullForce;

    [SerializeField]
    private bool canJump;
    private bool canMove;
    private UnityJellySprite jellySprite;
    private bool GetKeyJump;
    private int testGetKeyMove;

    testPlayerStick playerStick;

    testPlayerFloorDetect playerFloorDetect;

    public bool testType;

    void Start()
    {
        jellySprite = GetComponent<UnityJellySprite>();
        playerStick = gameObject.GetComponentInChildren<testPlayerStick>();
        playerFloorDetect = gameObject.GetComponentInChildren<testPlayerFloorDetect>();
        if (gameObject.tag == "player1")
            testType = true;
        else testType = false;
    }
    void Update()
    {
        if (testType)
        {
            GetKeyJump = Input.GetButtonDown("Jump_" + this.tag) || Input.GetKeyDown("z");

            if (Input.GetKeyDown("right")) testGetKeyMove = 1;
            else if (Input.GetKeyDown("left")) testGetKeyMove = -1;
            else if (Input.GetKeyUp("right") || Input.GetKeyUp("left")) testGetKeyMove = 0;
        }

        else
        {
            GetKeyJump = Input.GetButtonDown("Jump_" + this.tag) || Input.GetKeyDown("f");


            if (Input.GetKeyDown("d")) testGetKeyMove = 1;
            else if (Input.GetKeyDown("a")) testGetKeyMove = -1;
            else if (Input.GetKeyUp("d") || Input.GetKeyUp("a")) testGetKeyMove = 0;
        }


        canJump = playerFloorDetect.isOnFloor;

        playerStick.getIsOnFloor = playerFloorDetect.isOnFloor;
    }

    void FixedUpdate()
    {
        if (GetKeyJump && canJump)
        {
            Jump();
        }

        if (!playerStick.isStick)
            Move();
        else
            Pull();

    }
    void Move()
    {
        // jellySprite.AddVelocity(new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag) * moveSpeed, 0.0f));
        jellySprite.AddVelocity(new Vector2(testGetKeyMove * moveSpeed, 0.0f));
    }
    void Pull()
    {
        // jellySprite.AddForce(new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag) * pullForce, 0.0f));
        jellySprite.AddForce(new Vector2(testGetKeyMove * pullForce, 0.0f));
    }

    void Jump()
    {
        canJump = false;
        jellySprite.AddForce(new Vector2(0, jumpSpeed));
    }

}
