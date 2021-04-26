using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWooden : MonoBehaviour
{
    public WoodenCollider woodenCollider_left;
    public WoodenCollider woodenCollider_right;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (woodenCollider_left.breakCount >= 3||woodenCollider_right.breakCount >= 3)
        {
            Active(false);
        }
    }

    void Active(bool setActive)
    {
        woodenCollider_left.gameObject.SetActive(setActive);
        woodenCollider_right.gameObject.SetActive(setActive);
    }
}
