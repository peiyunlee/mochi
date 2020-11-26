using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{


    public Camera cam;

    public float x, y;

    Vector2 currentVector;

    RaycastHit2D hit;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // if (Input.GetKeyDown("t"))
        // {
        //     Vector2 newPos;
        //     newPos = cam.ScreenToWorldPoint(new Vector3(x, y, cam.nearClipPlane));
        //     newPos = DetectPoint(newPos);
        //     Debug.Log(newPos);
        //     this.transform.position = newPos;
        // }
    }

    Vector2 DetectPoint(Vector2 point)
    {
        bool result = false;
        Vector3 slop = new Vector3(point.x, point.y, -10) - new Vector3(point.x, point.y, 10);
        hit = Physics2D.Raycast(point, slop);
        result = hit.collider;

        if (result)
        {
            currentVector = point;
            return DetectPoint(point + Vector2.up);
        }
        else
        {
            if (point == currentVector + Vector2.right)
            {
                currentVector = Vector2.zero;
                return point;
            }
            else
            {
                currentVector = point;
                return DetectPoint(point + Vector2.right);
            }
        }
    }
}
