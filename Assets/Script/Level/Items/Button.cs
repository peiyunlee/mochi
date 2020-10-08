using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Item
{
    private Transform btnTransform;

    [SerializeField]
    private bool isTrigger;
    public bool isClicked;

    public GameObject btnCollider;

    // Use this for initialization
    override protected void Start()
    {
        isClicked = false;
        isTrigger = false;
        btnTransform = this.gameObject.GetComponent<Transform>();
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

        if (this.gameObject.layer == 12){
            isTrigger = true;
        }
        else if (this.gameObject.layer == other.gameObject.layer)
        {
            isTrigger = true;
        }
        else{
            btnCollider.SetActive(true);
        }
    }
    override protected void OnTriggerExit2D(Collider2D other)
    {
        btnCollider.SetActive(false);
    }

    override protected void Action()
    {
        //做出反應
        //呼叫player的反應
        if (btnTransform.position.y > 1.8f)
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
