using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDetect : MonoBehaviour
{
    [SerializeField]
    int playerColor;

    PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
        string playerColor_s = playerMovement.playerColor;
        switch (playerColor_s)
        {
            case "red":
                playerColor = 8;
                break;
            case "blue":
                playerColor = 10;
                break;
            case "green":
                playerColor = 11;
                break;
            case "yellow":
                playerColor = 9;
                break;
            default:
                playerColor = 0;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("player"))
        {
            if (other.gameObject.layer != playerColor && (other.gameObject.layer <= 11 &&　other.gameObject.layer >= 8))
            {
                playerMovement.Die();
            }
        }
    }
}
