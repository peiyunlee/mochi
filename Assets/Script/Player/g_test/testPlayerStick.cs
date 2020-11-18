using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerStick : MonoBehaviour
{

    testPlayerMovement testPlayerMovement;
    UnityJellySprite jellySprite;

    [SerializeField]
    StickDetect stickDetect;

    //

    public bool getIsOnFloor;
    // Use this for initialization
    public bool isStick { get { return m_isStick; } }

    [SerializeField]
    bool m_isStick;  //黏的狀態

    [SerializeField]
    bool canStick;  //有碰到東西可以黏


    [SerializeField]
    bool isPointAttachItem;  //有碰到Item

    public List<GameObject> stickItemList;  //黏住的角色

    [SerializeField]
    List<GameObject> pointAttachPlayerList;  //有碰到角色

    public List<GameObject> stickPlayerList = new List<GameObject>();  //黏住的角色

    [SerializeField]
    List<GameObject> touchPlayerList;  //黏住的角色


    [SerializeField]
    bool isTouchWall;  //有碰到Item

    public bool isPointAttachWall;  //有碰到wall


    [SerializeField]
    bool isTouchGround;  //有碰到Item
    bool isPointAttachGround;  //有碰到Ground

    //
    [SerializeField]
    public bool isPop;  //有碰到Ground
    //

    void Start()
    {
        jellySprite = gameObject.GetComponent<UnityJellySprite>();
        testPlayerMovement = gameObject.GetComponent<testPlayerMovement>();
        stickDetect = gameObject.GetComponentInChildren<StickDetect>();
        isPop = false;
    }
    void Update()
    {
        // Input.GetButtonDown("Stick_" + this.tag)
        if ((Input.GetKeyDown("x") && testPlayerMovement.testType == 1) || (Input.GetKeyDown("g") && testPlayerMovement.testType == 2) || (Input.GetKeyDown("o") && testPlayerMovement.testType == 4) || (Input.GetKeyDown("5") && testPlayerMovement.testType == 3))
        {
            if (canStick && !m_isStick)
            {
                m_isStick = true;
                jellySprite.SetAnimBool("isWalk", false);
            }
            else if (m_isStick)
            {
                ResetNotStick_Normal();
            }
        }

        jellySprite.SetAnimBool("isStick",m_isStick);

        //hold住黏
        // if ((Input.GetKeyDown("x") && testPlayerMovement.testType == 1) || (Input.GetKeyDown("g") && testPlayerMovement.testType == 2) || (Input.GetKeyDown("o") && testPlayerMovement.testType == 4) || (Input.GetKeyDown("5") && testPlayerMovement.testType == 3))
        // {
        //     if (canStick && !m_isStick)
        //     {
        //         m_isStick = true;
        //     }
        // }
        // if ((Input.GetKeyUp("x") && testPlayerMovement.testType == 1) || (Input.GetKeyUp("g") && testPlayerMovement.testType == 2) || (Input.GetKeyUp("o") && testPlayerMovement.testType == 4) || (Input.GetKeyUp("5") && testPlayerMovement.testType == 3))
        // {
        //     if (m_isStick)
        //     {
        //         ResetNotStick_Normal();
        //     }
        // }
        //hold住黏

        isPointAttachItem = jellySprite.GetIsItemAttach();

        touchPlayerList = stickDetect.touchPlayerList;

        pointAttachPlayerList = jellySprite.GetPlayersAttach();

        isPointAttachWall = jellySprite.GetIsWallAttach();

        isTouchWall = stickDetect.isTouchWall;

        isPointAttachGround = jellySprite.GetIsFloorAttach();

        if (isTouchWall || isPointAttachGround)
        {
            if (isPop)
            {
                isPop = false;
            }

        }

        if (((getIsOnFloor || isPointAttachGround) || (isPointAttachWall && isTouchWall) || (pointAttachPlayerList.Count != 0 && touchPlayerList.Count != 0) || isPointAttachItem))
        {
            canStick = true;
        }
        else
        {
            canStick = false;
        }

        if (m_isStick)
        {
            ItemToStick();
            PlayerToStick();
        }
    }

    private void ItemToStick()
    {
        stickItemList = jellySprite.SetItemStick();

        if (isTouchWall && isPointAttachWall)
            jellySprite.SetWallStick();

        if (getIsOnFloor || isPointAttachGround)
        {
            jellySprite.SetFloorStick();
        }
    }


    public void ResetItemNotStick()
    {
        stickItemList = null;
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
        if (touchPlayerList != null && pointAttachPlayerList != null)
        {
            foreach (var pointAttachPlayer in pointAttachPlayerList)
            {
                if (touchPlayerList.Contains(pointAttachPlayer) && !stickPlayerList.Contains(pointAttachPlayer) && !pointAttachPlayer.GetComponent<testPlayerStick>().isPop)
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
        m_isStick = false;

        ResetItemNotStick();

        ResetPlayersNotStick();

        ResetFloorStick();

        ResetWallStick();
    }

    public void ResetNotStick_PopWithPlayer()
    {
        m_isStick = false;

        ResetItemNotStick();

        ResetPlayersNotStick();
    }
}
