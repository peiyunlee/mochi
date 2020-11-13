using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerMovement : MonoBehaviour
{
    public string playerColor;
    public float moveSpeed;
    public float jumpSpeed;

    public float pullForce;

    [SerializeField]
    private bool canJump;
    private bool isJump;
    private bool canMove;
    private bool isMove;
    private UnityJellySprite jellySprite;
    private bool GetKeyJump;
    private int testGetKeyHMove;
    private int testGetKeyVMove;

    testPlayerStick playerStick;

    testPlayerFloorDetect playerFloorDetect;

    public int testType;

    bool notFreeze;

    public bool isFollow;

    public Rigidbody2D followTarget;

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

        followTarget = null;
    }

    void Update()
    {
        if (testType == 1)
        {
            GetKeyJump = Input.GetButtonDown("Jump_" + this.tag) || Input.GetKeyDown("z");

            if (Input.GetKeyDown("right")) { testGetKeyHMove = 1; isMove = true; }
            else if (Input.GetKeyDown("left")) { testGetKeyHMove = -1; isMove = true; }
            else if (Input.GetKeyUp("right") || Input.GetKeyUp("left")) { testGetKeyHMove = 0; isMove = false; }

            if (Input.GetKeyDown("up")) { testGetKeyHMove = 1; isMove = true; }
            else if (Input.GetKeyDown("down")) { testGetKeyHMove = -1; isMove = true; }
            else if (Input.GetKeyUp("up") || Input.GetKeyUp("down")) { testGetKeyHMove = 0; isMove = false; }
        }

        else if (testType == 2)
        {
            GetKeyJump = Input.GetButtonDown("Jump_" + this.tag) || Input.GetKeyDown("f");

<<<<<<< HEAD
            if (Input.GetKeyDown("d")) testGetKeyHMove = 1;
            else if (Input.GetKeyDown("a")) testGetKeyHMove = -1;
            else if (Input.GetKeyUp("d") || Input.GetKeyUp("a")) testGetKeyHMove = 0;

            if (Input.GetKeyDown("w")) testGetKeyVMove = 1;
            else if (Input.GetKeyDown("s")) testGetKeyVMove = -1;
            else if (Input.GetKeyUp("w") || Input.GetKeyUp("s")) testGetKeyVMove = 0;
=======
            if (Input.GetKeyDown("d")) { testGetKeyHMove = 1; isMove = true; }
            else if (Input.GetKeyDown("a")) { testGetKeyHMove = -1; isMove = true; }
            else if (Input.GetKeyUp("d") || Input.GetKeyUp("a")) { testGetKeyHMove = 0; isMove = false; }

            if (Input.GetKeyDown("w")) { testGetKeyHMove = 1; isMove = true; }
            else if (Input.GetKeyDown("s")) { testGetKeyHMove = -1; isMove = true; }
            else if (Input.GetKeyUp("w") || Input.GetKeyUp("s")) { testGetKeyHMove = 0; isMove = false; }
>>>>>>> Gu_2
        }

        else if (testType == 3)
        {
            GetKeyJump = Input.GetKeyDown("4");


            if (Input.GetKeyDown("3")) { testGetKeyHMove = 1; isMove = true; }
            else if (Input.GetKeyDown("1")) { testGetKeyHMove = -1; isMove = true; }
            else if (Input.GetKeyUp("3") || Input.GetKeyUp("1")) { testGetKeyHMove = 0; isMove = false; }
        }

        else if (testType == 4)
        {
            GetKeyJump = Input.GetKeyDown("k");


            if (Input.GetKeyDown("l")) { testGetKeyHMove = 1; isMove = true; }
            else if (Input.GetKeyDown("j")) { testGetKeyHMove = -1; isMove = true; }
            else if (Input.GetKeyUp("l") || Input.GetKeyUp("j")) { testGetKeyHMove = 0; isMove = false; }
        }

        ResetRotation();

        canJump = playerFloorDetect.isOnFloor;

        if (canJump && isJump)
            isJump = false;

        playerStick.getIsOnFloor = playerFloorDetect.isOnFloor;
    }

    void FixedUpdate()
    {
        if (GetKeyJump && canJump)
        {
            isJump = true;
            Jump();
        }

        if (!playerStick.isStick &&!playerStick.isPop && isMove){
            SetFollowMove(false);
            Move();
        }
        else if(playerStick.isStick && isMove)
            Pull();
        else if(!isMove && isFollow){
            SetFollowMove(true);
        }

    }
    void Move()
    {
        // jellySprite.AddVelocity(new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag) * moveSpeed, 0.0f));
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

    public void Pop(Vector2 slop, float popForce)
    {
        // jellySprite.AddVelocity(slop * popForce, false);
        jellySprite.AddForce(slop * popForce * 30.0f);
        // jellySprite.AddForce(slop * popForce * 40.0f);
        // jellySprite.AddRelativeForce(slop * popForce * 40.0f);
        // jellySprite.MovePosition(slop * 2.0f);
    }

    void ResetRotation()
    {
        if (!jellySprite.notFreeze)
            jellySprite.FreezePlayerRot();

        if (jellySprite.CentralPoint.transform.rotation.z != 0)
        {
            if (isJump && jellySprite.CentralPoint.Body2D.velocity.y > 0.0f)
            {
                jellySprite.ResetSelfRot();
            }
        }
    }

    public void SetFollowMove(bool set)
    {
        FixedJoint2D f = jellySprite.CentralPoint.GameObject.GetComponent<FixedJoint2D>();
        if (set)
        {
            f.connectedBody = followTarget.GetComponent<Rigidbody2D>();
            f.enabled = true;
        }
        else
        {
            f.connectedBody = null;
            f.enabled = false;
            isFollow = false;
        }
    }
}

