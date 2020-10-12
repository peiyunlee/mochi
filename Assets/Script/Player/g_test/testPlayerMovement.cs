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

    public int testType;

    void Start()
    {
        jellySprite = GetComponent<UnityJellySprite>();
        playerStick = gameObject.GetComponentInChildren<testPlayerStick>();
        playerFloorDetect = gameObject.GetComponentInChildren<testPlayerFloorDetect>();
        if (gameObject.tag == "player1")
            testType = 1;
        else if (gameObject.tag == "player2")
            testType = 2;
        else if (gameObject.tag == "player3")
            testType = 3;
        else testType = 4;
    }
    void Update()
    {
        if (testType == 1)
        {
            GetKeyJump = Input.GetButtonDown("Jump_" + this.tag) || Input.GetKeyDown("z");

            if (Input.GetKeyDown("right")) testGetKeyMove = 1;
            else if (Input.GetKeyDown("left")) testGetKeyMove = -1;
            else if (Input.GetKeyUp("right") || Input.GetKeyUp("left")) testGetKeyMove = 0;
        }

        else if (testType == 2)
        {
            GetKeyJump = Input.GetButtonDown("Jump_" + this.tag) || Input.GetKeyDown("f");


            if (Input.GetKeyDown("d")) testGetKeyMove = 1;
            else if (Input.GetKeyDown("a")) testGetKeyMove = -1;
            else if (Input.GetKeyUp("d") || Input.GetKeyUp("a")) testGetKeyMove = 0;
        }

        else if (testType == 3)
        {
            GetKeyJump = Input.GetKeyDown("4");


            if (Input.GetKeyDown("3")) testGetKeyMove = 1;
            else if (Input.GetKeyDown("1")) testGetKeyMove = -1;
            else if (Input.GetKeyUp("3") || Input.GetKeyUp("1")) testGetKeyMove = 0;
        }

        else if (testType == 4)
        {
            GetKeyJump = Input.GetKeyDown("k");


            if (Input.GetKeyDown("l")) testGetKeyMove = 1;
            else if (Input.GetKeyDown("j")) testGetKeyMove = -1;
            else if (Input.GetKeyUp("l") || Input.GetKeyUp("j")) testGetKeyMove = 0;
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

    public void Pop(Vector2 slop, float popForce)
    {
        // jellySprite.AddVelocity(slop * popForce, false);
        jellySprite.AddForce(slop * 500.0f);
        jellySprite.AddForce(slop * 500.0f);
    }

}
