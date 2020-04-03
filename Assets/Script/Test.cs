using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	//黏住沒有甩力
	public GameObject gParent;
	public GameObject earGroup;
	public GameObject grabOBJ;

	private Collider2D cTop;
	private Transform me;
	private Vector3 tParent;
	public bool canGrab;
	bool isGrab;


	void Start () {
		me = earGroup.GetComponent<Transform>();
		tParent = gParent.GetComponent<Transform>().position;
		cTop = this.gameObject.GetComponent<Collider2D>();
	}
	
	void FixedUpdate () {
        if (Input.GetKeyDown (KeyCode.RightArrow)){
            me.RotateAround(tParent,new Vector3(0,0,1),-20);
			if(isGrab){
				grabOBJ.transform.RotateAround(tParent,new Vector3(0,0,1),-20);
			}
		}
        if (Input.GetKeyDown (KeyCode.LeftArrow)){
            me.RotateAround(tParent,new Vector3(0,0,1),20);
			if(isGrab){
				grabOBJ.transform.RotateAround(tParent,new Vector3(0,0,1),20);
			}
		}
		if(canGrab && Input.GetKeyDown (KeyCode.UpArrow)){
			isGrab = true;
			print("is");
		}
	}

}
