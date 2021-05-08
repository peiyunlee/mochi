using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    static public LevelController instance;

    //Billboard
    public BillboardController billboardController;

    //UI
    public LevelUIController levelUIController;

    public int[] levelIndex = new int[2];

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

    //mochi
    public int mochiTotalCount;

    int mochiCount;

    [SerializeField]

    public bool mochiAllGet;

    //rocket
    public GameObject rocketObjet;

    Rocket rocket;

    //timer
    int timeCount;

    //Die
    public GameObject diePoint;
    public Transform[] diePointPos;

    //Goal

    public int radishGoalCount;

    public int timeGoalCount_mms;

    public int deadGoalCount;

    public int deadTotalCount;

    [SerializeField]
    bool[] goal = new bool[3];

    bool showScore;

    bool showStartUI;

    void Start()
    {
        instance = this;

        levelUIController = gameObject.GetComponent<LevelUIController>();
        billboardController = gameObject.GetComponent<BillboardController>();

        rocket = rocketObjet.GetComponent<Rocket>();

        diePointPos = diePoint.GetComponentsInChildren<Transform>();

        for (int i = 0; i < GameManager.instance.playerCount; i++)
            playerPrefab[i].SetActive(true);

        radishCount = 0;
        mochiCount = 0;
        mochiAllGet = false;
        levelUIController.mochiTotalCount = mochiTotalCount;
        levelUIController.levelIndex = levelIndex;

        showScore = false;
        levelUIController.ShowStartUI();
        showStartUI = true;
        GameManager.instance.PauseGame();
    }


    void Update()
    {
        if ((Input.GetButtonDown("AButton_player1") || Input.GetButtonDown("AButton_player2")) || Input.GetKeyDown("a"))
        {
            if (showScore)
                GameFinish();
            else if (showStartUI)
                GameStart();
        }

        if (Input.GetKeyDown("q"))
        {
            playerPrefab[0].GetComponent<PlayerMovement>().Die();
        }
        // else if (Input.GetKeyDown("w"))
        // {
        //     playerPrefab[1].GetComponent<PlayerMovement>().Die();
        // }
    }

    void GameStart()
    {
        GameManager.instance.StartGame();
        levelUIController.HideStartUI();
        showStartUI = false;
        timeCount = 0;
        InvokeRepeating("Count", 1, 0.01f);

        audio_Background.Play();
    }

    void Count()
    {
        timeCount++;
        levelUIController.ShowTimeText(timeCount);
    }

    public void PauseTimer()
    {
        CancelInvoke("Count");
    }

    public void AddMochi()
    {
        mochiCount++;
        levelUIController.UpdateMochi(mochiCount);
        audio_Collection.Play();
        if (mochiCount == mochiTotalCount)
            mochiAllGet = true;
    }

    public void AddRadish()
    {
        radishCount++;
        levelUIController.UpdateRadish(radishCount);
        audio_Collection.Play();
    }
    public void SetGoal()
    {
        if (radishCount >= radishGoalCount)
            goal[0] = true;

        if (timeCount <= timeGoalCount_mms)
            goal[1] = true;

        if (deadTotalCount <= deadGoalCount)
            goal[2] = true;
    }

    public void ShowScore()
    {
        SetGoal();
        levelUIController.ShowScore(goal, deadTotalCount);

        audio_Background.Pause();
        audio_Finish.Play(3);
        showScore = true;
    }

    void GameFinish()
    {
        GradeData gd = new GradeData();
        gd.Set(levelIndex[0], levelIndex[1], radishCount, goal[0], goal[1], goal[2]);
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        GameManager.instance.time = timeCount;
        GameManager.instance.dataManager.Save(gd);

        if (GameManager.instance.dataManager.getLevelCountInfo[levelIndex[0]] >= levelIndex[1])
            ReturnToMenu();
        else
            SceneController.instance.LoadNextScene(next);
    }

    void ReturnToMenu()
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
}
