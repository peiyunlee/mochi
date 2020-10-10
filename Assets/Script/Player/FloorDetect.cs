using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetect : MonoBehaviour
{

    // Use this for initialization
    private test test;
    public bool canJump = false;
    public GameObject parents;
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
        if (other.gameObject.tag != "wall" && other.gameObject.tag != parents.tag)
        {
            if (detectType == DetectType.floor)
            {
                Debug.Log("a");
                test.canJump = true;
            }
        }
        if (other.gameObject.tag != parents.tag)
        {
            if (detectType == DetectType.stick)
            {
                test.canStick = true;
                test.item = other.gameObject;
                // if(other.gameObject.tag=="player1"||other.gameObject.tag=="player2"||other.gameObject.tag=="player3"||other.gameObject.tag=="player4"||other.gameObject.tag=="player5"){
                //     test.canBomb=true;
                // }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag != "wall" && other.gameObject.tag != parents.tag)
        {
            // if (detectType == DetectType.floor)
            // {
            //     Debug.Log("a");
            //     test.canJump = true;
            // }

        }
        if (other.gameObject.tag != parents.tag)
        {
            if (detectType == DetectType.stick)
            {
                test.canStick = true;
                test.item = other.gameObject;
                if(other.gameObject.tag=="player1"||other.gameObject.tag=="player2"||other.gameObject.tag=="player3"||other.gameObject.tag=="player4"||other.gameObject.tag=="player5"){
                    test.canBomb=false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != "wall" && other.gameObject.tag != parents.tag)
        {
            // if (detectType == DetectType.floor)
            // {
            //     Debug.Log("b");
            //     test.canJump = false;
            // }
        }
        if (other.gameObject.tag != parents.tag)
        {
            if (detectType == DetectType.stick)
            {
                test.canStick = false;
                test.item = null;
                if(other.gameObject.tag=="player1"||other.gameObject.tag=="player2"||other.gameObject.tag=="player3"||other.gameObject.tag=="player4"||other.gameObject.tag=="player5"){
                    test.canBomb=false;
                }
            }
        }
    }
}
