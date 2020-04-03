using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test4 : MonoBehaviour {
	public GameObject top;
	public GameObject center;
	private Transform ttop;
	private Transform tcenter;
	private Transform tbottom;
	private LineRenderer line;

	// Use this for initialization
	void Start () {
		ttop = top.GetComponent<Transform>();
		tcenter = this.gameObject.GetComponent<Transform>();
		tbottom = this.gameObject.GetComponent<Transform>();
		line = this.gameObject.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 center = tbottom.position-((tbottom.position-ttop.position)/2.0f);
		line.SetPosition(0,tbottom.position);
		line.SetPosition(1,tcenter.position);
		line.SetPosition(2,ttop.position);
	}
}
