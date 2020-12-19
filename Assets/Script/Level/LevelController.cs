using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    static public LevelController instance;

    //soundeffect

    public AudioSource audio_Collection;

    public AudioSource audio_Background;

    public AudioSource audio_Finish;

    //player
    public List<GameObject> playerPrefab = new List<GameObject>(2);

    //camera

    public MultipleTargetCamera multipleTargetCamera;

    //radish
    [SerializeField]
    int radishCount;
    public Text radishText;

    //mochi
    public int mochiTotalCount;

    int mochiCount;

    [SerializeField]

    public bool mochiAllGet;

    public Text mochiText;

    // public GameObject option;

    //rocket
    public GameObject rocketObjet;

    Rocket rocket;

    //timer
    int timeCount;

    public Text timeText;

    //Billboard
    public GameObject billboardObjet;

    //Die
    public GameObject diePoint;
    public Transform[] diePointPos;

    //Goal

    public int radishGoalCount;

    public int timeGoalCount_mms;

    public int deadGoalCount;

    public int deadTotalCount;

    [SerializeField]
    bool deadGoal;
    [SerializeField]
    bool radishGoal;
    [SerializeField]
    bool timeGoal;

    bool showScore;

    public GameObject score;

    public Text[] scoreText;

    Animator scoreAnim;

    public Transform coinGroup;

    public List<GameObject> coins;


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

        timeCount = 0;
        InvokeRepeating("Count", 1, 0.01f);

        diePointPos = diePoint.GetComponentsInChildren<Transform>();

        scoreText = score.GetComponentsInChildren<Text>();
        scoreAnim = score.GetComponent<Animator>();
        showScore = false;
        for (int i = 0; i < coinGroup.childCount; i++)
        {
            coins.Add(coinGroup.GetChild(i).gameObject);
        }

        audio_Background.Play();
    }


    void Update()
    {
        if (GameManager.instance.isPause && billboardObjet != null && (Input.GetButtonDown("AButton_player1") || Input.GetButtonDown("AButton_player2")))
        {
            billboardObjet.GetComponent<Billboard>().Hide();
            billboardObjet = null;
        }
        else if ((Input.GetButtonDown("AButton_player1") || Input.GetButtonDown("AButton_player2")) && showScore)
        {
            GameFinish();
        }

        if (Input.GetKeyDown("q"))
        {
            playerPrefab[0].GetComponent<PlayerMovement>().Die();
        }
        else if (Input.GetKeyDown("w"))
        {
            playerPrefab[1].GetComponent<PlayerMovement>().Die();
        }
        else if (Input.GetKeyDown("a") && billboardObjet != null && GameManager.instance.isPause)
        {
            billboardObjet.GetComponent<Billboard>().Hide();
        }
    }

    void Count()
    {
        timeCount++;
        ShowTimeText();
    }

    void ShowTimeText()
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
        audio_Collection.Play();
    }

    public void AddMochi()
    {
        mochiCount++;
        mochiText.text = mochiCount + " / " + mochiTotalCount;
        audio_Collection.Play();
        if (mochiCount == mochiTotalCount)
            mochiAllGet = true;
    }

    public void PauseTimer()
    {
        CancelInvoke("Count");
    }

    void GameFinish()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        GameManager.instance.time = timeCount;
        SceneController.instance.LoadNextScene(next);
    }

    public void ReturnToMenu()
    {
        SceneController.instance.LoadNextScene("Menu");
        //儲存進度
    }

    // public void GameReturn()
    // {
    //     Time.timeScale = 1;
    //     option.SetActive(false);
    // }

    // public void ShowOption()
    // {
    //     Time.timeScale = 0;
    //     option.SetActive(true);
    // }

    public void SetRocketStick(GameObject player, bool set, int color)
    {
        if (mochiAllGet)
            rocket.SetPlayerStick(player, set, color);
    }

    public void CameraRemoveTarget(Transform player)
    {
        multipleTargetCamera.RemoveTarget(player);
    }

    public void CameraAddTarget(Transform player)
    {
        multipleTargetCamera.AddTarget(player);
    }

    public void Goal()
    {
        if (radishCount >= radishGoalCount)
            radishGoal = true;

        if (timeCount <= timeGoalCount_mms)
            timeGoal = true;

        if (deadTotalCount <= deadGoalCount)
            deadGoal = true;
    }

    public void ShowScore()
    {
        Goal();     //判斷分數

        //給UI數值
        scoreText[0].text = GameManager.instance.currentLevel;
        scoreText[1].text = timeText.text;
        scoreText[2].text = radishText.text;
        scoreText[3].text = "" + deadTotalCount;

        //SHOWSCORE
        score.SetActive(true);
        if (radishGoal)
            coins[0].SetActive(true);
        if (timeGoal)
            coins[1].SetActive(true);
        if (deadGoal)
            coins[2].SetActive(true);

        scoreAnim.SetTrigger("show");
        audio_Background.Pause();
        audio_Finish.Play(3);
        showScore = true;
    }
}
