// #define JOYSTICK

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    public GameObject btn;

    [SerializeField]
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
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player") && other.gameObject.name == "stickDetect")
        {
            inCount--;
            if (inCount == 0)
                btn.SetActive(false);
        }
    }

    public void Show()
    {
        if (!isActive)
        {
            billboard.SetActive(true);
            draw.SetActive(true);
			Invoke("SetActive",0.05f);
        }
    }

    void Hide()
    {
        if (isActive)
        {
            billboard.SetActive(false);
            draw.SetActive(false);
            isActive = false;
        }
    }

    void Update()
    {
        if (isActive)
        {
#if JOYSTICK
            if (Input.GetButtonDown("AButton_player1") || Input.GetButtonDown("AButton_player2"))
            {
                Hide();
            }
#else
            if (Input.GetKeyDown("b"))
            {
                Hide();
            }
#endif
        }
    }

    void SetActive()
    {
        isActive = true;
    }
}
