using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarStick : MonoBehaviour
{
    public bool earCanTouch = false;
    public Rigidbody2D otherRb;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            if (other.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                otherRb = other.gameObject.GetComponent<Rigidbody2D>();
            }
        }
        earCanTouch = true;
    }
}
