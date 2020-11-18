using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveTile : MonoBehaviour
{
    public Vector3 maxVec, minVec;

    public Vector3 moveSpeed;

    public bool canMove;

    public float maxStopSec;
    public float minStopSec;

    public float waitSec;

    Vector3 translate;

    Transform trans;
    Rigidbody2D rb;

    Vector2 newPos;

    public bool selfStart;

    public bool isContinue;

    public bool isMax;

    void Start()
    {
        trans = this.gameObject.GetComponent<Transform>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();

        if (selfStart)
        {
            SetStart();
        }
    }

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
            if (!isContinue)
            {
                if (isMax)
                    Invoke("SetCanMove", minStopSec);
            }
            else
                Invoke("SetCanMove", minStopSec);


        }

        else if (trans.position.x > maxVec.x || trans.position.y > maxVec.y)
        {
            moveSpeed = -moveSpeed;
            canMove = false;
            if (!isContinue)
            {
                if (!isMax)
                    Invoke("SetCanMove", minStopSec);
            }
            else
                Invoke("SetCanMove", minStopSec);
        }

        newPos = pos + moveSpeed;

        rb.MovePosition(newPos);

    }

    void SetCanMove()
    {
        canMove = true;
    }

    public void SetStart()
    {
        if (waitSec > 0)
        {
            canMove = false;
            Invoke("SetCanMove", waitSec);
        }
        else
        {
            SetCanMove();
        }
    }
}
