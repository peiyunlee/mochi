#define TEST_DIE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    //PlayerColor

    public string playerType;

    public int playerColor;


    //PlayerScript
    private UnityJellySprite jellySprite;
    PlayerStick playerStick;
    PlayerFloorDetect playerFloorDetect;
    ColorDetect colorDetect;

    InputSystem inputSystem;

    //anim
    enum State
    {
        Left,
        Right,
        Idle
    };
    State walkPreState;
    State walkCurState;
    bool playerLeft = true;

    //move or pull
    public float moveSpeed;
    private bool canMove;

    public float pullForce;

    //jump

    [SerializeField]
    public float jumpSpeed;
    private bool canJump;
    [SerializeField]
    private bool isJump;

    //DEAD

    public bool isDead;

    public bool isInvincible;

    void Start()
    {
        jellySprite = GetComponent<UnityJellySprite>();
        playerStick = gameObject.GetComponent<PlayerStick>();
        inputSystem = GetComponent<InputSystem>();
        playerFloorDetect = gameObject.GetComponentInChildren<PlayerFloorDetect>();
        colorDetect = gameObject.GetComponentInChildren<ColorDetect>();
        walkPreState = State.Left;
        walkCurState = State.Left;

        switch (playerType)
        {
            case "red":
                colorDetect.playerColor = 8;
                playerColor = 8;
                break;
            case "blue":
                colorDetect.playerColor = 9;
                playerColor = 9;
                break;
            case "green":
                colorDetect.playerColor = 11;
                playerColor = 11;
                break;
            case "yellow":
                colorDetect.playerColor = 10;
                playerColor = 10;
                break;
            default:
                colorDetect.playerColor = 0;
                playerColor = 0;
                break;
        }

#if TEST_DIE
        isDead = false;
#endif

    }

    void Update()
    {
        if (!GameManager.instance.isPause)
        {
            if (!isDead)
            {
                //**
                ResetRotation();
                //

                JumpDetect();

                jellySprite.SetAnimBool("isJump", !canJump);

                SetState();
            }
            else
            {
                DieReset();
            }
        }
    }

    void SetState()
    {
        if (inputSystem.GetKeyHMove == 1)
            walkCurState = State.Right;
        else if (inputSystem.GetKeyHMove == -1)
            walkCurState = State.Left;

        if (walkCurState != walkPreState)
        {
            playerLeft = !playerLeft;
            jellySprite.SetFlipHorizontal(!playerLeft);
            walkPreState = walkCurState;
        }
    }

    void DieReset()
    {
        canJump = false;

        inputSystem.GetKeyJump = false;
        inputSystem.GetKeyHMove = 0;
        inputSystem.GetKeyVMove = 0;
    }
    void FixedUpdate()
    {
        if (!isDead)
        {
            if (inputSystem.GetKeyJump && canJump)
            {
                Jump();
            }

            if (!playerStick.isStick && !playerStick.isPop)
                Move();
            else
                Pull();
        }

    }

    void Move()
    {

        if (inputSystem.GetKeyHMove != 0 && canJump)
            jellySprite.SetAnimBool("isWalk", true);
        else
            jellySprite.SetAnimBool("isWalk", false);

        jellySprite.AddVelocity(new Vector2(inputSystem.GetKeyHMove * moveSpeed, 0.0f));

    }

    void Pull()
    {
        jellySprite.AddForce(new Vector2(inputSystem.GetKeyHMove * pullForce * 1.5f, inputSystem.GetKeyVMove * pullForce * 2.0f));
    }

    void JumpDetect()
    {
        canJump = playerFloorDetect.isOnFloor;

        if (canJump && isJump && jellySprite.CentralPoint.Body2D.velocity.y < 0.0f)
            isJump = false;

        playerStick.getIsOnFloor = playerFloorDetect.isOnFloor;
    }
    void Jump()
    {
        canJump = false;
        isJump = true;
        jellySprite.AddForce(new Vector2(0, jumpSpeed));
    }

    public void Pop(Vector2 slop, float popForce)
    {
        jellySprite.AddVelocity(new Vector2(0, 0));
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
        if (!isDead)
        {
#if TEST_DIE
            isDead = true;

            Invoke("Die_Invincible", 0.05f);

            jellySprite.SetAnimBool("isDead", isDead);

            Invoke("Rebirth", 2);
#endif
        }
    }

    void Rebirth()
    {
        this.gameObject.SetActive(false);

        PlayerDeadReset();

        Vector2 newPos;
        // newPos = cam.ScreenToWorldPoint(new Vector3(camX, camY, cam.nearClipPlane));
        // jellySprite.SetPosition(DetectPoint_Ground(newPos), true);
        newPos = new Vector2(-3.42f, -0.03f);
        jellySprite.SetPosition(newPos, true);

        this.gameObject.SetActive(true);

        isDead = false;

        this.gameObject.SetActive(true);

        Die_Invincible(); //無敵狀態

        Invoke("Die_Not_Invincible", 0.5f);  //無敵狀態0.5秒再取消
    }
    void PlayerDeadReset()
    {
        if (playerStick.stickPlayerList != null)
        {
            foreach (var stickplayer in playerStick.stickPlayerList)
            {
                stickplayer.GetComponent<UnityJellySprite>().ResetPoint();
            }
            playerStick.ResetNotStick_Normal();
        }
        if (playerStick.isStickedPlayerList != null)
        {
            foreach (var isStickedPlayer in playerStick.isStickedPlayerList)
            {
                isStickedPlayer.GetComponent<PlayerStick>().ResetThePlayersNotStick(this.gameObject);
            }
            playerStick.isStickedPlayerList.Clear();
        }

        jellySprite.ResetPoint();
        jellySprite.CentralPoint.GameObject.GetComponent<HingeJoint2D>().connectedBody = null;
        jellySprite.ResetSelfRot();
    }

    void Die_Invincible()
    {
        isInvincible = true;
    }

    void Die_Not_Invincible()
    {
        isInvincible = false;
    }




}

