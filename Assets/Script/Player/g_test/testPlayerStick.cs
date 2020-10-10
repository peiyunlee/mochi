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

    [SerializeField]
    private bool isAttachHeavyItem;  //有碰到除了地板的HEAVYITEM

    public GameObject parents;
    private UnityJellySprite jellySprite;

    [SerializeField]
    private bool isAttachLightItem;  //有碰到light

    [SerializeField]
    private bool isAttachPlayer;  //有碰到角色

    [SerializeField]
    public List<GameObject> stickPlayerList;  //黏住的角色
    public enum DETECTTYPE
    {
        NONE,
        STICKHEAVY,
        STICKLIGHT
    }
    // public DETECTTYPE detectType{ get {return m_detectType;} } 
    DETECTTYPE m_detectType = DETECTTYPE.NONE;

    testPlayerMovement testPlayerMovement;
    void Start()
    {
        jellySprite = parents.GetComponent<UnityJellySprite>();
        testPlayerMovement = gameObject.GetComponentInParent<testPlayerMovement>();

    }
    void Update()
    {
        // Input.GetButtonDown("Stick_" + this.tag) || 
        // if ((Input.GetKeyDown("x") && testPlayerMovement.testType) || (Input.GetKeyDown("g") && !testPlayerMovement.testType))
        if (Input.GetButtonDown("Stick_" + this.tag))
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


        if (isStick)
        {
            // if (isAttachHeavyItem || getIsOnFloor)
            // {
            //     StickToItem();
            // }
            ItemToStick();
            PlayerToStick();
        }


        isAttachLightItem = jellySprite.GetIsLightAttach();
        isAttachPlayer = jellySprite.GetIsPlayerAttach();
        if (getIsOnFloor || isAttachHeavyItem || isAttachPlayer || isAttachLightItem)
        {
            canStick = true;
        }
        else if (!getIsOnFloor && !isAttachHeavyItem && isAttachLightItem && isAttachPlayer)
        {
            canStick = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.gameObject.tag != parents.tag)
        // {
        //     if (other.gameObject.layer == LayerMask.NameToLayer("light"))
        //     {
        //         attachLightItem.Add(other.gameObject);
        //     }
        // }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "wall" && !isAttachPlayer)  //?
        {
            isAttachHeavyItem = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != parents.tag)
        {
            if (other.gameObject.tag == "wall")
            {
                isAttachHeavyItem = false;
            }
        }
    }

    private void StickToItem()
    {
        jellySprite.SetPointsKinematic(true);
    }

    private void ResetNotStick_Normal()
    {
        isAttachHeavyItem = false;
        // jellySprite.SetPointsKinematic(false);
        jellySprite.SetItemStick(false);
        stickPlayerList = jellySprite.SetPlayerStick(false);
    }

    private void ItemToStick()
    {
        jellySprite.SetItemStick(true);
    }

    private void PlayerToStick()
    {
        stickPlayerList = jellySprite.SetPlayerStick(true);
    }

    //
    public void ResetNotStick_Pop()
    {
        isAttachHeavyItem = false;
        jellySprite.SetPointsKinematic(false);
    }
}
