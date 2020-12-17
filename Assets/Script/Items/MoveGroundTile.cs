using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGroundTile : MoveTile
{

    public GameObject ground;

    MoveTileGround moveGround;

    protected Transform trans;
    protected Rigidbody2D rb;

	void Start()
	{
        
        trans = this.gameObject.GetComponent<Transform>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();

		moveGround = ground.GetComponent<MoveTileGround>();
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
            isMax = false;
            Invoke("SetCanMove", minStopSec);
        }

        else if (trans.position.x > maxVec.x || trans.position.y > maxVec.y)
        {
            moveSpeed = -moveSpeed;
            canMove = false;
            isMax = true;
            if (ground != null)
            {
                moveGround.SetCanMove();
            }
        }

        newPos = pos + moveSpeed*Time.deltaTime;
        
        rb.MovePosition(newPos);
    }
}
