using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radish : MonoBehaviour
{


    bool isAte;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player") && !isAte)
        {
            isAte = true;
            LevelController.instance.AddRadish();
            Destroy(this.gameObject);
        }
    }
}
