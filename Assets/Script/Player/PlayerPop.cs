using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPop : MonoBehaviour
{
    public AudioSource audio_Pop;
    public float popForce;
    StickDetect stickDetect;
    PlayerStick playerStick;
    PlayerMovement playerMovement;
    private UnityJellySprite jellySprite;
    private InputSystem inputSystem;

    public bool canPop;    //可以彈的情況：按黏才可以彈

    float popTime;

    public bool getKeyPop;

    public bool TimesUp;
    public bool canTurn;

    void Start()
    {
        playerStick = gameObject.GetComponent<PlayerStick>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        jellySprite = gameObject.GetComponent<UnityJellySprite>();
        stickDetect = gameObject.GetComponentInChildren<StickDetect>();
        canPop = false;
        TimesUp = false;
    }

    void Update()
    {
        if (!GameManager.instance.isPause)
        {
            if (!playerMovement.isDead)
            {
                canPop = playerStick.isStick && (playerStick.getIsOnFloor > 1 || playerStick.isAttachWall || playerStick.isTouchGround) && (playerStick.stickPlayerList.Count != 0 || playerStick.stickItemList.Count != 0);
            }
            else
            {
                DieReset();
            }

        }
    }

    public void PopDown()
    {
        if (canPop) //加在inputSystem裡沒有效(可以在空中連續彈)，可能是時間差導致
        {
            //讓對方轉
            canTurn = true;
            jellySprite.SetAnimBool("isPop", true);
            jellySprite.SetPlayerRot(playerStick.stickPlayerList);
        }
    }

    public void PopUp()
    {
        if (canPop)
        {
            canTurn = false;
            getKeyPop = true;
        }
    }

    void FixedUpdate()
    {
        if (!playerMovement.isDead)
        {
            //**
            Turn();
            //
            if (getKeyPop || TimesUp)
            {
                canTurn = false;
                // jellySprite.isTurn = false;
                TimesUp = false;
                getKeyPop = false;
                jellySprite.ResetPlayerRot(playerStick.stickPlayerList);
                jellySprite.SetAnimBool("isPop", false);
                Pop();
                audio_Pop.Play();
            }
        }
    }

    void DieReset()
    {
        canPop = false;

        canTurn = false;
        jellySprite.isTurn = false;
        jellySprite.ResetPlayerRot(playerStick.stickPlayerList);

        jellySprite.SetAnimBool("isPop", false);
        getKeyPop = false;
    }

    void Turn()
    {
        if (canTurn)
        {
            // jellySprite.isTurn = true;
            //按彈時unfreeze對方的rotation
            if (playerStick.stickPlayerList != null && playerStick.stickPlayerList.Count > 0)
            {
                List<GameObject> stickPlayerList = playerStick.stickPlayerList;
                foreach (var player in stickPlayerList)
                {
                    player.GetComponent<UnityJellySprite>().CentralPoint.Body2D.freezeRotation = false;
                    player.GetComponent<PlayerStick>().ResetFloorStick();
                    player.GetComponent<PlayerStick>().ResetWallStick();
                }
                jellySprite.CentralPoint.GameObject.GetComponent<HingeJoint2D>().enabled = false;

            }

        }
        else if (playerStick.isStick && !canTurn)
        {
            //黏住的時候&&沒有按彈時freeze對方的rotation
            if (playerStick.stickPlayerList != null && playerStick.stickPlayerList.Count > 0)
            {
                List<GameObject> stickPlayerList = playerStick.stickPlayerList;
                foreach (var player in stickPlayerList)
                {
                    player.GetComponent<UnityJellySprite>().CentralPoint.Body2D.freezeRotation = true;
                }
            }
        }
    }

    void Pop()
    {
        if (playerStick.stickItemList != null && playerStick.stickItemList.Count > 0)
        {
            List<GameObject> stickItemList = playerStick.stickItemList;
            playerStick.ResetItemNotStick();
            foreach (var item in stickItemList)
            {
                if (item.tag != "ground" && item.tag != "wall")
                {
                    PopItem(item);
                }
            }
        }

        if (playerStick.stickPlayerList != null)
        {
            List<GameObject> stickPlayerList = playerStick.stickPlayerList;
            List<GameObject> popPlayerList = new List<GameObject>();
            List<GameObject> withPlayerList = new List<GameObject>();

            foreach (var player in stickPlayerList)
            {
                bool isStick = player.GetComponent<PlayerStick>().isStick;
                if (!isStick) //可以單獨彈出去的player
                {
                    popPlayerList.Add(player);
                }
                else if (isStick)
                {
                    withPlayerList.Add(player);
                }
            }

            if (popPlayerList.Count > 0)
            {
                foreach (var player in popPlayerList)
                {
                    playerStick.ResetThePlayersNotStick(player);
                    PopPlayer(player);
                }
            }

            if (withPlayerList.Count > 0)
            {
                //重設黏
                playerStick.ResetFloorStick();
                playerStick.ResetWallStick();
                // Vector2 slop = Vector2.zero;
                foreach (var player in withPlayerList)
                {
                    // Vector2 slop_p = player.transform.position - this.gameObject.transform.position;
                    // slop += slop_p;
                    // slop = slop / Mathf.Sqrt(Mathf.Pow(slop.x, 2) + Mathf.Pow(slop.y, 2));
                    PopWithPlayer(player);
                }

                //重設黏
                // playerStick.ResetNotStick_PopWithPlayer();
            }
        }

    }

    void PopItem(GameObject item)
    {
        Vector2 slop = item.transform.position - this.gameObject.transform.position;
        slop = slop / Mathf.Sqrt(Mathf.Pow(slop.x, 2) + Mathf.Pow(slop.y, 2));
        item.GetComponent<Rigidbody2D>().velocity = slop * popForce;
    }

    void PopPlayer(GameObject player)
    {
        player.GetComponent<PlayerStick>().isPoped = true;
        player.GetComponent<PlayerPop>().InvokeResetIsPoped();
        Vector2 slop = player.gameObject.transform.position - this.gameObject.transform.position;
        slop = slop / Mathf.Sqrt(Mathf.Pow(slop.x, 2) + Mathf.Pow(slop.y, 2));

        player.GetComponent<PlayerMovement>().Pop(slop, popForce);
    }

    void PopWithPlayer(GameObject player)
    {
        playerStick.isPoped = true;
        InvokeResetIsPoped();
        player.GetComponent<PlayerStick>().isPoped = true;
        player.GetComponent<PlayerPop>().InvokeResetIsPoped();

        Vector2 slop = player.gameObject.transform.position - this.gameObject.transform.position;
        slop = slop / Mathf.Sqrt(Mathf.Pow(slop.x, 2) + Mathf.Pow(slop.y, 2));

        player.GetComponent<PlayerMovement>().Pop(slop, popForce * 0.9f);
        playerMovement.Pop(slop, popForce * 0.9f);
    }

    public void InvokeResetIsPoped()
    {
        Invoke("ResetIsPoped", 3f);
    }

    void ResetIsPoped()
    {
        playerStick.isPoped = false;
    }
}