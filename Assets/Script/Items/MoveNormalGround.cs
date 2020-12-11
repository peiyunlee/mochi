using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNormalGround : MoveGround
{


    Transform trans;
    Rigidbody2D rb;


    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        trans = this.gameObject.GetComponent<Transform>();

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

        rb.MovePosition(pos + moveSpeed);

    }
}
