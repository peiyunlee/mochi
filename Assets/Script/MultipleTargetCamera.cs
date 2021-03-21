// using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    public struct ClipPlanePoints
    {
        public Vector3 upper;
        public Vector3 lower;
        public Vector3 left;
        public Vector3 right;
    }

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

    public float centralPoint;

    public ClipPlanePoints clipPlanePoints = new ClipPlanePoints();

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

        Zoom();

        Move();

    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        float greatestDistance = m_bounds.size.x > m_bounds.size.y ? m_bounds.size.x : m_bounds.size.y;


        float distance = cam.nearClipPlane;

        float halfFOV = (cam.fieldOfView * 0.5f) * Mathf.Deg2Rad;
        float aspect = cam.aspect;

        //screen height and screen width
        float height = Mathf.Tan(halfFOV) * distance;
        float width = height * aspect;

        //upper
        clipPlanePoints.upper = centerPoint + transform.forward * distance;
        clipPlanePoints.upper += transform.up * height;

        //lower
        clipPlanePoints.lower = centerPoint + transform.forward * distance;
        clipPlanePoints.lower -= transform.up * height;

        //right
        clipPlanePoints.right = centerPoint + transform.forward * distance;
        clipPlanePoints.right += transform.right * width;

        //left
        clipPlanePoints.left = centerPoint + transform.forward * distance;
        clipPlanePoints.left -= transform.right * width;

        if (clipPlanePoints.right.x > max.x)
        {
            centerPoint.x = max.x - (transform.right * width).x;
            clipPlanePoints.right.x = max.x;
        }
        else if (clipPlanePoints.left.x < min.x)
        {
            centerPoint.x = min.x + (transform.right * width).x;
            clipPlanePoints.left.x = min.x;
        }

        if (clipPlanePoints.lower.y < min.y)
        {
            centerPoint.y = min.y + (transform.up * height).y;
            // clipPlanePoints.lower.y=min.y;
            if (centerPoint.y > centralPoint) centerPoint.y = centralPoint;
        }
        else if (clipPlanePoints.upper.y > max.y)
        {
            centerPoint.y = max.y - (transform.up * height).y;
            // clipPlanePoints.upper.y=max.y;
            if (centerPoint.y < centralPoint) centerPoint.y = centralPoint;
        }

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
                // m_bounds.Encapsulate(targets[i].position);
                m_bounds.Encapsulate(new Vector3(targets[i].position.x - 1.0f, targets[i].position.y + 1.0f, targets[i].position.z));//左上
                m_bounds.Encapsulate(new Vector3(targets[i].position.x + 1.0f, targets[i].position.y -1.0f, targets[i].position.z));//右下
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
            // bounds.Encapsulate(targets[i].position);
            bounds.Encapsulate(new Vector3(targets[i].position.x - 1.0f, targets[i].position.y + 1.0f, targets[i].position.z));//左上
            bounds.Encapsulate(new Vector3(targets[i].position.x + 1.0f, targets[i].position.y - 1.0f, targets[i].position.z));//右下
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
    public bool PlayerIsTarget(Transform playerTarget)
    {
        Debug.Log(targets.Contains(playerTarget));
        return targets.Contains(playerTarget);
    }
}
