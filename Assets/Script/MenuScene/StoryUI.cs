using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryUI : MonoBehaviour
{

    public GameObject storyImgGroup;
    public Text storyText;

	[SerializeField]
    List<string> story;

	[SerializeField]
    RawImage[] storyImgs;

    Color show;
    Color hide;

    int current;

    // Use this for initialization
    void Start()
    {
        storyImgs = storyImgGroup.GetComponentsInChildren<RawImage>();

        show = new Color(1, 1, 1, 1);
        hide = new Color(1, 1, 1, 0);

        current = 0;

		story = new List<string>();
        story.Add("一年一度的中秋節快到了，為了趕在中秋前做好麻糬，月兔們忙碌的開始了搗麻糬的工作。");
        story.Add("由於前一天熬夜搗麻糬搗了整個晚上，一隻月兔不慎放錯了材料，結果…");
        story.Add("發生了大爆炸！！！");
        story.Add("材料都因為大爆炸全部往四周飛散，辛苦做好的工作全部都功虧一簣。");
        story.Add("神奇的是因為放錯材料，居然讓落在附近的麻糬們擁有靈魂，可以任意操控自己的身體。");
        story.Add("於是這些麻糬們，被月兔賦予重要的使命，出發去收集四散在各地的小麻糬，讓搗麻糬作業可以回到正軌。");

        storyText.text = story[current];
		storyImgs[current].color = show;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a") || Input.GetButtonDown("AButton_player1"))
        {
            if (current == story.Count - 1)
            {
                SceneController.instance.LoadNextScene("Menu");
            }
            else
            {
                storyImgs[current].color = hide;
                current++;
                storyImgs[current].color = show;
                storyText.text = story[current];
            }
        }
    }
}
