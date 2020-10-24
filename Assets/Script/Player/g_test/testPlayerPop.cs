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

    //aa
    // float stickTimer;

    // public float a;
    //aa

    void Start()
    {
        playerStick = gameObject.GetComponent<testPlayerStick>();
        playerMovement = gameObject.GetComponent<testPlayerMovement>();
        canPop = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (((Input.GetKeyDown("c") && playerMovement.testType == 1) || (Input.GetKeyDown("h") && playerMovement.testType == 2) || (Input.GetKeyDown("6") && playerMovement.testType == 3) || (Input.GetKeyDown("p") && playerMovement.testType == 4)) && canPop)
        // if (Input.GetButtonDown("Pop_" + this.tag) && canPop)
        {
            getKeyPop = true;
        }

        canPop = playerStick.isStick;


        //aa
        // if(playerStick.isPopPlayer){
        //     stickTimer ++;
        // }
        // if(stickTimer >= a){
        //     playerStick.isPopPlayer = false;
        //     stickTimer = 0;
        // }
        //aa
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

        if (playerStick.stickPlayerList != null)
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
                playerStick.isPopPlayer = true;
                int index = 0;
                foreach (var player in popPlayerList)
                {
                    playerStick.ResetThePlayersNotStick(index);
                    PopPlayer(player);
                    index++;
                }
                playerStick.stickPlayerList.Clear();
            }

            // if (withPlayerList.Count > 0)
            // {
            //     //重設黏
            //     playerStick.ResetFloorOrWallStick();
            //     Vector2 slop = Vector2.zero;
            //     foreach (var player in withPlayerList)
            //     {
            //         Vector2 slop_p = player.transform.position - this.gameObject.transform.position;
            //         slop += slop_p;
            //         slop = slop / Mathf.Sqrt(Mathf.Pow(slop.x, 2) + Mathf.Pow(slop.y, 2));
            //         PopWithPlayer(player);
            //     }
            //     // playerMovement.Pop(slop * 1.0f, popForce * 0.5f);
            //     //重設黏
            //     // playerStick.ResetNotStick_PopWithPlayer();
            // }
        }

    }

    void PopItem(GameObject item)
    {
        Vector2 slop = item.transform.position - this.gameObject.transform.position;
        slop = slop / Mathf.Sqrt(Mathf.Pow(slop.x, 2) + Mathf.Pow(slop.y, 2));
        item.GetComponent<Rigidbody2D>().velocity = slop * popForce;
    }

    void PopPlayer(GameObject player)
    {
        Vector2 slop = player.transform.position - this.gameObject.transform.position;
        slop = slop / Mathf.Sqrt(Mathf.Pow(slop.x, 2) + Mathf.Pow(slop.y, 2));
        player.GetComponent<testPlayerMovement>().Pop(slop, popForce);
        Debug.Log("pop" + player);
    }

    void PopWithPlayer(Vector2 slop)
    {
        // playerMovement.Pop(slop * 2.5f, popForce * 0.5f);
    }

    void PopWithPlayer(GameObject player)
    {
        Vector2 slop = player.transform.position - this.gameObject.transform.position;
        slop = slop / Mathf.Sqrt(Mathf.Pow(slop.x, 2) + Mathf.Pow(slop.y, 2));
        player.GetComponent<testPlayerMovement>().Pop(slop * 2.5f, popForce * 0.5f);
    }
}
