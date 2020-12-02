using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileButton : Btn {


    public GameObject buttonTile;

    ButtonTile buttonTileScript;
    public Vector3 moveSpeed;

    public bool isMove;

    public bool isClicked;

    protected Transform trans;

    protected float upY, downY;

    public float startY;
	

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
        buttonTileScript = buttonTile.GetComponent<ButtonTile>();

        upY = 0.13f;
        downY = -0.2f;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player") && other.gameObject.name == "stickDetect" && !isClicked && !isMove)
        {
            buttonTileScript.SetCanMove();
            SetIsMove(false);
        }
    }

    void Update()
    {
        if (isMove)
        {
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
        }

        else if (pos.y - startY > upY)
        {
            moveSpeed = -moveSpeed;
            isMove = false;
            isClicked = false;
        }

        Vector2 newPos = pos + moveSpeed * Time.deltaTime;

        trans.position = newPos;

    }

}
