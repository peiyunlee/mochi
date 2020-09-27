using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetect : MonoBehaviour
{

    // Use this for initialization
    private test test;
    enum DetectType
    {
        none,
        floor,
        stick
    }
    DetectType detectType = DetectType.none;
    // public GameObject item=null;
    void Start()
    {
        test = GetComponentInParent<test>();
        if (this.gameObject.tag == "floorDetect") detectType = DetectType.floor;
        else if (this.gameObject.tag == "stickDetect") detectType = DetectType.stick;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "wall" && other.gameObject.tag != "player3")
        {
            if (detectType == DetectType.floor)
            {
                test.canJump = true;
            }
        }
        if (other.gameObject.tag != "player3")
        {
            if (detectType == DetectType.stick)
            {
                test.canStick = true;
                test.item=other.gameObject;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag != "wall" && other.gameObject.tag != "player3")
        {
            if (detectType == DetectType.floor)
            {
                test.canJump = true;
            }

        }
        if (other.gameObject.tag != "player3")
        {
            if (detectType == DetectType.stick)
            {
                test.canStick = true;
                test.item=other.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != "wall" && other.gameObject.tag != "player3")
        {
            if (detectType == DetectType.floor)
            {
                test.canJump = false;
            }
        }
        if (other.gameObject.tag != "player3")
        {
            if (detectType == DetectType.stick)
            {
                test.canStick = false;
                test.item=null;
            }
        }
    }
}
