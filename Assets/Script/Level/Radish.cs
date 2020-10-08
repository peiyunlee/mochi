using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radish : MonoBehaviour
{

    LevelController levelController;

    void Awake()
    {
        levelController = GameObject.Find("EventSystem").GetComponent<LevelController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		Debug.Log("radish");
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            levelController.AddRadish();
            Destroy(this.gameObject);
        }
    }
}
