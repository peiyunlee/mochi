// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ArrowManager : MonoBehaviour
// {
// 	public GameObject btn;
//     public GameObject[] arrow;

//     private bool[] isStart;
// 	private bool isClosed;
// 	// private Button btnScript;

//     private int shootIdx;

//     public float speed;//2.5f
//     public float between;//2.0f


//     void Start()
//     {
// 		// btnScript = btn.GetComponent<Button>();
// 		isStart = new bool[arrow.Length];
// 		for (int i = 0; i < isStart.Length; i++)
// 		{
// 			isStart[i] = false;
// 		}
//         shootIdx = 0;
// 		InvokeRepeating("Shoot",2.0f,between);
// 		isClosed = false;
//     }

//     // Update is called once per frame
//     void Update()
//     {
// 		// if(btnScript.isClicked && !isClosed){
// 		// 	CancelInvoke("Shoot");
// 		// 	Action();
// 		// }
// 		for (int i = 0; i < isStart.Length; i++)
// 		{
// 			if(isStart[i])
// 				ArrowAction(arrow[i] ,i);
// 		}
//     }

// 	void Shoot(){
// 		isStart[shootIdx] = true;
//         shootIdx ++;
// 		if(shootIdx == isStart.Length)
// 			shootIdx = 0;
// 	}
//     void ArrowAction(GameObject item,int idx)
//     {
//         //做出反應
//         //呼叫player的反應
//         if (item.transform.position.x > -9.5f)
//         {
//             item.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
//         }
//         else
//         {
//             item.transform.position = new Vector3(9.0f, item.transform.position.y, 0);
//             isStart[idx] = false;
//         }
//     }
// 	void Action(){
//         if (this.gameObject.transform.position.x < 8.2f)
//         {
//             this.gameObject.transform.position += new Vector3(1.0f * Time.deltaTime, 0, 0);
//         }
//         else
//         {
//             isClosed = true;
//         }
// 	}
// }
