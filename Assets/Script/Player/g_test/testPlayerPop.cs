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

    void Start()
    {
        playerStick = gameObject.GetComponent<testPlayerStick>();
        playerMovement = gameObject.GetComponent<testPlayerMovement>();
        canPop = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (((Input.GetKeyDown("c") && playerMovement.testType) || (Input.GetKeyDown("h") && playerMovement.testType)) && canPop)
        // if (Input.GetButtonDown("Pop_" + this.tag) && canPop)
        {
            Debug.Log("input");
            Pop();
        }

        canPop = playerStick.isStick;
    }

    void Pop()
    {
        // if (playerStick.stickPlayerList.Count > 0)
        // {
        //     List<GameObject> stickItemList = playerStick.stickItemList;
        //     Vector2 slop = playerStick.stickPlayerList[0].transform.position - this.gameObject.transform.position;
        //     // playerMovement.Pop(slop);
        // }

        if (playerStick.stickItemList.Count > 0)
        {
            List<GameObject> stickItemList = playerStick.stickItemList;
            // ResetStickItem();
            Debug.Log(stickItemList);
            foreach (var item in stickItemList)
            {
                Vector2 slop = item.transform.position - this.gameObject.transform.position;
                item.GetComponent<Rigidbody2D>().AddForce(slop * popForce);
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
