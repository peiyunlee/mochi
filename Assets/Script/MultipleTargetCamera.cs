// using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{

    //farthest offset -10/-3/-15
    //center offset 11/7/-15


    public List<Transform> targets;
    public Vector3 offset;
    public float smoothTime = .5f;
    public Vector3 velocity;

    public float minX, minY;
    public float maxX, maxY;

    public float maxZoom, minZoom;
    public float zoomLimiter;

    public float zoomSpeed;

    private Camera cam;
    private Bounds m_bounds;


    public static MultipleTargetCamera Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        cam = GetComponent<Camera>();
    }


    void LateUpdate()
    {

        if (targets.Count == 0)
            return;

        GetGreatestDistance();
        Move();
        Zoom();
    }

    void Move()
    {

        Vector3 centerPoint = GetCenterPoint();
        // Vector3 farthestPoint = GetFarthestPoint();

        Vector3 newPostion = centerPoint + offset;

        // Vector3 p = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth,cam.pixelHeight, cam.nearClipPlane));


        float greatestDistance = m_bounds.size.x > m_bounds.size.y ? m_bounds.size.x : m_bounds.size.y;

        float p = Mathf.Lerp(14.5f, 64.5f, greatestDistance / zoomLimiter);

        // Debug.Log(p);

        if (cam.transform.position.x - p < minX)
        {
            newPostion.x += minX + p;
        }
        else if (cam.transform.position.x + p > maxX)
        {
            newPostion.x = newPostion.x - (newPostion.x + p - maxX);
        }

        // if (newPostion.y < minY)
        //     newPostion.y = minY;
        // else if (newPostion.y > maxY)
        //     newPostion.y = maxY;

        transform.position = Vector3.SmoothDamp(transform.position, newPostion, ref velocity, smoothTime);
    }

    void Zoom()
    {
        float greatestDistance = m_bounds.size.x > m_bounds.size.y ? m_bounds.size.x : m_bounds.size.y;

        float newZoom = Mathf.Lerp(maxZoom, minZoom, greatestDistance / zoomLimiter);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime * zoomSpeed);
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

    Vector3 GetFarthestPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        float maxX = targets[0].position.x;
        float maxY = targets[0].position.y;
        float maxZ = targets[0].position.z;

        for (int i = 1; i < targets.Count; i++)
        {
            if (maxX < targets[i].position.x) maxX = targets[i].position.x;
            if (maxY < targets[i].position.y) maxY = targets[i].position.y;
        }

        return new Vector3(maxX, maxY, maxZ);
    }

    public void AddTarget(Transform newTarget)
    {

        if (!targets.Contains(newTarget))
        {
            targets.Add(newTarget);
        }

    }
    public void RemoveTarget(Transform TargetToRemove)
    {

        if (!targets.Contains(TargetToRemove))
        {
            targets.Remove(TargetToRemove);
        }

    }
}
