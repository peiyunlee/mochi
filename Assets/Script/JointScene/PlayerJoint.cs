using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoint : MonoBehaviour
{

    [SerializeField]
    bool isReady = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if(isReady)
			SceneController.Instance.LoadNextScene("Play");
    }
}
