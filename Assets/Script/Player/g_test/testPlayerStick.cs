using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerStick : MonoBehaviour
{

    // Use this for initialization
    [SerializeField]
    public bool isStick { get { return m_isStick; } }

    [SerializeField]
    private bool m_isStick;

    [SerializeField]
    private bool canStick;

    private int stickItemCount;

    public GameObject parents;
    private UnityJellySprite jellySprite;

    public List<GameObject> heavyItem;

    public List<GameObject> lightItem;
    public enum DETECTTYPE
    {
        NONE,
        STICKHEAVY,
        STICKLIGHT
    }
    // public DETECTTYPE detectType{ get {return m_detectType;} } 
    DETECTTYPE m_detectType = DETECTTYPE.NONE;

    void Awake()
    {
        jellySprite = parents.GetComponent<UnityJellySprite>();
        // playerFloorDetect = gameObject.GetComponentIn<testPlayerFloorDetect>();
    }
    void Start()
    {

    }
    void Update()
    {
        // Input.GetButtonDown("Stick_" + this.tag) || 
        if ((Input.GetKeyDown("x")) && canStick)
        {
            m_isStick = !m_isStick;

            if (!m_isStick)
            {
                ResetNotStick();
            }
            
            // if (item != null && m_isStick)
            // {
            //     stickItem = item;
            // }
            // else
            // {
            //     stickItem = null;
            // }

            // jellySprite.SetStick(m_isStick, stickItem);



            //Item.transform.parent=this.transform;
        }
        // else if (Input.GetButtonUp("Stick_" + this.tag))
        // {
        //     jellySprite.PullItem(true);
        // }

        if (m_isStick)
        {
            StickToItem();
            ItemToStick();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != parents.tag && other.gameObject.tag != "ground" && other.gameObject.tag != "wall")
        {
            canStick = true;
            stickItemCount++;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "ground" || other.gameObject.tag == "wall")
            canStick = true;


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != parents.tag && other.gameObject.tag != "ground" && other.gameObject.tag != "wall")
        {
            stickItemCount--;
            if (stickItemCount == 0)
            {
                canStick = false;
            }
        }
    }

    private void StickToItem()
    {
        jellySprite.SetPointsKinematic(true);
    }

    private void ResetNotStick()
    {
        jellySprite.SetPointsKinematic(false);
        jellySprite.SetItemStick(false);
    }

    private void ItemToStick()
    {
       jellySprite.SetItemStick(true);
    }
}
