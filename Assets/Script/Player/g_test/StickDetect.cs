using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickDetect : MonoBehaviour
{
    testPlayerStick playerStick;

    public List<GameObject> touchPlayerList = new List<GameObject>();  //碰到的角色
	public bool isTouchWall = false;
	public bool isTouchGround = false;
    public List<GameObject> touchItemList = new List<GameObject>();  //碰到的物體

    void Start()
    {
        playerStick = GetComponentInParent<testPlayerStick>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag != this.gameObject.tag)
        {
            if (other.gameObject.gameObject.layer == LayerMask.NameToLayer("player"))
            {
                if (other.gameObject.name == "stickDetect")
                {
                    GameObject touchPlayer = other.transform.parent.gameObject;
                    if (!touchPlayerList.Contains(touchPlayer))
                    {
                        touchPlayerList.Add(touchPlayer);
                        // Debug.Log("enter" + touchPlayer);
                    }
                }
            }
            else if (other.gameObject.tag == "wall")
            {
                Debug.Log("enter wall");
				isTouchWall = true;
            }
            else if (other.gameObject.tag == "ground")
            {
                Debug.Log("enter ground");
				isTouchGround = true;
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("thing"))
            {
                // Debug.Log("enter thing");
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != this.gameObject.tag)
        {
            if (other.gameObject.gameObject.layer == LayerMask.NameToLayer("player"))
            {
                if (other.gameObject.name == "stickDetect")
                {
                    GameObject touchPlayer = other.transform.parent.gameObject;
                    if (touchPlayerList != null && touchPlayerList.Contains(touchPlayer))
                    {
                        touchPlayerList.Remove(touchPlayer);
                        playerStick.ResetThePlayersNotStick(touchPlayer);
                        // Debug.Log("exit" + touchPlayer);
                    }
                }
            }
            else if (other.gameObject.tag == "wall")
            {
                Debug.Log("exit wall");
				isTouchWall = false;
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("thing"))
            {
                // Debug.Log("exit thing");
            }
        }
    }
}
