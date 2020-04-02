using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour {

	public GameObject rabbit1;
	private Test test;
	// Use this for initialization
	void Start () {
		test = rabbit1.GetComponent<Test>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		test.canGrab = true;
		print("can");
	}
}
