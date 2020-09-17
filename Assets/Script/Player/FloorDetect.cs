using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetect : MonoBehaviour
{

    // Use this for initialization
    // public bool canJump = true;
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
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag != "wall")
        {
            //Debug.Log("trigger");
            test.canJump = true;
        }
    }
}
