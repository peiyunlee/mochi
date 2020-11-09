using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveTile : MonoBehaviour
{
    public Vector3 maxVec, minVec;

    public Vector3 moveSpeed;

    public List<Vector3Int> tileList;

    public bool canMove;

    public float maxStopSec;
    public float minStopSec;

    public float waitSec;

    Tilemap tilemap;

    Matrix4x4 matrix;

    Vector3 translate;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        if (waitSec > 0)
        {
            canMove = false;
            Invoke("SetCanMove", waitSec);
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
        

        if (translate.x < minVec.x || translate.y < minVec.y)
        {
            moveSpeed = -moveSpeed;
            canMove = false;
            Invoke("SetCanMove", minStopSec);
        }
        else if (translate.x > maxVec.x || translate.y > maxVec.y)
        {
            moveSpeed = -moveSpeed;
            canMove = false;
            Invoke("SetCanMove", maxStopSec);
        }


        translate += moveSpeed*Time.deltaTime;
        foreach (var tileVec in tileList)
        {
            matrix = Matrix4x4.TRS(translate, Quaternion.Euler(0f, 0f, 0f), Vector3.one);
            tilemap.SetTransformMatrix(tileVec, matrix);
        }
    }

    void SetCanMove()
    {
        canMove = true;
    }
}
