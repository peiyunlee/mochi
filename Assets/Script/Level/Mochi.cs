using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mochi : MonoBehaviour
{

    bool isAte;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player") && !isAte)
        {
            Debug.Log("mochi");
            LevelController.instance.AddMochi();
            isAte = true;
            Destroy(this.gameObject);
        }
    }
}
