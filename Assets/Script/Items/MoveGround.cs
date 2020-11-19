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

    Transform trans;
    Rigidbody2D rb;

    public GameObject tile;

    MoveTile moveTile;

    void Start()
    {
        trans = this.gameObject.GetComponent<Transform>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        if (needMochi)
            canMove = false;

        if (tile != null)
        {
            moveTile = tile.GetComponent<MoveTile>();
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            Move();
        }
    }

    void Move()
    {
        Vector3 pos = trans.position;

        if (trans.position.x < minVec.x || trans.position.y < minVec.y)
        {
            moveSpeed = -moveSpeed;
            canMove = false;
            Invoke("SetCanMove", minStopSec);
        }
        else if (trans.position.x > maxVec.x || trans.position.y > maxVec.y)
        {
            moveSpeed = -moveSpeed;
            canMove = false;
            Invoke("SetCanMove", maxStopSec);

            if (tile != null)
            {
                moveTile.SetStart();
            }
        }

        rb.MovePosition(pos + moveSpeed);

    }

    void SetCanMove()
    {
        canMove = true;
    }

    void OnTriggerEnter2D(Collider2D other)
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

