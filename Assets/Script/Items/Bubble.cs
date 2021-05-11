using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
			animator.SetTrigger("isBroken");
        }
    }
}
