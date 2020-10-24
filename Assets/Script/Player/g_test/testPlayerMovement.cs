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
    private bool isJump;
    private bool canMove;
    private UnityJellySprite jellySprite;
    private bool GetKeyJump;
    private int testGetKeyMove;
    private int testGetKeyVMove;

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

            // GetKeyJump = Input.GetKeyDown("f");

            // if (Input.GetKeyDown("d")) testGetKeyMove = 1;
            // else if (Input.GetKeyDown("a")) testGetKeyMove = -1;
            // else if (Input.GetKeyUp("d") || Input.GetKeyUp("a")) testGetKeyMove = 0;

            // if (Input.GetKeyDown("w")) testGetKeyVMove = 1;
            // else if (Input.GetKeyDown("s")) testGetKeyVMove = -1;
            // else if (Input.GetKeyUp("w") || Input.GetKeyUp("s")) testGetKeyVMove = 0;
        }

        else
        {
            GetKeyJump = Input.GetButtonDown("Jump_" + this.tag) || Input.GetKeyDown("f");
            // GetKeyJump = Input.GetButtonDown("Jump_" + this.tag);

            // testGetKeyMove = (int)Input.GetAxisRaw("Horizontal_" + this.tag);
            // testGetKeyVMove = (int)Input.GetAxisRaw("Vertical_" + this.tag);
            if (Input.GetKeyDown("d")) testGetKeyMove = 1;
            else if (Input.GetKeyDown("a")) testGetKeyMove = -1;
            else if (Input.GetKeyUp("d") || Input.GetKeyUp("a")) testGetKeyMove = 0;
        }

        isJump = playerFloorDetect.m_isJump;

        ResetRotation();

        canJump = playerFloorDetect.isOnFloor;

        playerStick.getIsOnFloor = playerFloorDetect.isOnFloor;
    }

    void FixedUpdate()
    {
        if (GetKeyJump && canJump)
        {
            isJump = true;
            playerFloorDetect.m_isJump = true;
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
        jellySprite.AddForce(new Vector2(testGetKeyMove * pullForce, testGetKeyVMove*pullForce));
    }

    void Jump()
    {
        canJump = false;
        jellySprite.AddForce(new Vector2(0, jumpSpeed));
    }

    void ResetRotation()
    {

        jellySprite.ResetTurn();
        if (jellySprite.CentralPoint.transform.rotation.z != 0)
        {
            if (isJump && jellySprite.CentralPoint.Body2D.velocity.y > 0.0f)
            {

                jellySprite.JumpResetTurn();
            }
        }
    }
}
