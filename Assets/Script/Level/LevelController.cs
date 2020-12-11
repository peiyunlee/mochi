using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    static public LevelController instance;
    public List<GameObject> playerPrefab = new List<GameObject>(4);

    [SerializeField]
    int radishCount;

    public int mochiTotalCount;

    int mochiCount;

    [SerializeField]

    bool mochiAllGet;

    public Text mochiText;
    public Text radishText;

    public GameObject option;

    public GameObject rocketObjet;

    Rocket rocket;

    //timer
    int timeCount;

    public Text timeText;

    // Use this for initialization
    void Start()
    {
        instance = this;
        for (int i = 0; i < GameManager.instance.playerCount; i++)
            playerPrefab[i].SetActive(true);

        radishCount = 0;
        mochiCount = 0;
        mochiAllGet = false;

        mochiText.text = mochiCount + " / " + mochiTotalCount;
        radishText.text = radishCount + "";

        rocket = rocketObjet.GetComponent<Rocket>();

        timeCount = GameManager.instance.time;
        InvokeRepeating("Count", 1, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowOption();
        }
    }

    void Count()
    {
        timeCount++;
        ShowText();
    }

    void ShowText()
    {
        int s = timeCount % 100;
        int second = timeCount / 100 % 60;
        int minute = timeCount / 6000 % 60;
        string ss = "";
        if (s < 10)
        {
            ss = "0" + s;
        }
        else
        {
            ss = "" + s;
        }

        string ssecond = "";
        if (second < 10)
        {
            ssecond = "0" + second;
        }
        else
        {
            ssecond = "" + second;
        }

        string sminute = "";
        if (minute < 10)
        {
            sminute = "0" + minute;
        }
        else
        {
            sminute = "" + minute;
        }

        timeText.text = sminute + " : " + ssecond + " : " + ss;
    }

    public void AddRadish()
    {
        radishCount++;
        radishText.text = radishCount + "";
    }

    public void AddMochi()
    {
        mochiCount++;
        mochiText.text = mochiCount + " / " + mochiTotalCount;
        if (mochiCount == mochiTotalCount)
            mochiAllGet = true;
    }

    public void PauseTimer()
    {
        CancelInvoke("Count");
    }

    public void GameFinish()
    {
        if (mochiAllGet)
        {
            int next = SceneManager.GetActiveScene().buildIndex + 1;
            GameManager.instance.time = timeCount;
            SceneController.instance.LoadNextScene(next);
        }
    }

    public void ReturnToMenu()
    {
        SceneController.instance.LoadNextScene("Menu");
        //儲存進度
    }

    public void GameReturn()
    {
        Time.timeScale = 1;
        option.SetActive(false);
    }

    public void ShowOption()
    {
        Time.timeScale = 0;
        option.SetActive(true);
    }

    public void SetRocketStick(GameObject player, bool set, int color)
    {
        rocket.SetPlayerStick(player, set, color);
    }
}
