using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloorDetect : MonoBehaviour
{

    public bool isOnFloor { get { return m_isOnFloor; } }

    [SerializeField]
    private bool m_isOnFloor;
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
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "ground" || other.gameObject.tag == "RotateItem"){
             m_isOnFloor = true;
        }
        else if(other.gameObject.tag == "TItem"){
            m_isOnFloor = other.gameObject.GetComponent<Item>().isOnFloor;
        }
        else if(other.gameObject.tag == "HItem" || other.gameObject.tag == "Rocket"){
            m_isOnFloor = true;
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("player") && other.gameObject.tag != parents.tag && other.gameObject.name == "stickDetect"){
            m_isOnFloor = other.gameObject.GetComponentInParent<PlayerMovement>().playerFloorDetect.isOnFloor;
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "ground"){
             m_isOnFloor = false;
        }
        else if(other.gameObject.tag == "TItem"){
            m_isOnFloor = false;
        }
        else if(other.gameObject.tag == "HItem"){
            m_isOnFloor = false;
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("player") && other.gameObject.tag != parents.tag && other.gameObject.name == "stickDetect"){
            m_isOnFloor = false;
        }
    }
}
