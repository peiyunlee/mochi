using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerPop : MonoBehaviour
{
    public float popForce;
    testPlayerStick playerStick;
    testPlayerMovement playerMovement;
    private UnityJellySprite jellySprite;

    bool canPop;    //可以彈的情況：按黏才可以彈

    float popTime;

    public bool getKeyPop;
    public bool canTurn;

    void Start()
    {
        // playerStick = gameObject.GetComponentInChildren<testPlayerStick>();
        playerStick = gameObject.GetComponent<testPlayerStick>();
        playerMovement = gameObject.GetComponent<testPlayerMovement>();
        jellySprite = gameObject.GetComponent<UnityJellySprite>();
        canPop = false;
    }

    // Update is called once per frame
    void Update()
    {

        canPop = playerStick.isStick && (playerStick.getIsOnFloor || playerStick.isPointAttachWall);
        // Debug.Log(playerStick.getIsOnFloor || playerStick.isPointAttachWall);

        if (((Input.GetKeyDown("c") && playerMovement.testType == 1) || (Input.GetKeyDown("h") && playerMovement.testType == 2) || (Input.GetKeyDown("6") && playerMovement.testType == 3) || (Input.GetKeyDown("p") && playerMovement.testType == 4)) && canPop)
        {
            //讓對方轉
            canTurn = true;
            jellySprite.SetPlayerRot(playerStick.stickPlayerList);
        }

        // if (((Input.GetKey("c") && playerMovement.testType == 1) || (Input.GetKey("h") && playerMovement.testType == 2) || (Input.GetKey("6") && playerMovement.testType == 3) || (Input.GetKey("p") && playerMovement.testType == 4)) && !canPop)
        // {
        //     //讓對方轉
        //     canTurn = false;
        //     jellySprite.ResetPlayerRot(playerStick.stickPlayerList);
        // }

        if (((Input.GetKeyUp("c") && playerMovement.testType == 1) || (Input.GetKeyUp("h") && playerMovement.testType == 2) || (Input.GetKeyUp("6") && playerMovement.testType == 3) || (Input.GetKeyUp("p") && playerMovement.testType == 4)) && canPop)
        {
            canTurn = false;
            jellySprite.ResetPlayerRot(playerStick.stickPlayerList);
            getKeyPop = true;
        }
    }

    void FixedUpdate()
    {

        Turn();

        if (getKeyPop)
        {
            getKeyPop = false;
            Pop();
        }
        // Debug.Log(this.tag + ":" + jellySprite.sticked);
    }

    void Turn()
    {
        if (canTurn)
        {
            //按彈時unfreeze對方的rotation
            if (playerStick.stickPlayerList != null && playerStick.stickPlayerList.Count > 0)
            {
                List<GameObject> stickPlayerList = playerStick.stickPlayerList;
                foreach (var player in stickPlayerList)
                {
                    player.GetComponent<UnityJellySprite>().CentralPoint.Body2D.freezeRotation = false;
                    if (GetComponent<UnityJellySprite>().CentralPoint.GameObject.GetComponent<HingeJoint2D>().enabled)
                        GetComponent<UnityJellySprite>().CentralPoint.GameObject.GetComponent<HingeJoint2D>().enabled = false;
                }
            }
        }
        else if (playerStick.isStick && !canTurn)
        {
            //黏住的時候&&沒有按彈時freeze對方的rotation
            if (playerStick.stickPlayerList != null && playerStick.stickPlayerList.Count > 0)
            {
                List<GameObject> stickPlayerList = playerStick.stickPlayerList;
                foreach (var player in stickPlayerList)
                {
                    player.GetComponent<UnityJellySprite>().CentralPoint.Body2D.freezeRotation = true;

                }
            }
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
                foreach (var player in popPlayerList)
                {
                    playerStick.ResetThePlayersNotStick(player);
                    PopPlayer(player);
                }
            }

            if (withPlayerList.Count > 0)
            {
                //重設黏
                playerStick.ResetFloorStick();
                playerStick.ResetWallStick();
                Vector2 slop = Vector2.zero;
                foreach (var player in withPlayerList)
                {
                    Vector2 slop_p = player.transform.position - this.gameObject.transform.position;
                    slop += slop_p;
                    slop = slop / Mathf.Sqrt(Mathf.Pow(slop.x, 2) + Mathf.Pow(slop.y, 2));
                    PopWithPlayer(player);
                }
                // playerMovement.Pop(slop * 1.5f, popForce);
                //重設黏
                playerStick.ResetNotStick_PopWithPlayer();
            }
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
        player.GetComponent<testPlayerStick>().isPop = true;
        Vector2 slop = player.gameObject.transform.position - this.gameObject.transform.position;
        slop = slop / Mathf.Sqrt(Mathf.Pow(slop.x, 2) + Mathf.Pow(slop.y, 2));
        player.GetComponent<testPlayerMovement>().Pop(slop, popForce);
    }

    // void PopWithPlayer(Vector2 slop)
    // {
    //     // playerMovement.Pop(slop * 2.5f, popForce * 0.5f);
    // }

    void PopWithPlayer(GameObject player)
    {
        playerStick.isPop = true;
        player.GetComponent<testPlayerStick>().isPop = true;
        Vector2 slop = player.gameObject.transform.position - this.gameObject.transform.position;
        slop = slop / Mathf.Sqrt(Mathf.Pow(slop.x, 2) + Mathf.Pow(slop.y, 2));
        // player.GetComponent<testPlayerMovement>().Pop(slop * 1.5f, popForce * 0.25f);
        player.GetComponent<testPlayerMovement>().Pop(slop, popForce * 2f);
    }
}