using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Anima : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;

    public float pullForce;

    [SerializeField]
    private bool canJump;
    private bool isJump;
    private bool canMove;
    private UnityJelly_Anima jellySprite;
    private bool GetKeyJump;
    private int testGetKeyHMove;
    private int testGetKeyVMove;

    testPlayerFloorDetect playerFloorDetect;

    public int testType;

    private Animator playerAnim;

    void Start()
    {
        playerAnim=GetComponent<Animator>();
        jellySprite = GetComponent<UnityJelly_Anima>();
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

            if (Input.GetKeyDown("right")) testGetKeyHMove = 1;
            else if (Input.GetKeyDown("left")) testGetKeyHMove = -1;
            else if (Input.GetKeyUp("right") || Input.GetKeyUp("left")) testGetKeyHMove = 0;

            if (Input.GetKeyDown("up")) testGetKeyVMove = 1;
            else if (Input.GetKeyDown("down")) testGetKeyVMove = -1;
            else if (Input.GetKeyUp("up") || Input.GetKeyUp("down")) testGetKeyVMove = 0;
        }

        else if (testType == 2)
        {
            GetKeyJump = Input.GetButtonDown("Jump_" + this.tag) || Input.GetKeyDown("f");

            if (Input.GetKeyDown("d")) testGetKeyHMove = 1;
            else if (Input.GetKeyDown("a")) testGetKeyHMove = -1;
            else if (Input.GetKeyUp("d") || Input.GetKeyUp("a")) testGetKeyHMove = 0;

            if (Input.GetKeyDown("w")) testGetKeyVMove = 1;
            else if (Input.GetKeyDown("s")) testGetKeyVMove = -1;
            else if (Input.GetKeyUp("w") || Input.GetKeyUp("s")) testGetKeyVMove = 0;
        }

        else if (testType == 3)
        {
            GetKeyJump = Input.GetKeyDown("4");


            if (Input.GetKeyDown("3")) testGetKeyHMove = 1;
            else if (Input.GetKeyDown("1")) testGetKeyHMove = -1;
            else if (Input.GetKeyUp("3") || Input.GetKeyUp("1")) testGetKeyHMove = 0;
        }

        else if (testType == 4)
        {
            GetKeyJump = Input.GetKeyDown("k");


            if (Input.GetKeyDown("l")) testGetKeyHMove = 1;
            else if (Input.GetKeyDown("j")) testGetKeyHMove = -1;
            else if (Input.GetKeyUp("l") || Input.GetKeyUp("j")) testGetKeyHMove = 0;
        }

        // ResetRotation();

        // canJump = playerFloorDetect.isOnFloor;

        if (canJump && isJump)
            isJump = false;
    }

    void FixedUpdate()
    {
        if (GetKeyJump && canJump)
        {
            isJump = true;
            Jump();
        }

        Move();

    }
    void Move()
    {
        // jellySprite.AddVelocity(new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag) * moveSpeed, 0.0f));
        // if(testGetKeyHMove!=0){
        //     playerAnim.SetBool("isWalk", true);
        // }
        // else{
        //     playerAnim.SetBool("isWalk", false);
        // }
        jellySprite.AddVelocity(new Vector2(testGetKeyHMove * moveSpeed, 0.0f));
    }
    void Pull()
    {
        // jellySprite.AddForce(new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag) * pullForce, 0.0f));
        jellySprite.AddForce(new Vector2(testGetKeyHMove * pullForce, testGetKeyVMove * pullForce));
    }

    void Jump()
    {
        canJump = false;
        jellySprite.AddForce(new Vector2(0, jumpSpeed));
    }
}

