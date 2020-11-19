using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickDetect : MonoBehaviour
{

    public List<GameObject> touchPlayerList = new List<GameObject>();  //碰到的角色
    public bool isTouchWall = false;
    public List<GameObject> touchItemList = new List<GameObject>();  //碰到的物體

    //TEST
    public List<GameObject> playerList = new List<GameObject>();  //碰到的物體

    void Start()
    {
        foreach (var player in playerList)
        {
            touchPlayerList.Add(player);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag != this.gameObject.tag)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("player"))
            {
                if (other.gameObject.name == "stickDetect")
                {
                    GameObject touchPlayer = other.transform.parent.gameObject;
                    if (!touchPlayerList.Contains(touchPlayer))
                    {
                        touchPlayerList.Add(touchPlayer);
                    }
                }
            }
            else if (other.gameObject.tag == "wall")
            {
                isTouchWall = true;
            }
            
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != this.gameObject.tag)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("player"))
            {
                if (other.gameObject.name == "stickDetect")
                {
                    //     GameObject touchPlayer = other.transform.parent.gameObject;
                    //     if (touchPlayerList != null && touchPlayerList.Contains(touchPlayer))
                    //     {

                    //         // touchPlayerList.Remove(touchPlayer);
                    //         // playerStick.ResetThePlayersNotStick(touchPlayer);
                    //     }
                    // }
                }
            }
            else if (other.gameObject.tag == "wall")
            {
                isTouchWall = false;
            }
        }
    }
}
