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
        story.Add("1");
        story.Add("2");
        story.Add("3");
        story.Add("4");

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
