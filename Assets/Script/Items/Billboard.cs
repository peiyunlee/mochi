#define JOYSTICK

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    public GameObject btn;
    public int inNumber;

    int inCount;
    bool inBool = false;

    public GameObject billboard;

    public GameObject draw;

    public bool isActive;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player") && other.gameObject.name == "stickDetect")
        {
            //inCount++;
            if (!inBool)
            {
                btn.SetActive(true);
                inBool = true;
            }
            LevelController.instance.billboardController.inNumber = inNumber;
            LevelController.instance.billboardController.inPlayer = other.gameObject.tag;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player") && other.gameObject.name == "stickDetect")
        {
            //inCount--;
            if (inBool)
            {
                btn.SetActive(false);
                inBool = false;
            }

            LevelController.instance.billboardController.inNumber = 0;
            LevelController.instance.billboardController.inPlayer = null;
        }
    }

    public void Show()
    {
        if (!isActive)
        {
            billboard.SetActive(true);
            draw.SetActive(true);
            isActive = true;
            GameManager.instance.PauseGame();
        }
    }

    public void Hide()
    {
        if (isActive)
        {
            billboard.SetActive(false);
            draw.SetActive(false);
            isActive = false;
            //Invoke("SetIsActive",0.1f);
            GameManager.instance.StartGame();
        }
    }

    void SetIsActive()
    {
        isActive = false;
    }
}
