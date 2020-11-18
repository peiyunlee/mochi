using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public List<GameObject> playerPrefab = new List<GameObject>(4);
    
	[SerializeField]
	int radishCount;

    public int mochiTotalCount;

    int mochiCount;

    bool mochiAllGet;
    public bool isReady;

	public Text mochiText;
	public Text radishText;

	public GameObject option;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < GameManager.instance.playerCount; i++)
            playerPrefab[i].SetActive(true);

        radishCount = 0;
        mochiCount = 0;
        mochiAllGet = false;
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.Escape)){
			ShowOption();
		}
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

    public void GameFinish()
    {
        if (mochiAllGet)
        {
            int next = SceneManager.GetActiveScene().buildIndex + 1;
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


}
