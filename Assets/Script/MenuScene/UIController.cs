using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public List<Transform> levelUI;

    public Transform cam;

    float currentCamSpeed;

    public float camSpeed;

    [SerializeField]
    int currentLevel;

    int maxLevel;

    // Use this for initialization
    void Start()
    {
        currentLevel = 1;
        maxLevel = 4;
        camSpeed = 340.0f / 1.0f;
        currentCamSpeed = camSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("right") && currentLevel < maxLevel)
        {
            currentLevel++;
            currentCamSpeed = camSpeed;
        }
        else if (Input.GetKeyDown("left") && currentLevel > 1)
        {
            currentLevel--;
            currentCamSpeed = -camSpeed;
        }
        // Zoom();
    }
    void FixedUpdate()
    {
        CamMove();
    }

    void CamMove()
    {
        float newPosX;
        if (currentCamSpeed > 0 && cam.position.x < 340.0f * (currentLevel - 1))
        {
            newPosX = cam.position.x + currentCamSpeed * Time.deltaTime;
            cam.position = new Vector3(newPosX, cam.position.y, cam.position.z);
        }
        else if (currentCamSpeed < 0 && cam.position.x > 340.0f * (currentLevel - 1))
        {
            newPosX = cam.position.x + currentCamSpeed * Time.deltaTime;
            cam.position = new Vector3(newPosX, cam.position.y, cam.position.z);
        }
    }

    void Zoom()
    {
    }
}
