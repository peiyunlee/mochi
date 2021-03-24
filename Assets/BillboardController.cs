using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardController : MonoBehaviour {


    //Billboard
    public GameObject billboardObjet;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.isPause && billboardObjet != null && (Input.GetButtonDown("AButton_player1") || Input.GetButtonDown("AButton_player2")))
        {
            billboardObjet.GetComponent<Billboard>().Hide();
            billboardObjet = null;
        }
        else if (Input.GetKeyDown("a") && billboardObjet != null && GameManager.instance.isPause)
        {
			//test
            billboardObjet.GetComponent<Billboard>().Hide();
        }
	}
}
