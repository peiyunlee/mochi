using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boneTest : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		rigidbody.rotation=0;
		rigidbody.position=new Vector2(-0.4f,2.5f);
		//rigidbody.velocity=new Vector2(0,0);
    }
}
