using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3 : MonoBehaviour {
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = this.gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyDown (KeyCode.RightArrow)){
            rb.AddForce (Vector3.right * 200);
		}
        if (Input.GetKeyDown (KeyCode.LeftArrow)){
            rb.AddForce (Vector3.left * 200);
		}
        if (Input.GetKeyDown (KeyCode.UpArrow)){
            rb.AddForce (Vector3.up * 200);
		}
	}
}
