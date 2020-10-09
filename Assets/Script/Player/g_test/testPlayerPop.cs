using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerPop : MonoBehaviour
{

    testPlayerStick playerStick;
    testPlayerMovement playerMovement;
    void Start()
    {
        playerStick = gameObject.GetComponentInChildren<testPlayerStick>();
        playerMovement = gameObject.GetComponent<testPlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c") && playerStick.isStick)
        {
            ResetOtherPlayerStick();
            Pop();
        }
    }

    void Pop()
    {
        if (playerStick.stickPlayerList.Count > 0)
        {
            Vector2 slop = playerStick.stickPlayerList[0].transform.position - this.gameObject.transform.position;
            playerMovement.Pop(slop);
        }
    }

    void ResetOtherPlayerStick()
    {
        foreach (GameObject player in playerStick.stickPlayerList)
        {
            player.GetComponentInChildren<testPlayerStick>().ResetNotStick_Pop();
        }
    }
}
