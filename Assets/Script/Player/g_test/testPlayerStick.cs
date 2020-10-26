using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerStick : MonoBehaviour
{

    testPlayerMovement testPlayerMovement;
    UnityJellySprite jellySprite;
    public StickDetect stickDetect;

    //

    public bool getIsOnFloor;
    // Use this for initialization
    [SerializeField]
    public bool isStick { get { return m_isStick; } }

    [SerializeField]
    private bool m_isStick;  //黏的狀態

    [SerializeField]
    private bool canStick;  //有碰到東西可以黏


    [SerializeField]
    private bool isPointAttachItem;  //有碰到Item

    [SerializeField]
    public List<GameObject> stickItemList;  //黏住的角色

    [SerializeField]
    private List<GameObject> pointAttachPlayerList;  //有碰到角色

    [SerializeField]
    public List<GameObject> stickPlayerList = new List<GameObject>();  //黏住的角色

    [SerializeField]
    public List<GameObject> touchPlayerList;  //黏住的角色


    [SerializeField]
    private bool isTouchWall;  //有碰到Item

    [SerializeField]
    private bool isPointAttachWall;  //有碰到wall


    [SerializeField]
    private bool isTouchGround;  //有碰到Item

    [SerializeField]
    private bool isPointAttachGround;  //有碰到Ground

    //

    public bool isPopPlayer;

    //

    void Start()
    {
        jellySprite = gameObject.GetComponent<UnityJellySprite>();
        testPlayerMovement = gameObject.GetComponent<testPlayerMovement>();
        stickDetect = gameObject.GetComponentInChildren<StickDetect>();
        isPopPlayer = false;

    }
    void Update()
    {
        // Input.GetButtonDown("Stick_" + this.tag)
        if ((Input.GetKeyDown("x") && testPlayerMovement.testType == 1) || (Input.GetKeyDown("g") && testPlayerMovement.testType == 2) || (Input.GetKeyDown("o") && testPlayerMovement.testType == 4) || (Input.GetKeyDown("5") && testPlayerMovement.testType == 3))
        {
            if (canStick && !m_isStick)
            {
                // jellySprite.isStick = true;
                m_isStick = true;
            }
            else if (m_isStick)
            {
                ResetNotStick_Normal();
            }
        }

        isPointAttachItem = jellySprite.GetIsItemAttach();

        touchPlayerList = stickDetect.touchPlayerList;

        pointAttachPlayerList = jellySprite.GetPlayersAttach();

        isPointAttachWall = jellySprite.GetIsWallAttach();

        isTouchWall = stickDetect.isTouchWall;

        isPointAttachGround = jellySprite.GetIsFloorAttach();

        isTouchGround = stickDetect.isTouchGround;

        if ((getIsOnFloor) || (isPointAttachGround && isTouchGround) || (isPointAttachWall && isTouchWall) || (pointAttachPlayerList.Count != 0 && touchPlayerList.Count != 0) || isPointAttachItem)
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

        if(isTouchWall && isPointAttachWall)
            jellySprite.SetWallStick();
        
        if(getIsOnFloor || (isPointAttachGround && isTouchGround))
            jellySprite.SetFloorStick();
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
                if (touchPlayerList.Contains(pointAttachPlayer) && !stickPlayerList.Contains(pointAttachPlayer))
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
                // int index = stickPlayerList.IndexOf(player);
                jellySprite.ResetThePlayeStick(player);
                // stickPlayerList[index] = null;
                stickPlayerList.Remove(player);
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
