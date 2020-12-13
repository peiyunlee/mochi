// using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{

    public List<Transform> targets;
    public Vector3 offset;
    public float smoothTime = .5f;
    public Vector3 velocity;

    public Vector2 min, max;

    public float maxZoom, minZoom;
    public float zoomLimiter;

    public float zoomSpeed;

    private Camera cam;
    private Bounds m_bounds;

    public static MultipleTargetCamera Instance { get; private set; }

    public float s1,s2;

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

        Vector2 cPos = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        
        if (centerPoint.x < min.x)
        {
            centerPoint.x = min.x;
        }
        else if (centerPoint.x > max.x){
            centerPoint.x = max.x;
        }
        
        if (centerPoint.y < min.y)
        {
            centerPoint.y = min.y;
        }
        else if (centerPoint.y > max.y){
            centerPoint.y = max.y;
        }

        float s = s1;

        if(centerPoint.y > max.y - 5.0f){
            s = s1;
        }
        else if(centerPoint.y < min.y + 5.0f){
            s = s2;
        }

        offset.y = Mathf.Lerp(s, 0, m_bounds.size.y / 10.0f);

        Vector3 newPostion = centerPoint + offset;

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

    public void AddTarget(Transform newTarget)
    {

        if (!targets.Contains(newTarget))
        {
            targets.Add(newTarget);
        }

    }
    public void RemoveTarget(Transform TargetToRemove)
    {

        if (targets.Contains(TargetToRemove))
        {
            targets.Remove(TargetToRemove);
        }

    }
}
