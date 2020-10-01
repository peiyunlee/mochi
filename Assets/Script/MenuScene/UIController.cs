using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public List<Transform> levelUI;

    public Transform cam;

    public List<GameObject> arrow;

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
            if (currentLevel == maxLevel)
            {
                arrow[1].SetActive(false);
            }
            else if (currentLevel == maxLevel - 1)
            {
                arrow[1].SetActive(true);
            }
            else if (currentLevel == 2)
            {
                arrow[0].SetActive(true);
            }
        }
        else if (Input.GetKeyDown("left") && currentLevel > 1)
        {
            currentLevel--;
            currentCamSpeed = -camSpeed;
            if (currentLevel == 1)
            {
                arrow[0].SetActive(false);
            }
            else if (currentLevel == 2)
            {
                arrow[0].SetActive(true);
            }
            else if (currentLevel == maxLevel - 1)
            {
                arrow[1].SetActive(true);
            }
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
