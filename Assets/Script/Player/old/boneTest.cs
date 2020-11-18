using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boneTest : MonoBehaviour
{

    private Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		rb.rotation=0;
		rb.position=new Vector2(-0.4f,2.5f);
		//rigidbody.velocity=new Vector2(0,0);
    }
}
