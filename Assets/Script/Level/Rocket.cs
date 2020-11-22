using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    LevelController levelController;

    [SerializeField]
    List<string> stickPlayer;

    void Start()
    {
        levelController = GameObject.Find("EventSystem").GetComponent<LevelController>();
    }

    public void SetPlayerStick(string player, bool set)
    {
        if (!stickPlayer.Contains(player) && set)
        {
            stickPlayer.Add(player);
        }
        else if (stickPlayer.Contains(player) && !set)
        {
            stickPlayer.Remove(player);
        }

        if (stickPlayer.Count == GameManager.instance.playerCount)
            RocketGo();
    }

    void RocketGo()
    {
        levelController.GameFinish();
    }
}
