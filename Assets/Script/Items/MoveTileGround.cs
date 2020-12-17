using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTileGround : MoveGround
{


    public GameObject tile;

    MoveGroundTile moveTile;

    Transform trans;
    Rigidbody2D rb;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        trans = this.gameObject.GetComponent<Transform>();
        if (tile != null)
        {
            moveTile = tile.GetComponent<MoveGroundTile>();
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
            if (tile != null)
            {
                moveTile.SetCanMove();
            }
        }

        rb.MovePosition(pos + moveSpeed*Time.deltaTime);

    }
}
