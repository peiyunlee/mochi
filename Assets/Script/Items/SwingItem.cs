using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingItem : MonoBehaviour
{
    private float turnAngle = 270;
    private int ifTurn = -1;
    private float max, min;
    public float swingAngle;//擺盪角度
    public float swingSpeed;
    public float r;//距離中心位置半徑
    public bool clock;//順時針
    public GameObject middle;
    //public Rigidbody2D handle;
    public Transform center;
    private Rigidbody2D handle;
    public List<GameObject> stickPlayer;

    // Use this for initialization
    void Start()
    {
        handle = GetComponent<Rigidbody2D>();
        if (clock) ifTurn = -1;
        else ifTurn = 1;

        //r = Vector2.Distance(handle.transform.position, transform.position);
        r = Vector2.Distance(transform.position, center.transform.position);
        max = turnAngle + swingAngle;
        min = turnAngle - swingAngle;
    }

    void FixedUpdate()
    {
        turnAngle += ifTurn * swingSpeed * Time.deltaTime;

        if (turnAngle >= max)
        {
            ifTurn = -1;
            turnAngle = max;
        }
        else if (turnAngle <= min)
        {
            ifTurn = 1;
            turnAngle = min;
        }

        //handle position and rotation
        float x = r * Mathf.Cos(turnAngle * (Mathf.PI / 180)) + center.transform.position.x;
        float y = r * Mathf.Sin(turnAngle * (Mathf.PI / 180)) + center.transform.position.y;

        Vector2 position = new Vector2(x, y);

        Vector2 Dir = transform.position - center.transform.position;
        float rAngle = Vector2.SignedAngle(Vector2.up, Dir);

        handle.MoveRotation(rAngle);
        handle.MovePosition(position);
        //middle rotation
        Quaternion rotation = Quaternion.Euler(0, 0, (turnAngle - 270));
        middle.transform.rotation = rotation;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        MochiDetect(other);
        if(stickPlayer.Contains(other.gameObject)){
            PlayerForce(other.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(stickPlayer.Contains(other.gameObject)){
            PlayerForce(other.gameObject);
            stickPlayer.Remove(other.gameObject);
        }
    }
    void MochiDetect(Collider2D other)
    {
        // 判斷有沒有腳色黏上來
        if (other.gameObject.layer == LayerMask.NameToLayer("player") && other.gameObject.name == "stickDetect")
        {
            GameObject player = other.gameObject.GetComponentInParent<PlayerMovement>().gameObject;
            if (player != null)
            {
                List<GameObject> stickItemList = player.GetComponent<PlayerStick>().stickItemList;
                if (stickItemList != null)
                {
                    if (stickItemList.Contains(this.gameObject))
                    {
                        if(!stickPlayer.Contains(other.gameObject)){
                            stickPlayer.Add(other.gameObject);
                        }
                    }
                }
            }

        }
    }

    void PlayerForce(GameObject player){
        if(player.GetComponentInParent<JellySprite>()!=null){
            JellySprite playerJelly=player.GetComponentInParent<JellySprite>();
            Debug.Log(new Vector2(playerJelly.m_Mass*14*playerJelly.m_GravityScale*Mathf.Cos(turnAngle * (Mathf.PI / 180)),playerJelly.m_Mass*14*playerJelly.m_GravityScale*Mathf.Sin(turnAngle * (Mathf.PI / 180))));
            playerJelly.AddForce(new Vector2(playerJelly.m_Mass*14*playerJelly.m_GravityScale*Mathf.Cos(turnAngle * (Mathf.PI / 180)),playerJelly.m_Mass*14*playerJelly.m_GravityScale*Mathf.Sin(turnAngle * (Mathf.PI / 180))));
        }
    }
}
