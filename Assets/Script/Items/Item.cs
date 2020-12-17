using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField]

    public bool isSticked;
    public bool isOnFloor = true;

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            isOnFloor = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            isOnFloor = false;
        }
    }
}
