// #define JOYSTICK

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

    bool getKeyConfirm;

    void Start()
    {
        foreach (var player in playerList)
        {
            touchPlayerList.Add(player);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Billboard")
        {
#if JOYSTICK
            getKeyConfirm = Input.GetButtonDown("AButton_" + this.tag);
#else
            getKeyConfirm = Input.GetKeyDown("b");
#endif

            if (getKeyConfirm && !other.gameObject.GetComponent<Billboard>().isActive)
            {
                other.gameObject.GetComponent<Billboard>().Show();
                getKeyConfirm = false;
            }
        }

    }
}
