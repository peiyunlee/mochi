using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraTest : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;
    public Vector3 velocity;
    public float smoothTime = .5f;
    public float minX, minY;
    public float maxX, maxY;
    public float maxZoom, minZoom;
    private Bounds m_bounds;
    public float zoomLimiter;
    private Camera cam;
    public float zoomSpeed;
    float greatestDistance;
    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targets.Count == 0)
            return;

        Move();

        GetGreatestDistance();

        Zoom();


    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        // Debug.Log(centerPoint);


        Vector3 newPostion = centerPoint + offset;

        float p = cam.orthographicSize / 2.0f;

        // float p = m_bounds.extents.x;
        if (newPostion.x - p < minX)
        {
            newPostion.x = minX + p;
        }
        else if (newPostion.x + p > maxX)
        {
            newPostion.x = maxX + p;
        }
        // p = m_bounds.extents.y;
        if (newPostion.y - p < minX)
        {
            newPostion.y = minY + p;
        }
        else if (newPostion.y + p > maxY)
        {
            newPostion.y = maxY + p;
        }
        // if (m_bounds.extents.x > m_bounds.extents.y)
        // {
        //     float p = m_bounds.extents.x;
        //     if (newPostion.x - p < minX)
        //     {
        //         newPostion.x = minX + p;
        //     }
        //     else if (newPostion.x + p > maxX)
        //     {
        //         newPostion.x = maxX + p;
        //     }
        // }
        // else
        // {
        //     float p = m_bounds.extents.y;
        //     if (newPostion.y - p < 0)
        //     {
        //         newPostion.y = minY + p;
        //     }
        //     else if (newPostion.y + p > maxY)
        //     {
        //         newPostion.y = maxY + p;
        //     }
        // }

        // Debug.Log(newPostion.y - p);
        transform.position = Vector3.SmoothDamp(transform.position, newPostion, ref velocity, smoothTime);
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(minZoom, maxZoom, greatestDistance / zoomLimiter);

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
        // cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    void GetGreatestDistance()
    {
        m_bounds = new Bounds(targets[0].position, Vector3.zero);

        if (targets.Count > 1)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                m_bounds.Encapsulate(targets[i].position);
            }
        }
        Debug.Log(new Vector2(m_bounds.size.x, m_bounds.size.y));
        // greatestDistance = m_bounds.extents.x > m_bounds.extents.y ? m_bounds.size.x : m_bounds.size.y;
        greatestDistance = Mathf.Sqrt(Mathf.Pow(m_bounds.size.x, 2) + Mathf.Pow(m_bounds.size.y, 2));
    }
    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);

        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}
