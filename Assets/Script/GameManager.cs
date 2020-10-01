using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public string currentLevel = "Level1-1";
    public int playerCount = 1;
    void Awake()
    {
        //轉換場景不會被刪除
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
    }
    void Update()
    {
    }

    public void SetCurrentLevel(string levelName)
    {
        currentLevel = levelName;
    }

    public void SetPlayerCount(int count)
    {
        playerCount = count;
    }
}
