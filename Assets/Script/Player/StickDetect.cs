// #define JOYSTICK

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickDetect : MonoBehaviour
{
    InputSystem inputSystem;
    public bool isTouchWall;

    public bool isTouchPlayer;
    public List<GameObject> touchItemList = new List<GameObject>();  //碰到的物體

    //TEST
    public List<GameObject> playerList = new List<GameObject>();  //碰到的物體

    bool getKeyConfirm;

    bool detect;

    void Start()
    {
        inputSystem = GetComponentInParent<InputSystem>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        DetectBillboard(other.gameObject);

        DetectTouchWall(other.gameObject, true);

        DetectTouchItem(other.gameObject, true);

        DetectTouchPlayer(other.gameObject, true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        DetectTouchWall(other.gameObject, false);

        DetectTouchItem(other.gameObject, false);

        DetectTouchPlayer(other.gameObject, false);
    }

    void DetectBillboard(GameObject other)
    {
        if (other.tag == "Billboard")
        {
            if (inputSystem.getKeyConfirm && !other.GetComponent<Billboard>().isActive)
            {
                other.GetComponent<Billboard>().Show();
                getKeyConfirm = false;
            }
        }
    }

    void DetectTouchPlayer(GameObject other, bool trigger)
    {

        if (trigger)
        {
            if (other.tag != this.tag && other.layer == LayerMask.NameToLayer("player") && other.name == "stickDetect")
            {
                isTouchPlayer = true;
            }
        }
        else
        {
            if (other.tag != this.tag && other.layer == LayerMask.NameToLayer("player") && other.name == "stickDetect")
            {
                isTouchPlayer = false;
            }
        }
    }

    void DetectTouchItem(GameObject other, bool trigger)
    {
        if (trigger)
        {
            if (other.tag == "Item" && !touchItemList.Contains(other))
            {
                touchItemList.Add(other);
            }
        }
        else
        {
            if (touchItemList.Contains(other))
            {
                touchItemList.Remove(other);
            }
        }
    }

    void DetectTouchWall(GameObject other, bool trigger)
    {
        if (trigger)
        {
            if (other.tag == "wall")
            {
                isTouchWall = true;
            }
        }
        else
        {
            isTouchWall = false;
        }
    }

    public void SetDetect(bool set)
    {
        detect = set;
    }

    void ResetDetect()
    {
        isTouchWall = false;
        touchItemList.Clear();
        isTouchPlayer = false;
    }
}
