﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	Rigidbody2D rb;
	float moveSpeed;
	float jumpSpeed;

	//Turn
	public Rigidbody2D earRb;//围绕的物体
    public float angularSpeed;//角速度
    private float aroundRadius;//半径
	private float angled;
	private float posX;
	private float posY;
	public float earMoveSpeed;
	//Use this for initialization
	bool isTouch;
	void Start () {
		moveSpeed=3;
		jumpSpeed=100;
		rb=GetComponent<Rigidbody2D>();

		//设置物体初始位置为围绕物体的正前方距离为半径的点
        //Vector2 p = aroundPoint.rotation * Vector2.right * aroundRadius;
        //transform.position = new Vector3(p.x, aroundPoint.position.y, 0);
		angled=90;
		aroundRadius=earRb.position.y-rb.position.y;
		isTouch=false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.X))isTouch=!isTouch;
	}

	void FixedUpdate() {
		if(!isTouch){
			Move();

			Jump();	
		}

		Turn();
	}

	void Move(){
		Vector2 trans;
		trans=new Vector2(Input.GetAxisRaw("Horizontal")*moveSpeed,rb.velocity.y);
		rb.velocity=trans;
		earRb.velocity=trans;
	}

	void Jump(){
		if(Input.GetKeyDown(KeyCode.Z)){
			rb.AddForce(new Vector2(0,jumpSpeed));
			earRb.AddForce(new Vector2(0,jumpSpeed));
		}
	}
	void Turn(){
		if(isTouch){
			// if(Input.GetAxisRaw("Horizontal")==0){
			// 	posX=earRb.position.x;
			// 	posY=earRb.position.y;
			// }
			// else{
				// angled -= (angularSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal")) % 360;//累加已经转过的角度
				// //Debug.Log(angled);
        		// posY = aroundRadius * Mathf.Sin(angled * Mathf.Deg2Rad)+rb.position.y;//计算x位置
        		// posX = aroundRadius * Mathf.Cos(angled * Mathf.Deg2Rad)+rb.position.x;//计算y位置
			//}
			// else if(Input.GetAxisRaw("Horizontal")==1){
			// angled -= (angularSpeed * Time.deltaTime) % 360;//累加已经转过的角度
			// //Debug.Log(angled);
        	// posY = aroundRadius * Mathf.Sin(angled * Mathf.Deg2Rad)+rb.position.y;//计算x位置
        	// posX = aroundRadius * Mathf.Cos(angled * Mathf.Deg2Rad)+rb.position.x;//计算y位置
			
        	// //earRb.velocity = new Vector2(posX,posY)* Input.GetAxisRaw("Horizontal");//更新位置
			// }
			// else if(Input.GetAxisRaw("Horizontal")==-1){
			// angled += (angularSpeed * Time.deltaTime) % 360;//累加已经转过的角度
			// //Debug.Log(angled);
        	// posY = aroundRadius * Mathf.Sin(angled * Mathf.Deg2Rad)+rb.position.y;//计算x位置
        	// posX = aroundRadius * Mathf.Cos(angled * Mathf.Deg2Rad)+rb.position.x;//计算y位置
			// }
			//if(Input.GetKeyDown(KeyCode.RightArrow))earRb.AddForce (Vector3.right * 0);
			//if(Input.GetKeyDown(KeyCode.RightArrow))earRb.AddForce (Vector3.right * 100);
			//else if(Input.GetKeyDown(KeyCode.LeftArrow))earRb.AddForce (Vector3.left * 100);
			earRb.AddForce (new Vector2(Input.GetAxisRaw("Horizontal") * 10, 0));
			
		}
	}
}
