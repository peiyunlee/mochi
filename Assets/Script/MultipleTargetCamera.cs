using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{

    public List<Transform> targets;
    public Vector3 offset;
    public float smoothTime = .5f;
    public Vector3 velocity;

    public float minX,minY;
    public float maxX,maxY;

    public float maxZoom=10f;
    public float minZoom=40f;
    public float zoomLimiter=50f;

    private Camera cam;

    void Start() {
        cam=GetComponent<Camera>();
    }


    void LateUpdate()
    {
        
        if (targets.Count == 0)
            return;

        Move();
        Zoom();
    }

    void Move(){
			
        // Vector3 centerPoint = GetCenterPoint();
        Vector3 farthestPoint = GetFarthestPoint();

        Vector3 newPostion = farthestPoint + offset;

        if(newPostion.x < minX)
            newPostion.x = minX;
        else if(newPostion.x > maxX)
            newPostion.x = maxX;

        if(newPostion.y > minY)
            newPostion.y = minY;
        else if(newPostion.y < maxY)
            newPostion.y = maxY;

        transform.position = Vector3.SmoothDamp(transform.position,newPostion,ref velocity,smoothTime);
    }

    void Zoom(){

        float newZoom=Mathf.Lerp(maxZoom,minZoom,GetGreatestDistance()/zoomLimiter);

        cam.fieldOfView=Mathf.Lerp(cam.fieldOfView,newZoom,Time.deltaTime);
    }

    float GetGreatestDistance(){
        var bounds=new Bounds(targets[0].position,Vector3.zero);

        for(int i=0;i<targets.Count;i++){
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

    // Vector3 GetCenterPoint()
    // {
    //     if (targets.Count == 1)
    //     {
    //         return targets[0].position;
    //     }

    //     Bounds bounds = new Bounds(targets[0].position, Vector3.zero);

    //     for (int i = 0; i < targets.Count; i++)
    //     {
    //         bounds.Encapsulate(targets[i].position);
    //     }

    //     return bounds.center;
    // }

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
            if(maxX < targets[i].position.x) maxX = targets[i].position.x;
            if(maxY < targets[i].position.y) maxY = targets[i].position.y;
        }

        return new Vector3(maxX,maxY,maxZ);
    }
}
