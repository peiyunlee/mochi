using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarStick : MonoBehaviour
{
    public bool earCanTouch = false;
    public bool rootMotion = false;
    public Rigidbody2D otherRb;
    private Rigidbody2D earRb;
    public float moveY = 0;

    public Collider2D[] exclude;
    // Use this for initialization
    void Start()
    {
        earRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //idleAnim();
    }

    public void idleAnim()
    {
        earRb.velocity = new Vector2(earRb.velocity.x, earRb.velocity.y + moveY);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.gameObject.tag == "ground")
        // {
        //     if (other.gameObject.GetComponent<Rigidbody2D>() != null)
        //     {
        //         otherRb = other.gameObject.GetComponent<Rigidbody2D>();
        //     }
        // }
        

        if (other != exclude[0]&&other != exclude[1]&&other.gameObject.tag!="joint"){
            earCanTouch = true;
            otherRb = other.gameObject.GetComponent<Rigidbody2D>();
        }

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        // if (other.gameObject.tag == "ground")
        // {
        //     if (other.gameObject.GetComponent<Rigidbody2D>() != null)
        //     {
        //         otherRb = other.gameObject.GetComponent<Rigidbody2D>();
        //     }
        // }
        

        if (other != exclude[0]&&other != exclude[1]&&other.gameObject.tag!="joint"){
            earCanTouch = true;
            otherRb = other.gameObject.GetComponent<Rigidbody2D>();
        }

    }
}
