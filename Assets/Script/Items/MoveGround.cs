using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveGround : MonoBehaviour
{
    public Vector3 maxVec, minVec;


    public float maxStopSec;
    public float minStopSec;

    public bool canMove;

    public bool needMochi;
    public Vector3 moveSpeed;

    public float waitSec;

    void Start()
    {
        if (needMochi)
            canMove = false;
    }

    public void SetCanMove()
    {
        canMove = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (needMochi)
        {
            MochiDetect(other);
        }
    }

    void MochiDetect(Collider2D other)
    {
        // 判斷有沒有腳色黏上來
        if (other.gameObject.layer == LayerMask.NameToLayer("player") && other.gameObject.name == "stickDetect")
        {
            GameObject player = other.gameObject.GetComponentInParent<PlayerMovement>().gameObject;
            if (player != null)
            {
                List<GameObject> stickItemList = player.GetComponent<PlayerStick>().stickItemList;
                if (stickItemList != null)
                {
                    if (stickItemList.Contains(this.gameObject))
                    {
                        Invoke("SetCanMove", waitSec);
                        needMochi = false;
                    }
                }
            }

        }
    }
}

