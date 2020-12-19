using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloorDetect : MonoBehaviour
{
    public enum TOUCHTYPE
    {
        NONE = 0,
        PLAYER,
        GROUND,
        WALL,
        TITEM, 
        HITEM,
        ROTATEITEM,

        ROCKETITEM,
    }

    public int isOnFloor { get { return m_isOnFloor; } }

    [SerializeField]
    private int m_isOnFloor;
    public GameObject parents;

    void Start()
    {
    }

    void Update()
    {
        if (!GameManager.instance.isPause)
        {
            FollowGround();
        }
    }

    private void FollowGround()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 50.0f);
        float central=parents.GetComponent<UnityJellySprite>().CentralPoint.Body2D.rotation;
        if ((central < -45 && central > -150) || (central < 150 && central > 45))
        {
            GetComponent<BoxCollider2D>().offset = new Vector2(0, -1.2f);
        }
        else
            GetComponent<BoxCollider2D>().offset = new Vector2(0, -1);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "ground"){
             m_isOnFloor = (int)TOUCHTYPE.GROUND;
        }
        else if(other.gameObject.tag == "RotateItem"){
             m_isOnFloor = (int)TOUCHTYPE.ROTATEITEM;
        }
        else if(other.gameObject.tag == "TItem" && other.gameObject.GetComponent<Item>().isOnFloor){
            m_isOnFloor = (int)TOUCHTYPE.TITEM;
        }
        else if(other.gameObject.tag == "HItem" || other.gameObject.tag == "Rocket"){
            m_isOnFloor = (int)TOUCHTYPE.HITEM;
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("player") && other.gameObject.tag != parents.tag && other.gameObject.name == "stickDetect" && other.gameObject.GetComponentInParent<PlayerMovement>().playerFloorDetect.isOnFloor > 0){
            m_isOnFloor = (int)TOUCHTYPE.PLAYER;
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "ground"){
             m_isOnFloor =  (int)TOUCHTYPE.NONE;
        }
        else if(other.gameObject.tag == "RotateItem"){
             m_isOnFloor =  (int)TOUCHTYPE.NONE;
        }
        else if(other.gameObject.tag == "TItem"){
            m_isOnFloor =  (int)TOUCHTYPE.NONE;
        }
        else if(other.gameObject.tag == "HItem"){
            m_isOnFloor =  (int)TOUCHTYPE.NONE;
        }
        else if(other.gameObject.tag == "Rocket"){
            m_isOnFloor =  (int)TOUCHTYPE.NONE;
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("player") && other.gameObject.tag != parents.tag && other.gameObject.name == "stickDetect"){
            m_isOnFloor =  (int)TOUCHTYPE.NONE;
        }
    }
}
