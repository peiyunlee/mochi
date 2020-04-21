using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Item
{
    public int _color;
    private Transform arrowTransform;
    public bool isStart;

    override protected void Start()
    {
        arrowTransform = this.gameObject.GetComponent<Transform>();
        color = _color;
    }

    // Update is called once per frame
    override protected void Update()
    {
        if (isStart)
        {
            Action();
        }
    }

    override protected void OnTriggerEnter2D(Collider2D other)
    {
    }

    override protected void Action()
    {
        //做出反應
        //呼叫player的反應
        if (arrowTransform.position.x > -9.5f)
        {
            arrowTransform.position += new Vector3(-2.5f * Time.deltaTime, 0, 0);
        }
        else
        {
            arrowTransform.position = new Vector3(9.0f, arrowTransform.position.y, 0);
            isStart = false;
        }
    }
}
