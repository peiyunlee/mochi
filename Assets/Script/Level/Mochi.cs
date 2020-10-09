using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mochi : MonoBehaviour {

	LevelController levelController;

    void Start()
    {
        levelController = GameObject.Find("EventSystem").GetComponent<LevelController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		Debug.Log("mochi");
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            levelController.AddMochi();
            Destroy(this.gameObject);
        }
    }
}
