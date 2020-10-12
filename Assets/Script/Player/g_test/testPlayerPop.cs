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

    bool getKeyPop;

    void Start()
    {
        playerStick = gameObject.GetComponent<testPlayerStick>();
        playerMovement = gameObject.GetComponent<testPlayerMovement>();
        canPop = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (((Input.GetKeyDown("c") && playerMovement.testType == 1 ) || (Input.GetKeyDown("h") && playerMovement.testType ==2)|| (Input.GetKeyDown("6") && playerMovement.testType == 3)|| (Input.GetKeyDown("p") && playerMovement.testType == 4)) && canPop)
        // if (Input.GetButtonDown("Pop_" + this.tag) && canPop)
        {
            getKeyPop = true;
        }

        canPop = playerStick.isStick;
    }
    void FixedUpdate()
    {
        if (getKeyPop)
        {
            getKeyPop = false;
            Pop();
        }
    }

    void Pop()
    {

        if (playerStick.stickItemList.Count > 0)
        {
            List<GameObject> stickItemList = playerStick.stickItemList;
            playerStick.ResetItemNotStick();
            foreach (var item in stickItemList)
            {
                if (item.tag != "ground" && item.tag != "wall")
                {
                    PopItem(item);
                }
            }
        }

        if (playerStick.stickPlayerList.Count > 0)
        {
            List<GameObject> stickPlayerList = playerStick.stickPlayerList;
            List<GameObject> popPlayerList = new List<GameObject>();
            List<GameObject> withPlayerList = new List<GameObject>();
            foreach (var player in stickPlayerList)
            {
                bool isStick = player.GetComponent<testPlayerStick>().isStick;
                if (!isStick) //可以單獨彈出去的player
                {
                    popPlayerList.Add(player);
                }
                else if (isStick)
                {
                    withPlayerList.Add(player);
                }
            }

            if (popPlayerList.Count > 0)
            {
                playerStick.ResetThePlayersNotStick(popPlayerList);
                foreach (var player in popPlayerList)
                {
                    PopPlayer(player);
                }
            }

            if (withPlayerList.Count > 0)
            {
                //計算pop方向、給予每隻受力
                foreach (var player in withPlayerList)
                {
                    PopWithPlayer(player);
                }
                //重設黏
                playerStick.ResetNotStick_Normal();
            }
        }

    }

    void PopItem(GameObject item)
    {
        Vector2 slop = item.transform.position - this.gameObject.transform.position;
        item.GetComponent<Rigidbody2D>().velocity = slop * popForce;
    }

    void PopPlayer(GameObject player)
    {
        Vector2 slop = player.transform.position - this.gameObject.transform.position;
        player.GetComponent<testPlayerMovement>().Pop(slop, popForce * 0.5f);
    }

    void PopWithPlayer(GameObject player)
    {
        Vector2 slop = player.transform.position - this.gameObject.transform.position;
        // playerMovement.Pop(slop, popForce);
        player.GetComponent<testPlayerMovement>().Pop(slop, popForce * 3.0f);
    }
}
