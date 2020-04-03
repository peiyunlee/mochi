using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour {
	//黏住沒甩力、耳朵偵測

	public GameObject rabbit1;
	private Test test;
	// Use this for initialization
	void Start () {
		test = rabbit1.GetComponent<Test>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		test.canGrab = true;
		ContactPoint2D contact = other.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
		Debug.Log(pos);
		print("can");
	}

}
