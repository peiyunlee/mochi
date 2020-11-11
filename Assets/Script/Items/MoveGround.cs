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


    void Start()
    {
        trans = this.gameObject.GetComponent<Transform>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        if(needMochi)
            canMove = false;
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
        }

        rb.MovePosition(pos + moveSpeed*Time.deltaTime);

    }

    void SetCanMove()
    {
        canMove = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // 判斷有沒有腳色黏上來
        if (needMochi)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("player"))
            {
                GameObject player = other.gameObject.GetComponent<JellySpriteReferencePoint>().ParentJellySprite.gameObject;
                if (player!=null && player.GetComponent<testPlayerStick>().stickItemList.Contains(this.gameObject))
                {
                    Invoke("SetCanMove", waitSec);
                    needMochi = false;
                }
            }

        }
    }
}

