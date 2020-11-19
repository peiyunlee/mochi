using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Item
{
    //PlayerMovement playerMovement;
    override protected void Start()
    {
        //playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    override protected void Update()
    {
    }

    override protected void OnTriggerEnter2D(Collider2D other)
    {
        // if (this.gameObject.layer != 12 && (this.gameObject.layer != other.gameObject.layer))
        // {
        //     if (other.gameObject.GetComponent<PlayerMovement>() != null)
        //     {
        //         other.gameObject.GetComponent<PlayerMovement>().Die();
        //     }

        // }
        if (this.gameObject.layer != 12 && (this.gameObject.layer != other.gameObject.layer))
        {
            if (other.gameObject.GetComponent<testPlayerMovement>() != null)
            {
                other.gameObject.GetComponent<testPlayerMovement>().Die();
            }

        }
        else
        {
            Debug.Log("arrow ok");
        }
    }


    override protected void Action()
    {
    }
}
