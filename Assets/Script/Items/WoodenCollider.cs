using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenCollider : MonoBehaviour
{
    public Rigidbody2D upwooden;
    public Rigidbody2D downwooden;
    public Transform movePosition;
    public float colliderMove;
    public List<float> breakAngle;

    public WoodenCollider otherCollider;

    float upAngle = 0;
    float downAngle = 0;
    public int breakCount = 0;
    bool breakWooden = false;
    bool canUse=true;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (otherCollider.breakCount > 0)
        {
            canUse = false;
        }
    }

    void BreakWooden()
    {
        breakCount++;

        Quaternion rotation;

        upAngle -= breakAngle[breakCount - 1];
        rotation = Quaternion.Euler(0, 0, upAngle);
        upwooden.gameObject.transform.rotation = rotation;

        downAngle += breakAngle[breakCount - 1];
        rotation = Quaternion.Euler(0, 0, downAngle);
        downwooden.gameObject.transform.rotation = rotation;

        breakWooden = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canUse)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("player"))
            {
                if (other.gameObject.name == "stickDetect")
                {
                    if (other.gameObject.GetComponentInParent<PlayerStick>() != null)
                    {
                        if (other.gameObject.GetComponentInParent<PlayerStick>().isPoped)
                        {
                            if (!breakWooden)
                            {

                                BreakWooden();
                            }

                        }
                    }
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (canUse)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("player"))
            {
                if (other.gameObject.name == "stickDetect")
                {
                    if (breakWooden)
                    {
                        transform.position = new Vector3((movePosition.position).x, transform.position.y, transform.position.z);
                        breakWooden = false;
                    }
                }
            }
        }
    }
}
