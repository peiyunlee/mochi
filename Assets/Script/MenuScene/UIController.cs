using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public List<Transform> levelUI;

    int currentLevel;
	
    float distance;

    // Use this for initialization
    void Start()
    {
        currentLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal_player1") == -1)
            Debug.Log("LEFT");
        // Move(distance);
        // Zoom();
    }

    public void UIBtnClick(string type)
    {
        // if (type == "Right")
        // {
        //     distance = -340.0f;
        //     currentLevel++;
        // }
        // else
        // {
        //     distance = -340.0f;
        //     currentLevel--;
        // }
    }

    void Move(float distance)
    {
        levelUI.ForEach(delegate (Transform item)
        {
            float newPosX = Mathf.Lerp(item.position.x, item.position.x + distance, Time.deltaTime);

            item.position = new Vector3(newPosX, item.position.y, item.position.z);

        });


    }

    void Zoom()
    {
    }
}
