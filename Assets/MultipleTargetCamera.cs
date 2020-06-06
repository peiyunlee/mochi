using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{

    public List<Transform> targets;
    public Vector3 offset;

    public float x,y;

    public float max=10f;
    public float min=20f;
    public float zoomLimiter=30f;

    private Camera cam;

    void Start() {
        cam=GetComponent<Camera>();
    }


    void LateUpdate()
    {
        
        if (targets.Count == 0)
            return;
			
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPostion = centerPoint + offset;

        if(newPostion.x<x){
            newPostion.x=x;
        }
        if(newPostion.y<y){
            newPostion.y=y;
        }

        transform.position = newPostion;
        Zoom();
    }

    void Zoom(){
        Debug.Log(GetGreatestDistance());
        float newZoom=Mathf.Lerp(max,min,GetGreatestDistance()/zoomLimiter);
        cam.fieldOfView=Mathf.Lerp(cam.fieldOfView,newZoom,Time.deltaTime);
    }

    float GetGreatestDistance(){
        var bounds=new Bounds(targets[0].position,Vector3.zero);
        for(int i=0;i<targets.Count;i++){
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
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
