﻿// #define TEST_NOT_DIE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public AudioSource audio_Jump;
    public AudioSource audio_Death;

    //PlayerColor

    public string playerType;

    public int playerColor;


    //PlayerScript
    private UnityJellySprite jellySprite;
    PlayerStick playerStick;
    public PlayerFloorDetect playerFloorDetect;
    ColorDetect colorDetect;

    MultipleTargetCamera cam;

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
    public float airPullForce;
    public float pullForce;

    //jump

    [SerializeField]
    public float jumpSpeed;
    private bool canJump;
    [SerializeField]
    public bool isJump;

    //DEAD

    public int diePoint;

    public bool isDead;

    public bool isInvincible;

    void Start()
    {
        jellySprite = GetComponent<UnityJellySprite>();
        playerStick = gameObject.GetComponent<PlayerStick>();
        inputSystem = GetComponent<InputSystem>();
        playerFloorDetect = gameObject.GetComponentInChildren<PlayerFloorDetect>();
        colorDetect = gameObject.GetComponentInChildren<ColorDetect>();
        cam = GameObject.Find("Main Camera").GetComponent<MultipleTargetCamera>();
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
        isDead = false;
        diePoint = 0;

#if TEST_NOT_DIE
            isInvincible = true
#endif

    }

    void Update()
    {
        if (!GameManager.instance.isPause)
        {
            if (!isDead)
            {
                jellySprite.CountPointsDistance();

                JumpDetect();

                jellySprite.SetAnimBool("isJump", !canJump);

                playerRange();
            }
            else
            {
                DieReset();
            }
        }
    }

    void playerRange()
    {
        if (jellySprite.CentralPoint.transform.position.x < cam.min.x + 0.5f) jellySprite.SetPosition(new Vector2(cam.min.x + 0.5f, jellySprite.CentralPoint.Body2D.position.y), false);
        else if (jellySprite.CentralPoint.transform.position.x > cam.max.x - 0.5f) jellySprite.SetPosition(new Vector2(cam.max.x - 0.5f, jellySprite.CentralPoint.Body2D.position.y), false);

        // if (jellySprite.CentralPoint.transform.position.y < cam.min.y) jellySprite.SetPosition(new Vector2(jellySprite.CentralPoint.Body2D.position.x, cam.min.y), false);
        // else if (jellySprite.CentralPoint.transform.position.y > cam.max.y - 0.2f) jellySprite.SetPosition(new Vector2(jellySprite.CentralPoint.Body2D.position.x, cam.max.y - 0.2f), false);
    }

    void SetState()
    {
        if (inputSystem.GetKeyHMove > 0)
            walkCurState = State.Right;
        else if (inputSystem.GetKeyHMove < 0)
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
        if (!isDead && !GameManager.instance.isPause)
        {
            //**
            ResetRotation();
            //

            if (inputSystem.GetKeyJump && canJump)
            {
                Jump();
            }
            else if (!playerStick.isStick && !playerStick.isPoped && !jellySprite.isTurn)
                Move();
            else if (playerStick.isPoped && !jellySprite.isTurn)
                PopedForce();
            else if (playerStick.isStick && !playerStick.isPoped && !jellySprite.isTurn)
                StickForce();
            else if (jellySprite.isTurn && !playerStick.isPoped)
                TurnForce();

            SetState();
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

    void TurnForce()
    {
        if (inputSystem.GetKeyHMove == 0 && inputSystem.GetKeyVMove == 0)
        {
            jellySprite.AddVelocity(new Vector2(0.0f, 0.0f));
            jellySprite.CentralPoint.Body2D.freezeRotation = true;
        }

        else
        {
            jellySprite.CentralPoint.Body2D.freezeRotation = false;
            jellySprite.AddForce(new Vector2(inputSystem.GetKeyHMove * pullForce * 1.5f, inputSystem.GetKeyVMove * pullForce * 2.5f));
        }

    }

    void StickForce()
    {
        float force = pullForce;
        jellySprite.AddForce(new Vector2(inputSystem.GetKeyHMove * force * 1.5f, 0));
    }

    void PopedForce()
    {
        float force = airPullForce;
        jellySprite.AddForce(new Vector2(inputSystem.GetKeyHMove * force * 1.5f, 0));
    }

    void JumpDetect()
    {

        canJump = playerFloorDetect.isOnFloor > 0 && !playerStick.isStick;

        if (canJump && isJump && jellySprite.CentralPoint.Body2D.velocity.y < 0.0f)
            isJump = false;

        playerStick.getIsOnFloor = playerFloorDetect.isOnFloor;
    }
    void Jump()
    {
        audio_Jump.Play();
        canJump = false;
        isJump = true;
        jellySprite.AddVelocity(new Vector2(0, jumpSpeed), false);
        // jellySprite.AddForce(new Vector2(0, jumpSpeed));
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

        if (jellySprite.CentralPoint.transform.rotation.z != 0 && !jellySprite.notFreeze)
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
#if !TEST_NOT_DIE
            isDead = true;

            jellySprite.AddVelocity(new Vector2(0, 0));

            jellySprite.SetAnimBool("isDead", true);

            audio_Death.Play();

            Invoke("Die_Invincible", 0.05f);

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
        // newPos = new Vector2(-3.42f, -0.03f);
        newPos = LevelController.instance.diePointPos[diePoint].position;
        jellySprite.SetPosition(newPos, true);

        this.gameObject.SetActive(true);

        isDead = false;
        LevelController.instance.deadTotalCount++;
        LevelController.instance.CameraAddTarget(transform);

        jellySprite.SetAnimBool("isDead", false);

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
        playerStick.DieResetDetect();
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

