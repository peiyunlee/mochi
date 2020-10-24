using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerPop : MonoBehaviour
{
    public float popForce;
    testPlayerStick playerStick;
    testPlayerMovement playerMovement;

    bool canPop;    //可以彈的情況：按黏才可以彈

    float popTime;

    public bool getKeyPop;
    public bool getKeyTurn;

    void Start()
    {
        playerStick = gameObject.GetComponentInChildren<testPlayerStick>();
        playerMovement = gameObject.GetComponent<testPlayerMovement>();
        canPop = false;
    }

    // Update is called once per frame
    void Update()
    {
        canPop = playerStick.isStick;
        if (((Input.GetKeyUp("c") && playerMovement.testType) || (Input.GetKeyUp("h") && playerMovement.testType)) && canPop)
        // if ((Input.GetKeyUp("h") && playerMovement.testType) || (Input.GetButtonUp("Pop_" + this.tag) && !playerMovement.testType) && canPop)
        {
            getKeyTurn = false;
            getKeyPop = true;
        }
        if (((Input.GetKeyDown("c") && playerMovement.testType) || (Input.GetKeyDown("h") && playerMovement.testType)) && canPop)
        // if ((Input.GetKeyDown("h") && playerMovement.testType) || (Input.GetButtonDown("Pop_" + this.tag) && !playerMovement.testType) && canPop)
        {
            getKeyTurn = true;
        }


    }
    void FixedUpdate()
    {

        Turn();

        if (getKeyPop)
        {
            // getKeyTurn = false;
            getKeyPop = false;
            Pop();
        }
    }

    void Turn()
    {
        if (getKeyTurn)
        {
            if (playerStick.stickPlayerList != null && playerStick.stickPlayerList.Count > 0)
            {
                List<GameObject> stickPlayerList = playerStick.stickPlayerList;
                foreach (var item in stickPlayerList)
                {
                    GetComponent<UnityJellySprite>().isStick = false;
                    item.GetComponent<UnityJellySprite>().CentralPoint.Body2D.freezeRotation = false;
                }
            }
        }
        else if (playerStick.isStick)//不轉&&黏住的狀態
        {
            if (playerStick.stickPlayerList != null && playerStick.stickPlayerList.Count > 0)
            {
                List<GameObject> stickPlayerList = playerStick.stickPlayerList;
                foreach (var item in stickPlayerList)
                {
                    item.GetComponent<UnityJellySprite>().CentralPoint.Body2D.freezeRotation = true;
                }
            }
        }
    }

    void Pop()
    {
        // if (playerStick.stickPlayerList.Count > 0)
        // {
        //     List<GameObject> stickItemList = playerStick.stickItemList;
        //     Vector2 slop = playerStick.stickPlayerList[0].transform.position - this.gameObject.transform.position;
        //     // playerMovement.Pop(slop);
        // }
        if (playerStick.stickPlayerList != null && playerStick.stickItemList.Count > 0)
        {
            List<GameObject> stickItemList = playerStick.stickItemList;
            ResetStickItem();
            foreach (var item in stickItemList)
            {
                if (item.tag != "ground" && item.tag != "wall")
                {
                    Vector2 slop = item.transform.position - this.gameObject.transform.position;
                    item.GetComponent<Rigidbody2D>().velocity = slop * popForce;
                }
            }
        }

    }

    void ResetOtherPlayerStick()
    {
        // foreach (GameObject player in playerStick.stickPlayerList)
        // {
        //     player.GetComponent<testPlayerStick>().ResetItemNotStick();
        // }
    }

    void ResetStickItem()
    {
        playerStick.ResetItemNotStick();
    }
}
