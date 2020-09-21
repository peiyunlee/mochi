using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetect : MonoBehaviour
{

    // Use this for initialization
	private test test;
    void Start()
    {
		test=GetComponentInParent<test>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "wall"&&other.gameObject.tag != "player3")
        {
            test.canJump = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != "player3")
        {
            test.canJump = false;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag != "wall"&&other.gameObject.tag != "player3")
        {
            test.canJump = true;
        }
    }
}
