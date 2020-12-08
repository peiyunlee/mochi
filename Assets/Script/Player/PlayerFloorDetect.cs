using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloorDetect : MonoBehaviour
{

    public bool isOnFloor { get { return m_isOnFloor; } }

    [SerializeField]
    private bool m_isOnFloor;
    public GameObject parents;

    void Start()
    {
    }

    void Update()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 50.0f);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // if (other.gameObject.tag == "ground" && other.gameObject.tag != parents.tag)
        if (other.gameObject.tag != parents.tag && other.gameObject.tag != "wall")
        {
            m_isOnFloor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != parents.tag && other.gameObject.tag != "wall")
        {
            m_isOnFloor = false;
        }
    }
}
