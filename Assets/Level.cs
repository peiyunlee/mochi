using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    public List<GameObject> playerPrefab = new List<GameObject>(4);
    // Use this for initialization
    void Start()
    {
        for (int i=0; i < GameManager.instance.playerCount; i++)
            playerPrefab[i].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
