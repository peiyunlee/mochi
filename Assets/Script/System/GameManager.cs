using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public string currentLevel = "Level1-1";
    public int playerCount = 1;

    public bool isPause;

    public int time;

    public DataManager dataManager;
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
        time = 0;
        dataManager = GetComponent<DataManager>();
    }
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            Restart(0);
        }
        else if (Input.GetKeyDown("2"))
        {
            Restart(1);
        }
        else if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown("p"))
        {
            Time.timeScale = 0;
        }
    }

    public void SetCurrentLevel(string levelName)
    {
        currentLevel = levelName;
    }

    public void SetPlayerCount(int count)
    {
        playerCount = count;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPause = true;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        isPause = false;
    }

    public void Restart(int scene)
    {
        SceneController.instance.LoadNextScene(scene);
        Time.timeScale = 1;
    }
}
