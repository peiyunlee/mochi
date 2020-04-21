using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Item
{
    public int _color;
    private Transform btnTransform;

    [SerializeField]
    private bool isTrigger;
    public bool isClicked;

    // Use this for initialization
    override protected void Start()
    {
        isClicked = false;
        isTrigger = false;
        btnTransform = this.gameObject.GetComponent<Transform>();
        color = _color;
    }

    // Update is called once per frame
    override protected void Update()
    {
        if (!isClicked && isTrigger)
        {
            Action();
        }
    }

    override protected void OnTriggerEnter2D(Collider2D other)
    {
		
        if (color == (int)EColor.NONE)
            isTrigger = true;
        else
        {
            // if (other.gameObject.tag == "Player"+color)
            // {
			// 	isTrigger = true;
            // }
             if((this.gameObject.layer >> other.gameObject.layer & 1) == 1){
                 isTrigger = true;
             }
    }
        }
	}

    override protected void Action()
    {
        //做出反應
        //呼叫player的反應
        if (btnTransform.position.y > -0.31f)
        {
            //做出反應
            btnTransform.position += new Vector3(0, -2.5f * Time.deltaTime, 0);
        }
        else
        {
            isClicked = true;
        }
    }
}
