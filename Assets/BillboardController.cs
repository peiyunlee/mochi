using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardController : MonoBehaviour
{


    //Billboard
    public int inNumber;
    public GameObject billboardGroup;

    [SerializeField]
    Billboard[] billboards;

    bool getKeyConfirm = false;
    public string inPlayer = "player1";
    // Use this for initialization
    void Start()
    {
        if (billboardGroup != null)
            billboards = billboardGroup.GetComponentsInChildren<Billboard>();
    }
    void Update()
    {
        if (inNumber != 0)
        {
            if (Input.GetButtonDown("AButton_" + inPlayer) || Input.GetKeyDown("a"))
            {
                getKeyConfirm = !getKeyConfirm;
            }

            if (getKeyConfirm)
            {
                billboards[inNumber - 1].GetComponent<Billboard>().Show();
            }
            else if (!getKeyConfirm)
            {
                billboards[inNumber - 1].GetComponent<Billboard>().Hide();
            }
        }
        // if (GameManager.instance.isPause && inNumber != 0 && (Input.GetButtonDown("AButton_player1") || Input.GetButtonDown("AButton_player2") || Input.GetKeyDown("a")))
        // {
        //     billboards[inNumber-1].GetComponent<Billboard>().Hide();
        // }
    }
}
