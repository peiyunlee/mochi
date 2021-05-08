using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIController : MonoBehaviour
{
    public GameObject startUI;

    public GameObject scoreUI;
	Animator sui_Anim;
    Text[] suiLevel_Text;
    public GameObject coinGroup;
    Image[] coinsImage;

    public Text timeText;

    public Text mochiText;
    public Text radishText;

    public int timeCount;

    public int mochiCount;

    public int mochiTotalCount;

    public int radishCount;

    public int[] levelIndex = new int[2];
	
    bool showStartUI;


    // Use this for initialization
    void Start()
    {
        timeCount = 0;
        radishCount = 0;
        mochiCount = 0;

        // mochiText.text = mochiCount + " / " + mochiTotalCount;
        radishText.text = radishCount + "";

        suiLevel_Text = scoreUI.GetComponentsInChildren<Text>();
        sui_Anim = scoreUI.GetComponent<Animator>();
        coinsImage = coinGroup.GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateMochi(int count)
    {
        mochiCount = count;
        mochiText.text = count + " / " + mochiTotalCount;
    }

    public void UpdateRadish(int count)
    {
        radishCount = count;
        radishText.text = radishCount + "";
    }

    public void ShowStartUI()
    {
        startUI.SetActive(true);
    }

    public void HideStartUI()
    {
        startUI.SetActive(false);
    }

    public void ShowTimeText(int count)
    {
        timeCount = count;
        int s = count % 100;
        int second = count / 100 % 60;
        int minute = count / 6000 % 60;
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



    public void ShowScore(bool[] goalResult,int deadTotalCount)
    {

        int[] level = new int[2] { levelIndex[0] + 1, levelIndex[1] + 1 };
        //給UI數值
        suiLevel_Text[0].text = level[0] + "-" + level[1];
        suiLevel_Text[1].text = timeText.text;
        suiLevel_Text[2].text = radishText.text;
        suiLevel_Text[3].text = "" + deadTotalCount;

        //SHOWSCORE
        Color cShow = new Color(1f, 1f, 1f, 1f);
        Color cDark = new Color(0.5f, 0.5f, 0.5f, 0.5f);

        scoreUI.SetActive(true);
        if (goalResult[0])
            coinsImage[0].color = cShow;
        else coinsImage[0].color = cDark;
        if (goalResult[1])
            coinsImage[1].color = cShow;
        else coinsImage[1].color = cDark;
        if (goalResult[2])
            coinsImage[2].color = cShow;
        else coinsImage[2].color = cDark;

        sui_Anim.SetTrigger("show");
    }
}
