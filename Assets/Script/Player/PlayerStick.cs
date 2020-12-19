
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStick : MonoBehaviour
{
    public AudioSource audio_Stick;
    PlayerMovement playerMovement;
    UnityJellySprite jellySprite;
    public StickDetect stickDetect;

    //

    public int getIsOnFloor;
    // Use this for initialization

    [SerializeField]
    public bool isStick;  //黏的狀態

    [SerializeField]
    public bool canStick;  //有碰到東西可以黏

    //item

    [SerializeField]
    public List<GameObject> stickItemList;
    public List<GameObject> touchItemList;
    public bool isTouchItem;

    //player
    public bool isTouchPlayer;

    public GameObject touchPlayer;

    [SerializeField]
    private List<GameObject> pointAttachPlayerList;  //有碰到角色

    [SerializeField]
    public List<GameObject> stickPlayerList = new List<GameObject>();  //我黏住的角色
    [SerializeField]
    public List<GameObject> isStickedPlayerList = new List<GameObject>();  //黏住我的角色

    // [SerializeField]
    // public List<GameObject> touchPlayerList;  //黏住的角色

    //wall

    [SerializeField]
    public bool isTouchWall;

    [SerializeField]
    public bool isAttachWall;

    [SerializeField]
    bool isStickWall;

    //ground

    [SerializeField]
    public bool isTouchGround;

    [SerializeField]
    bool isStickGround;


    //rocket

    [SerializeField]
    bool isStickRocket;

    //
    [SerializeField]
    public bool isPoped;
    //

    void Start()
    {
        jellySprite = gameObject.GetComponent<UnityJellySprite>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        stickDetect = gameObject.GetComponentInChildren<StickDetect>();
        isPoped = false;
        jellySprite.notFreeze = false;
    }

    void Update()
    {
        if (!GameManager.instance.isPause)
        {
            if (!playerMovement.isDead)
            {
                AttachDetect();

                if (isAttachWall || isTouchGround)
                {
                    if (isPoped)
                    {
                        isPoped = false;
                    }
                }

                jellySprite.SetAnimBool("isStick", isStick);

                CanStick();

                if (isStick)
                {
                    ItemToStick();
                    PlayerToStick();
                }
            }
            else
            {
                DieReset();
            }
        }
    }

    public void SetStick()
    {
        stickDetect.SetDetect(true);
        audio_Stick.Play();
        isStick = true;
        jellySprite.SetAnimBool("isWalk", false);
    }

    public void ResetStick()
    {
        ResetNotStick_Normal();
    }

    void DieReset()
    {
        ResetNotStick_Normal();
    }

    private void ItemToStick()
    {
        stickItemList = jellySprite.SetItemStick(touchItemList);

        isStickRocket = jellySprite.GetIsRocketAttach();

        if (isStickRocket)
            RocketStick();

        if (isAttachWall && !jellySprite.isTurn)
            isStickWall = jellySprite.SetWallStick();

        if (isTouchGround && !jellySprite.isTurn)
            isStickGround = jellySprite.SetFloorStick();
    }


    public void ResetItemNotStick()
    {
        stickItemList.Clear();
        jellySprite.ResetItemStick();
    }

    public void ResetFloorStick()
    {
        jellySprite.ResetFloorStick();
    }

    public void ResetWallStick()
    {
        jellySprite.ResetWallStick();
    }

    private void PlayerToStick()
    {
        if (pointAttachPlayerList != null)
        {
            foreach (var pointAttachPlayer in pointAttachPlayerList)
            {
                if (!stickPlayerList.Contains(pointAttachPlayer) && !pointAttachPlayer.GetComponent<PlayerStick>().isPoped)
                {
                    stickPlayerList.Add(pointAttachPlayer);
                }
            }

            if (stickPlayerList != null)
                jellySprite.SetPlayerStick(stickPlayerList);
        }
    }

    public void ResetPlayersNotStick()
    {
        jellySprite.ResetPlayersStick(stickPlayerList);
        stickPlayerList.Clear();
    }

    public void ResetThePlayersNotStick(List<GameObject> popPlayerList)
    {
        foreach (var player in popPlayerList)
        {
            stickPlayerList.Remove(player);
            jellySprite.ResetThePlayeStick(player);
        }
    }

    public void ResetThePlayersNotStick(GameObject player)
    {
        if (player != null && stickPlayerList != null)
        {
            if (stickPlayerList.Contains(player))
            {
                stickPlayerList.Remove(player);
                jellySprite.ResetThePlayeStick(player);
            }
        }
    }

    public void ResetNotStick_Normal()
    {
        isStick = false;

        isStickGround = false;

        isStickWall = false;

        ResetItemNotStick();

        ResetPlayersNotStick();

        ResetFloorStick();

        ResetWallStick();

        stickDetect.SetDetect(false);

        stickDetect.ResetDetect();
    }

    public void ResetNotStick_PopWithPlayer()
    {
        isStick = false;

        isStickGround = false;

        isStickWall = false;

        ResetItemNotStick();

        ResetPlayersNotStick();
    }

    void RocketStick()
    {
        LevelController.instance.SetRocketStick(this.gameObject, isStickRocket, playerMovement.playerColor);
    }

    void AttachDetect()
    {
        pointAttachPlayerList = jellySprite.GetPlayersAttach();

        isAttachWall = jellySprite.GetIsWallAttach();
    }

    void CanStick()
    {
        if ((isTouchGround || isAttachWall || pointAttachPlayerList.Count != 0 || (touchItemList.Count != 0)))
        {
            canStick = true;
        }
        else
        {
            canStick = false;
        }
    }

    public void DieResetDetect(){
        stickDetect.DieResetDetect();
    }
}
