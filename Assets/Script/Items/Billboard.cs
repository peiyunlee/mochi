#define JOYSTICK

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    public GameObject btn;
    public int inNumber;

    int inCount;

    public GameObject billboard;

    public GameObject draw;

    public bool isActive;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player") && other.gameObject.name == "stickDetect")
        {
            inCount++;
            if (inCount == 1)
                btn.SetActive(true);
            LevelController.instance.billboardController.inNumber = inNumber;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player") && other.gameObject.name == "stickDetect")
        {
            inCount--;
            if (inCount == 0)
                btn.SetActive(false);
            LevelController.instance.billboardController.inNumber = 0;
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
            Invoke("SetIsActive",0.1f);
            GameManager.instance.StartGame();
        }
    }

    void SetIsActive(){
        isActive = false;
    }
}
