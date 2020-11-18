using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyStick : MonoBehaviour {
public bool canTouch=true;
public bool canJump=true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag != "wall")
        {
            canJump = true;

        }
        if (other.gameObject.tag != "player3")
        {
            
            canTouch = true;

        }
        
    }
    
}
