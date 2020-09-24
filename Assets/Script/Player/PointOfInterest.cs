using UnityEngine;

public class PointOfInterest : MonoBehaviour {

	private void Start(){
		MultipleTargetCamera.Instance.AddTarget(transform);
	}
	
	private void OnDisable(){
		MultipleTargetCamera.Instance.RemoveTarget(transform);
	}
}
