using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    LevelController levelController;

    [SerializeField]
    List<GameObject> stickPlayer;

    [SerializeField]
    List<int> playerIndex;

    [SerializeField]
    public List<GameObject> playerSprite;
    public Animator rocketAnim;


    void Start()
    {
        levelController = GameObject.Find("EventSystem").GetComponent<LevelController>();
    }

    public void SetPlayerStick(GameObject player, bool set, int color)
    {
        if (!stickPlayer.Contains(player) && set)
        {
            stickPlayer.Add(player);
            playerIndex.Add(color-8);
        }
        else if (stickPlayer.Contains(player) && !set)
        {
            stickPlayer.Remove(player);
            playerIndex.Remove(color-8);
        }

        if (stickPlayer.Count == GameManager.instance.playerCount)
            RocketGo();
    }

    void RocketGo()
    {
        int index = 0;
        foreach (var player in stickPlayer)
        {
            playerSprite[playerIndex[index]].SetActive(true);
            playerSprite[playerIndex[index]].transform.position = player.transform.position;
            player.SetActive(false);
            index++;
        }
        rocketAnim.SetTrigger("start");
        Invoke("NextScene", 3);
    }

    void NextScene()
    {
        levelController.GameFinish();
    }
}
