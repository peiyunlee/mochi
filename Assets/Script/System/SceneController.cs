using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{

    public static SceneController instance;

    private Loading loading;

    private string activeScene;


    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        activeScene = "Start";
        loading = gameObject.GetComponent<Loading>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNextScene(string nextScene)
    {
        //Loading.instance.LoadingScene(nextScene);
        loading.LoadingScene(nextScene);
        //SceneManager.LoadScene(nextScene);
    }

    public void LoadNextScene(int nextScene)
    {
        loading.LoadingScene(nextScene);
        //Loading.instance.LoadingScene(nextScene);
        //SceneManager.LoadScene(nextScene);
    }
}
