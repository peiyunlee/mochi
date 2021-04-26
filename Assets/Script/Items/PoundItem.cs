using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoundItem : MonoBehaviour
{
    public float downSpeed;
    public float upSpeed;
    public float stopTime;
    private float currentTime = 0;
    public float downPosY;
    private float initPosY;
    private float currentPosY;
    private float movePosY;
    int level = 0;
    bool isStick = false;
    private GameObject stickPlayer;
    public Rigidbody2D rb;
    bool isPound = false;
    // Use this for initialization
    void Start()
    {
        initPosY = rb.transform.position.y;
        stickPlayer = null;
        movePosY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        drop();

        StickPlayer();
    }

    void drop()
    {
        if (level == 0)
        {
            currentPosY = rb.transform.position.y;
            currentPosY -= Time.deltaTime * downSpeed;
            if (currentPosY <= downPosY)
            {
                isPound = true;
                currentPosY = downPosY;
                level = 1;
            }
            rb.transform.position = new Vector2(rb.transform.position.x, currentPosY);
        }
        else if (level == 1)
        {
            currentTime += Time.deltaTime * 10.0f;
            if (currentTime >= stopTime)
            {
                level = 2;
                currentTime = 0;
            }
        }
        else if (level == 2)
        {
            currentPosY = rb.transform.position.y;
            currentPosY += Time.deltaTime * upSpeed;
            movePosY += Time.deltaTime * upSpeed;
            if (currentPosY >= initPosY)
            {
                movePosY = 0;
                currentPosY = initPosY;
                level = 3;
            }
            rb.transform.position = new Vector2(rb.transform.position.x, currentPosY);
        }
        else if (level == 3)
        {
            currentTime += Time.deltaTime * 10.0f;
            if (currentTime >= stopTime)
            {
                level = 0;
                currentTime = 0;
            }
        }

    }

    void StickPlayer()
    {
        Transform player;
        if (stickPlayer != null)
        {
            if (isPound)
            {
                if (!stickPlayer.GetComponent<JellySpriteReferencePoint>().ParentJellySprite.GetComponent<PlayerMovement>().isDead)
                {
                    player = stickPlayer.GetComponent<Transform>();
                    player.position = new Vector2(player.position.x, player.position.y + movePosY);
                    if (movePosY >= 1)
                    {
                        isPound = false;
                        isStick = false;
                        stickPlayer = null;
                    }
                }
                else
                {
                    isPound = false;
                    isStick = false;
                    stickPlayer = null;
                }
            }

        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isStick)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("player"))
            {
                if (other.gameObject.GetComponent<JellySpriteReferencePoint>() != null)
                {
                    if (stickPlayer == null)
                    {
                        if (isPound)
                        {
							stickPlayer = other.gameObject;
                            if (stickPlayer.GetComponent<JellySpriteReferencePoint>().ParentJellySprite.GetComponent<PlayerMovement>().isDead)
                            {
                                stickPlayer = null;
                                isStick = false;
                            }
                            else
                            {
                                isStick = true;
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        // if (isStick)
        // {
        //     if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        //     {
        //         if (stickPlayer != null)
        //         {
        //             if (stickPlayer.GetComponent<JellySpriteReferencePoint>() != null)
        //             {
        //                 if (stickPlayer.GetComponent<JellySpriteReferencePoint>().ParentJellySprite.GetComponent<PlayerMovement>().isDead)
        //                 {
        //                     Debug.Log("dead");
        //                     isStick = false;
        //                     stickPlayer = null;
        //                 }
        //             }
        //         }

        //     }
        // }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        // if (isStick)
        // {
        //     if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        //     {
        //         if (stickPlayer != null)
        //         {
        //             if (stickPlayer.GetComponent<JellySpriteReferencePoint>() != null)
        //             {
        //                 if (stickPlayer.GetComponent<JellySpriteReferencePoint>().ParentJellySprite.GetComponent<PlayerMovement>().isDead)
        //                 {
        //                     Debug.Log("dead");
        //                     isStick = false;
        //                     stickPlayer = null;
        //                 }
        //             }
        //         }

        //     }
        // }
    }
}
