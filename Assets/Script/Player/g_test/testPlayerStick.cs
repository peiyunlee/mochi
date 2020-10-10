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
        if ((Input.GetKeyDown("x") && testPlayerMovement.testType) || (Input.GetKeyDown("g") && !testPlayerMovement.testType))
        {
            if (canStick && !m_isStick)
            {
                m_isStick = true;
            }
            else if (m_isStick)
            {
                m_isStick = false;
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

    private void ResetNotStick_Normal()
    {
        ResetItemNotStick();

        stickPlayerList = null;
        jellySprite.ResetPlayerStick();

        jellySprite.ResetFloorOrWallStick();
    }

    private void ItemToStick()
    {
        stickItemList = jellySprite.SetItemStick();

        jellySprite.SetFloorOrWallStick();
    }

    private void PlayerToStick()
    {
        stickPlayerList = jellySprite.SetPlayerStick();
    }


    public void ResetItemNotStick()
    {
        stickItemList = null;
        jellySprite.ResetItemStick();
    }
}
