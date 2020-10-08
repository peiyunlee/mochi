using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerFloorDetect : MonoBehaviour
{

    public bool isOnFloor { get { return m_isOnFloor; } }

    private bool m_isOnFloor;
    public GameObject parents;

    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ground" && other.gameObject.tag != parents.tag)
        {
            m_isOnFloor = true;
        }
    }
}
