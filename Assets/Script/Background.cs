using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

	public Camera c;

	float field;

	float size;

	public float scale;

	public float offsetY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		field = c.fieldOfView;
		
		size = (field - 60) * scale + 1;

		this.gameObject.transform.localScale = new Vector2(size,size);
	}
}
