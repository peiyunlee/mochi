using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowButton : MonoBehaviour
{
    public Vector3 moveSpeed;

    public bool isMove;

    public bool isClicked;

    protected Transform trans;

    protected float upY, downY;

    public float startY;

    public AudioSource audio_Click;

    // public AudioSource audio_Up;

    bool audioClick;

    bool audioUp;


    public void SetIsMove(bool clicked)
    {
        if (isClicked == clicked)
        {
            isMove = true;
        }
    }

    void Start()
    {
        trans = this.gameObject.GetComponent<Transform>();

        upY = 0.13f;
        downY = -0.2f;

        if (isClicked)
        {
            audioClick = true;
        }
        else
        {
            audioUp = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player") && other.gameObject.name == "stickDetect" && !isMove)
        {
            if (!isClicked)
            {
                SetIsMove(false);
            }
        }
    }

    void Update()
    {
        if (isMove)
        {
            if (!audioClick && audioUp)
            {
                audioClick = true;
                audio_Click.Play();
            }
            // else if (!audioUp && audioClick)
            // {
            //     audioUp = true;
            //     audio_Up.Play();
            // }
            Action();
        }
    }

    void Action()
    {
        Vector3 pos = this.gameObject.transform.position;

        if (pos.y - startY < downY)
        {
            moveSpeed = -moveSpeed;
            isMove = false;
            isClicked = true;
            audioUp = false;
            pos.y = startY + downY;
        }

        Vector2 newPos = pos + moveSpeed * Time.deltaTime;

        trans.position = newPos;

    }

}
