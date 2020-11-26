// #define JOYSTICK

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    public Camera cam;
    public float camX, camY;

    Vector2 currentVector;

    RaycastHit2D hit;

    enum State
    {
        Left,
        Right,
        Idle
    };
    State walkPreState;
    State walkCurState;
    bool playerLeft = true;
    public float moveSpeed;
    public float jumpSpeed;

    public float pullForce;

    [SerializeField]
    private bool canJump;
    [SerializeField]
    private bool isJump;
    private bool canMove;
    private UnityJellySprite jellySprite;
    private bool GetKeyJump;

#if !JOYSTICK
    private int testGetKeyHMove;
    private int testGetKeyVMove;
#else
    private float testGetKeyHMove;
    private float testGetKeyVMove;
#endif


    public int GetKey { get { return testGetKeyVMove; } }

    PlayerStick playerStick;

    PlayerFloorDetect playerFloorDetect;

    public int testType;

    public string playerType;

    bool notFreeze;

    ColorDetect colorDetect;

    void Start()
    {
        jellySprite = GetComponent<UnityJellySprite>();
        playerStick = gameObject.GetComponentInChildren<PlayerStick>();
        playerFloorDetect = gameObject.GetComponentInChildren<PlayerFloorDetect>();
        colorDetect = gameObject.GetComponentInChildren<ColorDetect>();
        walkPreState = State.Left;
        walkCurState = State.Left;

        switch (playerType)
        {
            case "red":
                colorDetect.playerColor = 8;
                break;
            case "blue":
                colorDetect.playerColor = 10;
                break;
            case "green":
                colorDetect.playerColor = 11;
                break;
            case "yellow":
                colorDetect.playerColor = 9;
                break;
            default:
                colorDetect.playerColor = 0;
                break;
        }

#if !JOYSTICK
        if (gameObject.tag == "player1")
            testType = 1;
        else if (gameObject.tag == "player2")
            testType = 2;
        else if (gameObject.tag == "player3")
            testType = 3;
        else testType = 4;
#endif

        cam = Camera.main;
    }

    void Update()
    {
#if !JOYSTICK
        if (testType == 1)
        {
            GetKeyJump = Input.GetKeyDown("z");

            if (Input.GetKeyDown("right")) testGetKeyHMove = 1;
            else if (Input.GetKeyDown("left")) testGetKeyHMove = -1;
            else if (Input.GetKeyUp("right") || Input.GetKeyUp("left")) testGetKeyHMove = 0;

            if (Input.GetKeyDown("up")) testGetKeyVMove = 1;
            else if (Input.GetKeyDown("down")) testGetKeyVMove = -1;
            else if (Input.GetKeyUp("up") || Input.GetKeyUp("down")) testGetKeyVMove = 0;
        }
        else if (testType == 2)
        {
            GetKeyJump = Input.GetKeyDown("f");

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
#else
        GetKeyJump = Input.GetButtonDown("Jump_" + this.tag);
        testGetKeyHMove = Input.GetAxisRaw("Horizontal_" + this.tag);
        testGetKeyVMove = Input.GetAxisRaw("Vertical_" + this.tag);
#endif

        if (Input.GetKeyDown("t")) Die();

        ResetRotation();

        canJump = playerFloorDetect.isOnFloor;

        if (canJump && isJump)
            isJump = false;

        jellySprite.SetAnimBool("isJump", !canJump);

        playerStick.getIsOnFloor = playerFloorDetect.isOnFloor;
    }

    void SetState()
    {
        if (testGetKeyHMove == 1)
            walkCurState = State.Right;
        else if (testGetKeyHMove == -1)
            walkCurState = State.Left;

        if (walkCurState != walkPreState)
        {
            playerLeft = !playerLeft;
            jellySprite.SetFlipHorizontal(!playerLeft);
            walkPreState = walkCurState;
        }
    }

    void FixedUpdate()
    {
        if (GetKeyJump && canJump)
        {
            Jump();
        }

        if (!playerStick.isStick && !playerStick.isPop)
            Move();
        else
            Pull();

    }
    void Move()
    {
        SetState();

        if (testGetKeyHMove != 0 && canJump)
            jellySprite.SetAnimBool("isWalk", true);
        else
            jellySprite.SetAnimBool("isWalk", false);

        jellySprite.AddVelocity(new Vector2(testGetKeyHMove * moveSpeed, 0.0f));
    }
    void Pull()
    {
        jellySprite.AddForce(new Vector2(testGetKeyHMove * pullForce, testGetKeyVMove * pullForce));
    }

    void Jump()
    {
        canJump = false;
        isJump = true;
        jellySprite.AddForce(new Vector2(0, jumpSpeed));
    }

    public void Pop(Vector2 slop, float popForce)
    {
        jellySprite.AddForce(slop * popForce * 30.0f);
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

    public void Die()
    {
        this.gameObject.SetActive(false);
        Vector2 newPos;
		newPos = cam.ScreenToWorldPoint(new Vector3(camX, camY, cam.nearClipPlane));
		jellySprite.SetPosition(DetectPoint(newPos), true);
        this.gameObject.SetActive(true);
    }

    
    Vector2 DetectPoint(Vector2 point)
    {
        bool result = false;
        Vector3 slop = new Vector3(point.x, point.y, -10) - new Vector3(point.x, point.y, 10);
        hit = Physics2D.Raycast(point, slop);
        result = hit.collider;
        Debug.Log(hit.collider);
        Collider2D hc = hit.collider;

        if (result)
        {
            currentVector = point;
            return DetectPoint(point + Vector2.up);
        }
        else
        {
            if (point == currentVector + Vector2.right)
            {
                currentVector = Vector2.zero;
                return point;
            }
            else 
            {
                currentVector = point;
                return DetectPoint(point + Vector2.right);
            }
        }

    }
}

