using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    LevelController levelController;
    int stickCount;

    void Awake()
    {
        levelController = GameObject.Find("EventSystem").GetComponent<LevelController>();
    }

    void Start()
    {
        stickCount = 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            stickCount++;
			if(stickCount == GameManager.instance.playerCount)
            	levelController.GameFinish();
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            stickCount--;
        }
    }
}
