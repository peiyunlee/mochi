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

    protected Vector2 newPos;

    public bool selfStart;

    public bool isContinue;

    public bool isMax;

    void Start()
    {

        if (selfStart)
        {
            SetStart();
        }
    }

    public void SetCanMove()
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
