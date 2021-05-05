using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    //public static Loading instance;
    [SerializeField]
    private GameObject loading;
    private Text loadingText;
    // Use this for initialization
    void Awake()
    {
        GetRef();
        // if (instance == null)
        // {
        //     instance = this;
        //     DontDestroyOnLoad(gameObject);
        //     //GetRef();
        // }
        // else
        // {
        //     Destroy(gameObject);
        // }
        // loading = GameObject.Find("load").gameObject;
        // if (loading != null)
        // {
        //     loadingText = loading.GetComponentInChildren<Text>();
        //     loading.SetActive(false);
        // }
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
        if(GameObject.FindGameObjectWithTag("Load")!=null)
            loading = GameObject.FindGameObjectWithTag("Load");
        if (loading != null)
        {
            loadingText = loading.GetComponentInChildren<Text>();
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
        AsyncOperation async = Application.LoadLevelAsync(sceneName);////(2)
        while (!async.isDone)////(3)
        {
            if (loading != null)
            {
                loading.SetActive(true);
                if (loadingText != null)
                {
                    loadingText.text = (Mathf.Round(async.progress * 100)).ToString() + "%";
                }
            }

            yield return null;
        }
    }
    IEnumerator DisplayLoadingScreen(string sceneName)////(1)
    {
        AsyncOperation async = Application.LoadLevelAsync(sceneName);////(2)

        while (!async.isDone)////(3)
        {
            if (loading != null)
            {
                loading.SetActive(true);
                if (loadingText != null)
                {
                    loadingText.text = (Mathf.Round(async.progress * 100)).ToString() + "%";
                }
            }
            yield return null;
        }
    }
}
