using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField]
    private int selectBigLevel;

    public GameObject bigLevelGroup;

    private Transform blg_trans;

    public Vector2 bl_distance;

    [SerializeField]
    private Vector2 blg_pos;

    [SerializeField]
    private Animator[] blList_anim;

    [SerializeField]
    private int bigLevelCount;

    //biglevelui

    public Text bigLevel_text;
    public GameObject[] bigLevel_btn;

    [SerializeField]
    private int selectSmallLevel;



    public bool rightInput;
    public bool leftInput;

    [SerializeField]
    private bool blg_move;


    void Start()
    {
        selectBigLevel = 1;
        selectSmallLevel = 1;

        rightInput = false;
        leftInput = false;

        blList_anim = bigLevelGroup.GetComponentsInChildren<Animator>();
        bigLevelCount = blList_anim.Length;

        blg_trans = bigLevelGroup.GetComponent<Transform>();
        blg_move = false;
    }
    void Update()
    {
        if (rightInput)
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

            rightInput = false;
        }

        if (leftInput)
        {
            if (selectBigLevel > 0)
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

            leftInput = false;
        }

    }
}
