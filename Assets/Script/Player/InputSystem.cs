// #define JOYSTICK
// #define HOLDSTICK
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private int testType;

    //movement
    public bool GetKeyJump;

#if !JOYSTICK
    public int GetKeyHMove;
    public int GetKeyVMove;
#else
    public float GetKeyHMove;
    public float GetKeyVMove;

#endif

    //billboard
    public bool getKeyConfirm;

    PlayerPop playerPop;

    PlayerStick playerStick;

    void Start()
    {
        playerPop = GetComponent<PlayerPop>();
        playerStick = GetComponent<PlayerStick>();

#if !JOYSTICK
        if (gameObject.tag == "player1")
            testType = 1;
        else if (gameObject.tag == "player2")
            testType = 2;
        else if (gameObject.tag == "player3")
            testType = 3;
        else testType = 4;
#endif

    }
    void Update()
    {
        if (!GameManager.instance.isPause)
        {
            InputDetect();
        }
    }

    void InputDetect()
    {
        MovementInput();
        PopInput();
        StickInput();
        Other();
    }

    void Other()
    {
#if JOYSTICK
        getKeyConfirm = Input.GetButtonDown("AButton_" + this.tag);
#else
        getKeyConfirm = Input.GetKeyDown("b");
#endif

    }

    void MovementInput()
    {
#if !JOYSTICK
        if (testType == 1)
        {
            GetKeyJump = Input.GetKeyDown("z");

            if (Input.GetKeyDown("right")) GetKeyHMove = 1;
            else if (Input.GetKeyDown("left")) GetKeyHMove = -1;
            else if (Input.GetKeyUp("right") || Input.GetKeyUp("left")) GetKeyHMove = 0;

            if (Input.GetKeyDown("up")) GetKeyVMove = 1;
            else if (Input.GetKeyDown("down")) GetKeyVMove = -1;
            else if (Input.GetKeyUp("up") || Input.GetKeyUp("down")) GetKeyVMove = 0;
        }
        else if (testType == 2)
        {
            GetKeyJump = Input.GetKeyDown("f");

            if (Input.GetKeyDown("d")) GetKeyHMove = 1;
            else if (Input.GetKeyDown("a")) GetKeyHMove = -1;
            else if (Input.GetKeyUp("d") || Input.GetKeyUp("a")) GetKeyHMove = 0;

            if (Input.GetKeyDown("w")) GetKeyVMove = 1;
            else if (Input.GetKeyDown("s")) GetKeyVMove = -1;
            else if (Input.GetKeyUp("w") || Input.GetKeyUp("s")) GetKeyVMove = 0;
        }
        else if (testType == 3)
        {
            GetKeyJump = Input.GetKeyDown("4");


            if (Input.GetKeyDown("3")) GetKeyHMove = 1;
            else if (Input.GetKeyDown("1")) GetKeyHMove = -1;
            else if (Input.GetKeyUp("3") || Input.GetKeyUp("1")) GetKeyHMove = 0;
        }
        else if (testType == 4)
        {
            GetKeyJump = Input.GetKeyDown("k");


            if (Input.GetKeyDown("l")) GetKeyHMove = 1;
            else if (Input.GetKeyDown("j")) GetKeyHMove = -1;
            else if (Input.GetKeyUp("l") || Input.GetKeyUp("j")) GetKeyHMove = 0;
        }
#else
        GetKeyJump = Input.GetButtonDown("Jump_" + this.tag);
        GetKeyHMove = Input.GetAxisRaw("Horizontal_" + this.tag);
        GetKeyVMove = Input.GetAxisRaw("Vertical_" + this.tag);
#endif

    }

    void PopInput()
    {
#if !JOYSTICK
        if (((Input.GetKeyDown("c") && testType == 1) || (Input.GetKeyDown("h") && testType == 2) || (Input.GetKeyDown("6") && testType == 3) || (Input.GetKeyDown("p") && testType == 4) && playerPop.canPop))
        {
            playerPop.PopDown();
        }
        if (((Input.GetKeyUp("c") && testType == 1) || (Input.GetKeyUp("h") && testType == 2) || (Input.GetKeyUp("6") && testType == 3) || (Input.GetKeyUp("p") && testType == 4) && playerPop.canPop))
        {
            playerPop.PopUp();
        }
#else
        if (Input.GetButtonDown("YButton_" + this.tag) && playerPop.canPop)
        {
            playerPop.PopDown();
        }
        if (Input.GetButtonUp("YButton_" + this.tag) && playerPop.canPop)
        {
            playerPop.PopUp();
        }
#endif
    }

    void StickInput()
    {
#if !JOYSTICK && !HOLDSTICK
        if ((Input.GetKeyDown("x") && testType == 1) || (Input.GetKeyDown("g") && testType == 2) || (Input.GetKeyDown("o") && testType == 4) || (Input.GetKeyDown("5") && testType == 3))
        {
            if (playerStick.canStick && !playerStick.m_isStick)
                playerStick.SetStick();
            else if (playerStick.m_isStick)
                playerStick.ResetStick();
        }
#elif JOYSTICK && !HOLDSTICK
        if (Input.GetButtonDown("Stick_" + this.tag))
        {
            if (playerStick.canStick && !playerStick.m_isStick)
                playerStick.SetStick();
            else if (playerStick.m_isStick)
                playerStick.ResetStick();
        }
#elif !JOYSTICK && HOLDSTICK
        if ((Input.GetKeyDown("x") && testtestType == 1) || (Input.GetKeyDown("g") && testtestType == 2) || (Input.GetKeyDown("o") && testtestType == 4) || (Input.GetKeyDown("5") && testtestType == 3))
        {
            if(playerStick.canStick && !playerStick.m_isStick)
                playerStick.SetStick();
        }
        if ((Input.GetKeyUp("x") && testtestType == 1) || (Input.GetKeyUp("g") && testtestType == 2) || (Input.GetKeyUp("o") && testtestType == 4) || (Input.GetKeyUp("5") && testtestType == 3))
        {
            if (playerStick.m_isStick)
                playerStick.ResetStick();
        }
#elif JOYSTICK && HOLDSTICK
        if (Input.GetButtonDown("Stick_" + this.tag))
        {
            if(playerStick.canStick && !playerStick.m_isStick)
                playerStick.SetStick();
        }
        if (Input.GetButtonUp("Stick_" + this.tag))
        {
            if (playerStick.m_isStick)
                playerStick.ResetStick();
        }
#endif

    }
}
