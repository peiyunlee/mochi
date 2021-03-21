using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField]
    private int selectBigLevel;

    [SerializeField]
    private int bigLevelCount;

    [SerializeField]
    private int selectSmallLevel;

    private bool isBig;

    //biglevelgroup

    public GameObject bigLevelGroup;

    private Transform blg_trans;

    [SerializeField]
    private Vector2 blg_pos;

    //bllist

    [SerializeField]
    private Animator[] blList_anim;

    [SerializeField]
    private Image[] blList_img;

    public Vector2 bl_distance;

    //biglevelui

    public GameObject bigLevelUI;

    private Animator blu_anim;

    public Text bigLevel_text;
    public GameObject[] bigLevel_btn;

    public Animator bluLine_anim;

    //smalllevelgroup
    public GameObject smallLevelGroup;

    [SerializeField]

    private List<GameObject> slList;

    [SerializeField]

    private Image[] slList_img;

    //smalllevelUI

    public GameObject smallLevelUI;

    private Animator slg_anim;

    //smalllevelui

    public Text sl_text;

    public Transform selectBorder_pos;




    [SerializeField]
    private bool blg_move;


    void Start()
    {
        selectBigLevel = 1;
        selectSmallLevel = 1;

        isBig = true;

        blList_anim = bigLevelGroup.GetComponentsInChildren<Animator>();
        bigLevelCount = blList_anim.Length;

        blList_img = bigLevelGroup.GetComponentsInChildren<Image>();
        slList_img = smallLevelGroup.GetComponentsInChildren<Image>();
        UpdateUI();

        blu_anim = bigLevelUI.GetComponent<Animator>();
        blu_anim.SetTrigger("selected");
        bluLine_anim.SetTrigger("start");

        blg_trans = bigLevelGroup.GetComponent<Transform>();
        blg_move = false;

        for (int i = 0; i < smallLevelGroup.transform.childCount; i++)
            slList.Add(smallLevelGroup.transform.GetChild(i).gameObject);
        slg_anim = smallLevelUI.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown("right"))
        {
            if (isBig)
                SelectRight_Big();
            else
                SelectRight_Small();
        }
        else if (Input.GetKeyDown("left"))
        {
            if (isBig)
                SelectLeft_Big();
            else
                SelectLeft_Small();
        }
        else if (Input.GetKeyDown("a") && isBig)
        {
            isBig = false;
            ShowSmallUI();
        }
        else if (Input.GetKeyDown("b") && !isBig)
        {
            isBig = true;
            HideSmallUI();
        }

    }

    void SelectRight_Big()
    {
        if (selectBigLevel < bigLevelCount)
        {
            blList_anim[selectBigLevel - 1].SetTrigger("unselected");
            blList_anim[selectBigLevel].SetTrigger("selected");
            selectBigLevel++;
            blg_pos -= bl_distance;
            blg_trans.position = blg_pos;
            bigLevel_text.text = "LEVLE " + selectBigLevel;
            if (selectBigLevel == bigLevelCount)
            {
                bigLevel_btn[0].SetActive(false);
            }
            else if (selectBigLevel == 2)
            {
                bigLevel_btn[1].SetActive(true);
            }
        }
    }

    void SelectLeft_Big()
    {
        if (selectBigLevel > 1)
        {
            blList_anim[selectBigLevel - 2].SetTrigger("selected");
            blList_anim[selectBigLevel - 1].SetTrigger("unselected");
            selectBigLevel--;
            blg_pos += bl_distance;
            blg_trans.position = blg_pos;
            bigLevel_text.text = "LEVLE " + selectBigLevel;
            if (selectBigLevel == 1)
            {
                bigLevel_btn[1].SetActive(false);
            }
            else if (selectBigLevel == bigLevelCount - 1)
            {
                bigLevel_btn[0].SetActive(true);
            }
        }
    }

    void SelectRight_Small()
    {
        if (selectSmallLevel < 4)
        {
            selectSmallLevel++;
            SelectBorderMove();
        }
    }

    void SelectLeft_Small()
    {
        if (selectSmallLevel > 1)
        {
            selectSmallLevel--;
            SelectBorderMove();
        }

    }

    void SelectBorderMove()
    {
        Vector3 border_pos = Vector3.zero;
        switch (selectSmallLevel % 3)
        {
            case 1:
                border_pos.x = -170.5f;
                break;
            case 0:
                border_pos.x = 170.5f;
                break;
            default:
                border_pos.x = 0f;
                break;

        }
        border_pos.y = 85f + (selectSmallLevel - 1) / 3 * (-158f);
        selectBorder_pos.position = border_pos;
    }

    void ShowSmallUI()
    {
        blu_anim.SetTrigger("unselected");

        sl_text.text = "LEVEL " + selectBigLevel;
        selectSmallLevel = 1;

        slList[selectBigLevel - 1].SetActive(true);
        smallLevelUI.SetActive(true);
        slg_anim.SetTrigger("selected");

    }

    void HideSmallUI()
    {
        smallLevelUI.SetActive(false);
        slList[selectBigLevel - 1].SetActive(false);
        slg_anim.SetTrigger("unselected");

        bigLevelUI.SetActive(true);
        blu_anim.SetTrigger("selected");
    }

    void UpdateUI()
    {
        PlayerData pd = GameManager.instance.dataManager.getPlayerData;
        int[] levelCount = GameManager.instance.dataManager.getLevelCountInfo;
        Color cHide = new Color(0f, 0f, 0f, 0f);
        Color cDark = new Color(0.5f, 0.5f, 0.5f);
        Color cShow = new Color(1f, 1f, 1f, 1f);
        int l = 0;
        //bigui
        for (int i = 0; i < levelCount.Length; i++)
        {
            if (i > pd.lastBigIndex)
            {
                blList_img[i * 2].color = cDark;
                blList_img[i * 2 + 1].color = cShow;
            }
            else
            {
                blList_img[i * 2].color = cShow;
                blList_img[i * 2 + 1].color = cHide;

                //smallui
                for (int j = 0; j < levelCount[i]; j++)
                {
                    if (j >= pd.lastSmallIndex && i == pd.lastBigIndex)
                    {
                        slList_img[j * 6 + l].color = cDark;
                        slList_img[j * 6 + l + 1].color = cShow;
                        for (int k = 2; k < 6; k++)
                        {
                            slList_img[j * 6 + l + k].color = cHide;
                        }
                    }
                    else
                    {
                        slList_img[j * 6 + l].color = cShow;
                        slList_img[j * 6 + l + 1].color = cHide;
                        for (int k = 0; k < 3; k++)
                        {
                            if (pd.gradeData[j + l].goal[k])
                                slList_img[j * 6 + l + k + 3].color = cShow;
                            else
                                slList_img[j * 6 + l + k + 3].color = cDark;
                        }
                    }
                }
            }
            l += levelCount[i];
        }

    }

}
