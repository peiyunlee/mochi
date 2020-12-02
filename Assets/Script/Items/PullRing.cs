using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullRing : MonoBehaviour
{
    //拉環伸長距離
    public Vector2 Distant;
    public GameObject middle;
    public Handle handle;
    Rigidbody2D middleRb;
    RelativeJoint2D middleJoint;

    public float maxForce;
    public float maxTorque;

    //平台
    public GameObject plate;
    public float plateSpeed;
    Rigidbody2D plateRb;

    //拉環位置
    Vector2 startPos;
    Vector2 endPos;

    bool isStop = false;

    // Use this for initialization
    void Start()
    {
        middleRb = middle.GetComponent<Rigidbody2D>();
        middleJoint = middle.GetComponent<RelativeJoint2D>();

        plateRb = plate.GetComponent<Rigidbody2D>();

        middleJoint.maxForce = maxForce;
        middleJoint.maxTorque = maxTorque;

        startPos = middleRb.position;
        endPos = startPos + Distant;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStop && !handle.needMochi)
        {
            
            middleRb.isKinematic=false;
            Move();
        }



        Stop();
    }

    void Move()
    {
        plateRb.velocity = new Vector2(middleRb.velocity.y * plateSpeed, 0.0f);
    }

    void Stop()
    {
        Vector2 currentPos = middleRb.position;

        if (currentPos.y <= endPos.y)
        {
            middleRb.position = endPos;
            middleRb.isKinematic = true;
            plateRb.velocity = new Vector2(0.0f, 0.0f);
            isStop = true;
        }

    }
}
