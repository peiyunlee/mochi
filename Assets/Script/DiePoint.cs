using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiePoint : MonoBehaviour
{
    public int index;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player") && other.gameObject.name == "stickDetect")
        {
            other.gameObject.GetComponentInParent<PlayerMovement>().diePoint = index;
        }
    }
}
