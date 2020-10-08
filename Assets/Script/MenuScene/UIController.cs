using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject bigLevelUI;
    Transform bigLevelTrans;
    // public List<Transform> bigLevelUIList;
    public GameObject smallLevelUI;
    public GameObject cam;
    Animator camAnim;


    public List<GameObject> arrow;

    float currentCamSpeed;

    public float camSpeed;

    [SerializeField]
    int currentLevel;

    int maxLevel;

    bool isBigMenu;

    [SerializeField]
    int inputtimer;

    int horizontalAxisRaw;

    void Awake()
    {
        camAnim = cam.GetComponent<Animator>();
        bigLevelTrans = bigLevelUI.GetComponent<Transform>();
        // for(int i = 0 ; i < maxLevel ; i++){
        //     bigLevelUIList.Add(bigLevelUI.transform.GetChild(i));
        // }
    }

    // Use this for initialization
    void Start()
    {
        currentLevel = 1;
        maxLevel = 4;
        camSpeed = 340.0f / 1.0f;
        currentCamSpeed = camSpeed;
        isBigMenu = true;
        inputtimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal_player1") == 1)
        {
            if(inputtimer == 0)
                StartCoroutine("InputTimer");
            if (inputtimer % 2 == 0)
                horizontalAxisRaw = 1;
        }
        else if (Input.GetAxisRaw("Horizontal_player1") == 0)
        {
            inputtimer = 0;
            horizontalAxisRaw = 0;
        }
        else if (Input.GetAxisRaw("Horizontal_player1") == -1)
        {
            InvokeRepeating("InputTimer", 0f, 1f);
            if (inputtimer % 2 == 0)
                horizontalAxisRaw = -1;
        }
        if ((horizontalAxisRaw == 1 || Input.GetKeyDown("right")) && currentLevel < maxLevel && isBigMenu)
        {
            currentLevel++;
            currentCamSpeed = camSpeed;
            camAnim.enabled = false;
            if (currentLevel == maxLevel)
            {
                arrow[1].SetActive(false);
            }
            else if (currentLevel == maxLevel - 1)
            {
                arrow[1].SetActive(true);
            }
            else if (currentLevel == 2)
            {
                arrow[0].SetActive(true);
            }
        }
        else if ((horizontalAxisRaw == -1 || Input.GetKeyDown("left")) && currentLevel > 1 && isBigMenu)
        {
            currentLevel--;
            currentCamSpeed = -camSpeed;
            camAnim.enabled = false;
            if (currentLevel == 1)
            {
                arrow[0].SetActive(false);
            }
            else if (currentLevel == 2)
            {
                arrow[0].SetActive(true);
            }
            else if (currentLevel == maxLevel - 1)
            {
                arrow[1].SetActive(true);
            }
        }

        if (Input.GetKeyDown("a") && !isBigMenu)
        {
            BackLevelMenu();
        }

        // Zoom();
    }
    void FixedUpdate()
    {
        CamMove();
    }

    void CamMove()
    {
        float newPosX;
        if (currentCamSpeed > 0 && bigLevelTrans.position.x > -340.0f * (currentLevel - 1))
        {
            newPosX = bigLevelTrans.position.x - currentCamSpeed * Time.deltaTime;
            bigLevelTrans.position = new Vector3(newPosX, bigLevelTrans.position.y, bigLevelTrans.position.z);
        }
        else if (currentCamSpeed < 0 && bigLevelTrans.position.x < -340.0f * (currentLevel - 1))
        {
            newPosX = bigLevelTrans.position.x - currentCamSpeed * Time.deltaTime;
            bigLevelTrans.position = new Vector3(newPosX, bigLevelTrans.position.y, bigLevelTrans.position.z);
        }
    }

    void Zoom()
    {
    }

    public void BigLevelBtnClick(int num)
    {
        isBigMenu = false;
        camAnim.enabled = true;
        camAnim.SetTrigger("up");
    }

    public void BackLevelMenu()
    {
        isBigMenu = true;
        camAnim.SetTrigger("down");
    }

    void InputTimer()
    {
        inputtimer++;
    }

}
