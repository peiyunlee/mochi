using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTile : MoveTile
{

    void Update()
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
            isMax = false;
            Invoke("SetCanMove", minStopSec);
        }

        else if (trans.position.x > maxVec.x || trans.position.y > maxVec.y)
        {
            moveSpeed = -moveSpeed;
            canMove = false;
            isMax = true;
            Invoke("SetCanMove", maxStopSec);
        }

        newPos = pos + moveSpeed;

        trans.position = newPos;
        rb.MovePosition(newPos);
    }
}
