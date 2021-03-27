using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    [SerializeField]
    private int[] levelCountInfo = new int[] { 2, 1 };  //第一大關4個小關、第二大關2個小關

    public int levelTotalCount;
    public int[] getLevelCountInfo { get { return levelCountInfo; } }
    public PlayerData getPlayerData { get { return playerData; } }

    [SerializeField]
    private PlayerData playerData;

    public bool testFinishLevel = false;

    public GradeData testGradeData;

    // Use this for initialization
    void Start()
    {
        playerData = new PlayerData();
        playerData.lastBigIndex = 0;
        playerData.lastSmallIndex = 1;
        playerData.gradeData = new List<GradeData>();
        levelTotalCount = 0;
        for (int i = 0; i < levelCountInfo.Length; i++)
        {
            GradeData gd = new GradeData();
            for (int j = 0; j < levelCountInfo[i]; j++)
            {
                gd.Set(i, j + 1, 0, false, false, false);
                playerData.gradeData.Add(gd);
            }
            levelTotalCount += levelCountInfo[i];
        }

        // //test
        // GradeData gd_test = new GradeData();
        // playerData.lastBigIndex = 0;    // 設定現在到第二關開啟
        // playerData.lastSmallIndex = 1;    // 設定現在到2-1開啟
        // gd_test.Set(0, 1, 2, true, false, false);
        // Save(gd_test);
    }

    // Update is called once per frame
    void Update()
    {
        if (testFinishLevel)
        {
            testFinishLevel = false;
            Save(testGradeData);
        }
    }

    public void Save(GradeData gradeData)
    {
        if (playerData.lastBigIndex == gradeData.bigIndex && playerData.lastSmallIndex == gradeData.smallIndex)    //玩最新關卡
        {
            if (gradeData.smallIndex < levelCountInfo[gradeData.bigIndex])    //不是最後的關卡
                playerData.lastSmallIndex++;
            else
            {
                playerData.lastBigIndex++;
                playerData.lastSmallIndex = 0;
            }
        }
        SaveGrade(gradeData);
    }

    private void SaveGrade(GradeData gradeData)
    {
        int level = playerData.lastSmallIndex;
        for (int i = 0; i < playerData.lastBigIndex; i++)
        {
            level += (i + 1) * levelCountInfo[i];
        }
        int levelIndex = level - 1;

        GradeData gd = new GradeData();
        int r = playerData.gradeData[levelIndex].radishCount;
        int b = playerData.gradeData[levelIndex].bigIndex;
        int s = playerData.gradeData[levelIndex].smallIndex;
        bool[] g = playerData.gradeData[levelIndex].goal;
        if (r < gradeData.radishCount)
            r = gradeData.radishCount;
        for (int i = 0; i < 3; i++)
        {
            if (g[i] == false)
            {
                g[i] = gradeData.goal[i];
            }

        }
        gd.Set(b, s, r, g[0], g[1], g[2]);
        playerData.gradeData[levelIndex] = gd;
    }

}

[System.Serializable]
public struct PlayerData
{
    public int lastBigIndex;
    public int lastSmallIndex;
    public List<GradeData> gradeData;
}

[System.Serializable]
public struct GradeData
{
    public int bigIndex;
    public int smallIndex;
    public int radishCount;
    public bool[] goal; //radish、time、death

    public void Set(int b, int s, int r, bool g1, bool g2, bool g3)
    {
        bigIndex = b;
        smallIndex = s;
        radishCount = r;
        goal = new bool[3];
        goal[0] = g1;
        goal[1] = g2;
        goal[2] = g3;
    }
}