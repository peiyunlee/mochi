using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    //public static Loading instance;
    [SerializeField]
    private GameObject loading;
    [SerializeField]
    private Image progress;
    [SerializeField]
    private Text loadingText;
    // Use this for initialization
    void Awake()
    {
        GetRef();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnLevelWasLoaded(int sceneName)
    {
        GetRef();
    }

    void OnLevelWasLoaded(string sceneName)
    {
        GetRef();
    }

    void GetRef()
    {
        //if (GameObject.FindGameObjectWithTag("Load") != null)
        loading = GameObject.FindGameObjectWithTag("Load");
        if (GameObject.FindGameObjectWithTag("progress") != null)
            progress = GameObject.FindGameObjectWithTag("progress").GetComponent<Image>();
        if (GameObject.FindGameObjectWithTag("loadText") != null)
            loadingText = GameObject.FindGameObjectWithTag("loadText").GetComponent<Text>();
        if (loading != null)
        {
            loading.SetActive(false);
        }
    }

    public void LoadingScene(int sceneName)
    {
        StartCoroutine(DisplayLoadingScreen(sceneName));
    }

    public void LoadingScene(string sceneName)
    {
        StartCoroutine(DisplayLoadingScreen(sceneName));
    }

    IEnumerator DisplayLoadingScreen(int sceneName)////(1)
    {
        int Progress = 0;
        int toProgress = 0;
        AsyncOperation async = Application.LoadLevelAsync(sceneName);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            loading.SetActive(true);
            toProgress = (int)async.progress * 100;
            while (Progress < toProgress)
            {
                Progress++;
                progress.fillAmount = Progress / 100f;
                loadingText.text = ((int)Progress).ToString() + "%";
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
        }
        toProgress = 100;
        while (Progress < toProgress)
        {
            Progress++;
            progress.fillAmount = Progress / 100f;
            loadingText.text = ((int)Progress).ToString() + "%";
            if (Progress >= 100) { async.allowSceneActivation = true; }
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator DisplayLoadingScreen(string sceneName)////(1)
    {
        int Progress = 0;
        int toProgress = 0;
        AsyncOperation async = Application.LoadLevelAsync(sceneName);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            loading.SetActive(true);
            toProgress = (int)async.progress * 100;
            while (Progress < toProgress)
            {
                Progress++;
                progress.fillAmount = Progress / 100f;
                loadingText.text = ((int)Progress).ToString() + "%";
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
        }
        toProgress = 100;
        while (Progress < toProgress)
        {
            Progress++;
            progress.fillAmount = Progress / 100f;
            loadingText.text = ((int)Progress).ToString() + "%";
            if (Progress >= 100) { async.allowSceneActivation = true; }
            yield return new WaitForEndOfFrame();
        }
    }
}
