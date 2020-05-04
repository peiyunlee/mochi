using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarTest : MonoBehaviour {
	public Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		//rb=GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw("Vertical_" + this.tag) > 0)
            {
                //earRb.AddForce (Vector2.right * 200,0);
                rb.velocity = new Vector3(Vector2.up.x * 5, Vector2.up.y * 5, 0);
            }
            if (Input.GetAxisRaw("Vertical_" + this.tag) < 0)
            {
                //earRb.AddForce (Vector2.right * 200,0);
                rb.velocity = new Vector3(Vector2.down.x * 5, Vector2.down.y * 5, 0);
            }
            if (Input.GetAxisRaw("Horizontal_" + this.tag) < 0)
            {
                rb.velocity = new Vector3(Vector2.left.x * 5, Vector2.left.y * 5, 0);
            }
            if (Input.GetAxisRaw("Horizontal_" + this.tag) > 0)
            {
                rb.velocity = new Vector3(Vector2.right.x * 5, Vector2.right.y * 5, 0);
            }
	
	}
}
