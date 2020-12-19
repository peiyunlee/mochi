using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickDetect : MonoBehaviour
{
    InputSystem inputSystem;
    PlayerStick playerStick;

    bool getKeyConfirm;

    bool detect;

    void Start()
    {
        inputSystem = GetComponentInParent<InputSystem>();
        playerStick = GetComponentInParent<PlayerStick>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        DetectBillboard(other.gameObject);

        DetectTouchGround(other.gameObject, true);

        DetectTouchWall(other.gameObject, true);

        DetectTouchItem(other.gameObject, true);

        DetectTouchPlayer(other.gameObject, true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        DetectTouchWall(other.gameObject, false);

        DetectTouchGround(other.gameObject, false);

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
        if (other.tag != this.tag && other.layer == LayerMask.NameToLayer("player") && other.name == "stickDetect")
        {
            playerStick.isTouchPlayer = trigger;
            if (trigger)
                playerStick.touchPlayer = other.GetComponentInParent<PlayerMovement>().gameObject;
            else
                playerStick.touchPlayer = null;
        }
    }

    void DetectTouchItem(GameObject other, bool trigger)
    {
        if (other.tag == "TItem" || other.tag == "HItem" || other.tag == "RotateItem" || other.tag == "Rocket"){
            playerStick.isTouchItem = trigger;
            if (trigger && !playerStick.touchItemList.Contains(other))
            {
                playerStick.touchItemList.Add(other);
            }
            else if (!trigger && playerStick.touchItemList.Contains(other))
            {
                playerStick.touchItemList.Remove(other);
            }
        }
    }

    void DetectTouchWall(GameObject other, bool trigger)
    {
        if (other.tag == "wall")
        {
            playerStick.isTouchWall = trigger;
        }
    }

    void DetectTouchGround(GameObject other, bool trigger)
    {
        if (other.tag == "ground")
        {
            playerStick.isTouchGround = trigger;
        }
    }

    public void SetDetect(bool set)
    {
        detect = set;
    }

    public void ResetDetect()
    {
        playerStick.isTouchWall = false;
        playerStick.touchItemList.Clear();
        playerStick.isTouchPlayer = false;
    }

    public void DieResetDetect(){
        playerStick.isTouchWall = false;
        playerStick.touchItemList.Clear();
        playerStick.isTouchPlayer = false;
        playerStick.isTouchGround=false;
    }
}
