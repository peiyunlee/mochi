using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDetect : MonoBehaviour
{
    public int playerColor;

    PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!playerMovement.isDead && !playerMovement.isInvincible)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("player"))
            {
                if ((other.gameObject.layer != playerColor && (other.gameObject.layer <= 11 && other.gameObject.layer >= 8)) || other.gameObject.tag == "die")
                {
                    playerMovement.Die();
                }
            }
        }
    }
}
