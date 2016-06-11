using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class SceneLoading : MonoBehaviour
{

    private AsyncOperation async;
    private AsyncOperation asyncUI;
    public UILabel label;
    void Start()
    {

        if (GameMain.online)
        {
            if (GameMain.host)
            {
                Debug.Log("Host");
                NetworkManager.singleton.StartHost();
            }
            else
            {
                NetworkManager.singleton.StartClient();
            }

        }
        else
        {
            StartCoroutine(Load());
        }




    }

    IEnumerator Load()
    {

        if (GameMain.sceneIs3D)
        {
            async = SceneManager.LoadSceneAsync(GameMain.sceneName);
            asyncUI = SceneManager.LoadSceneAsync(GameMain.uiSceneName, LoadSceneMode.Additive);
        }
        else
        {
            async = SceneManager.LoadSceneAsync(GameMain.sceneName);
        }

        if (async != null && !async.isDone)
        {
            yield return async;
        }
        yield return asyncUI;
    }

    void Update()
    {
        //label.text = ((async.progress + 0.1f) * 100).ToString();
    }




}
