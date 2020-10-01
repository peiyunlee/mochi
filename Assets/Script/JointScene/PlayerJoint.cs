using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoint : MonoBehaviour
{

    [SerializeField]
    bool isReady = false;
    [SerializeField]
    int playerCount = 0;

    public List<GameObject> playerPrefab = new List<GameObject>(4);

    [SerializeField]
    int[] playerList = new int[4];
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isReady)
        {
            GameManager.instance.SetPlayerCount(playerCount);
            SceneController.instance.LoadNextScene(GameManager.instance.currentLevel);
        }

        if (Input.GetButtonDown("AButton_player1") && playerList[0] == 0)
            AddPlayer(1);
        else if (Input.GetButtonDown("BButton_player1") && playerList[0] != 0)
            RemovePlayer(1);


        if ((Input.GetButtonDown("AButton_player2") || Input.GetButtonDown("XButton_player1")) && playerList[1] == 0)
            AddPlayer(2);
        else if ((Input.GetButtonDown("BButton_player2")|| Input.GetButtonDown("YButton_player1")) && playerList[1] != 0)
            RemovePlayer(2);
    }



    public void AddPlayer(int playerNum)
    {
            playerList[playerNum - 1] = playerCount + 1;
            playerPrefab[playerList[playerNum - 1] - 1].transform.position = new Vector3(0, 0, 0);
            playerPrefab[playerList[playerNum - 1] - 1].SetActive(true);
            playerCount++;
    }

    public void RemovePlayer(int playerNum)
    {
        playerPrefab[playerList[playerNum - 1] - 1].SetActive(false);
        playerList[playerNum - 1] = 0;
        playerCount--;
    }
}
