using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTile : MoveTile
{

    protected Transform trans;
    protected Rigidbody2D rb;

    void Update()
    {
        if (canMove)
        {
            Move();
        }
    }

    void Move()
    {

        trans = this.gameObject.GetComponent<Transform>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();

        Vector3 pos = trans.position;

        if (trans.position.x < minVec.x || trans.position.y < minVec.y)
        {
            rb.MovePosition(minVec);
            moveSpeed = -moveSpeed;
            canMove = false;
            isMax = false;
            Invoke("SetCanMove", minStopSec);
        }

        else if (trans.position.x > maxVec.x || trans.position.y > maxVec.y)
        {
            rb.MovePosition(maxVec);
            moveSpeed = -moveSpeed;
            canMove = false;
            isMax = true;
            Invoke("SetCanMove", maxStopSec);
        }

        newPos = pos + moveSpeed;

        rb.MovePosition(newPos);
    }
}
