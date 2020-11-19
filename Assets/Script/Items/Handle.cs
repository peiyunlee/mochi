using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{
    GameObject player;
    public bool needMochi = true;

    void Update()
    {
        MochiDetect(player);

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            player = other.gameObject;
        }

    }
    // void OnCollisionExit2D(Collision2D other)
    // {
    //     if (other.gameObject.layer == LayerMask.NameToLayer("player"))
    //     {
    //         player = null;
    //         if (!needMochi)
    //         {
    //             needMochi = true;
    //         }
    //     }

    // }

    void MochiDetect(GameObject other)
    {
        // 判斷有沒有腳色黏上來
        if (other != null && other.layer == LayerMask.NameToLayer("player"))
        {
            if (other.GetComponent<JellySpriteReferencePoint>().ParentJellySprite.gameObject != null)
            {
                GameObject player = other.GetComponent<JellySpriteReferencePoint>().ParentJellySprite.gameObject;
                List<GameObject> stickItemList = player.GetComponent<PlayerStick>().stickItemList;
                if (player != null && stickItemList != null)
                {
                    if (stickItemList.Contains(this.gameObject))
                    {
                        needMochi = false;
                    }
                }
				else{
					needMochi = true;
				}
            }

        }
    }
}
