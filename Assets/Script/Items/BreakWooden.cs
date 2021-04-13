using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWooden : MonoBehaviour
{
    public WoodenCollider woodenCollider;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (woodenCollider.breakCount >= 3)
        {
            Active(false);
        }
    }

    void Active(bool setActive)
    {
        woodenCollider.gameObject.SetActive(setActive);
    }
}
