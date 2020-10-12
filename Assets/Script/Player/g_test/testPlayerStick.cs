using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerStick : MonoBehaviour
{
    public bool getIsOnFloor;
    // Use this for initialization
    [SerializeField]
    public bool isStick { get { return m_isStick; } }

    [SerializeField]
    private bool m_isStick;  //黏的狀態

    [SerializeField]
    private bool canStick;  //有碰到東西可以黏
    private UnityJellySprite jellySprite;

    [SerializeField]
    private bool isAttachItem;  //有碰到Item

    [SerializeField]
    public List<GameObject> stickItemList;  //黏住的角色

    [SerializeField]
    private bool isAttachPlayer;  //有碰到角色

    [SerializeField]
    public List<GameObject> stickPlayerList;  //黏住的角色

    [SerializeField]
    private bool isAttachWall;  //有碰到wall or floor
    public enum DETECTTYPE
    {
        NONE,
        STICKHEAVY,
        STICKLIGHT
    }
    // public DETECTTYPE detectType{ get {return m_detectType;} } 
    DETECTTYPE m_detectType = DETECTTYPE.NONE;

    testPlayerMovement testPlayerMovement;

    bool stopStick = true;

    void Start()
    {
        jellySprite = gameObject.GetComponent<UnityJellySprite>();
        testPlayerMovement = gameObject.GetComponent<testPlayerMovement>();

    }
    void Update()
    {
        // Input.GetButtonDown("Stick_" + this.tag)
        if ((Input.GetKeyDown("x") && testPlayerMovement.testType == 1) || (Input.GetKeyDown("g") && testPlayerMovement.testType == 2) || (Input.GetKeyDown("o") && testPlayerMovement.testType == 4) || (Input.GetKeyDown("5") && testPlayerMovement.testType == 3))
        {
            if (canStick && !m_isStick)
            {
                m_isStick = true;
            }
            else if (m_isStick)
            {
                ResetNotStick_Normal();
            }
        }


        if (m_isStick)
        {
            ItemToStick();
            PlayerToStick();
        }


        isAttachItem = jellySprite.GetIsItemAttach();
        isAttachPlayer = jellySprite.GetIsPlayerAttach();
        isAttachWall = jellySprite.GetIsFloorOrWallAttach();

        if (getIsOnFloor || isAttachWall || isAttachPlayer || isAttachItem)
        {
            canStick = true;
        }
        else if (!getIsOnFloor && !isAttachItem && !isAttachPlayer && !isAttachWall)
        {
            canStick = false;
        }
    }

    public void ResetNotStick_Normal()
    {
        m_isStick = false;

        ResetItemNotStick();

        ResetOtherPlayersNotStick();

        ResetFloorOrWallStick();
    }

    private void ItemToStick()
    {
        stickItemList = jellySprite.SetItemStick();

        jellySprite.SetFloorOrWallStick();
    }


    public void ResetItemNotStick()
    {
        stickItemList = null;
        jellySprite.ResetItemStick();
    }

    public void ResetFloorOrWallStick()
    {
        jellySprite.ResetFloorOrWallStick();
    }

    private void PlayerToStick()
    {
        stickPlayerList = jellySprite.SetPlayerStick();
    }


    public void ResetOtherPlayersNotStick()
    {
        stickPlayerList = null;
        jellySprite.ResetPlayerStick();
    }


    public void ResetThePlayersNotStick(List<GameObject> popPlayerList)
    {
        foreach (var player in popPlayerList)
        {
            stickPlayerList.Remove(player);
            jellySprite.ResetThePlayeStick(player);
        }
    }
}
