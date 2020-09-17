using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EarTest : MonoBehaviour
{
    Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal_player3") < 0)
        {
            rb.velocity = new Vector3(Vector2.left.x * 4, Vector2.left.y * 4, 0);
        }
        else if (Input.GetAxisRaw("Horizontal_player3") > 0)
        {
            rb.velocity = new Vector3(Vector2.right.x * 4, Vector2.right.y * 4, 0);
        }
		if(Input.GetAxisRaw("Vertical_player3") < 0){
			rb.velocity = new Vector3(Vector2.down.x * 4, Vector2.down.y * 4, 0);
		}
    }
}
