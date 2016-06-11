using UnityEngine;
using System.Collections;
using Develop;
using UnityEngine.SceneManagement;
using DevelopEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public class GameMain : MonoSingleton<GameMain>
{
    private AsyncOperation async;
    public static string sceneName;
    public static string uiSceneName;
    public static bool sceneIs3D;
    public static bool online;
    public static bool host;

    public GameQuality gamequality;
    public int ScreenX;
    public int ScreenY;
    public bool fullScreen;
    private FPSCalc fps;
    private Dictionary<string, Dictionary<string, string>> coreData;
    void Start()
    {
        gamequality = new GameQuality();
        LoadConfig();
        DontDestroyOnLoad(gameObject);
        fps = gameObject.AddComponent<FPSCalc>();
        Application.targetFrameRate = 30;

        Screen.SetResolution(ScreenX, ScreenY, fullScreen);

    }
    /// <summary>
    /// 加载配置项
    /// </summary>
    private void LoadConfig()
    {
        coreData = ConfigLoader.Load("preference.sea");
        string ScreenXstr = coreData["Core"]["resolutionX"];
        string ScreenYstr = coreData["Core"]["resolutionY"];
        ScreenX = int.Parse(ScreenXstr);
        ScreenY = int.Parse(ScreenYstr);

        if (coreData["Core"]["FullScreen"] == "True")
        {
            fullScreen = true;
        }
        else
        {
            fullScreen = false;
        }


        if (coreData["Core"]["FPS"] == "True")
        {
            fps.ShowFPS = true;
            fps.updateInterval = 0.5f;
        }
        else
        {
            fps.ShowFPS = false;
            fps.updateInterval = 0.5f;
        }


        if (coreData["Core"]["Debug"] == "True")
        {
            Debugger.showDebug = true;
            Debugger.Log("开启调试输出！");
        }

        try
        {
            var strQ = coreData["Quality"]["quality"];
            int quality = int.Parse(strQ);
            gamequality.SetQualityType((MQualityType)quality);
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    private IEnumerator Load()
    {
        async = SceneManager.LoadSceneAsync("Loading");
        yield return async;
    }

    public void LoadScene(string scene)
    {
        sceneIs3D = false;
        sceneName = scene;
        online = false;
        StartCoroutine(Load());
    }

    public void LoadScene(string scene, string uiScene)
    {
        sceneIs3D = true;
        sceneName = scene;
        uiSceneName = uiScene;
        online = false;
        StartCoroutine(Load());
    }

    public void LoadSceneNetWork(string scene, string uiScene)
    {
        sceneIs3D = true;
        sceneName = scene;
        uiSceneName = uiScene;
        online = true;
        StartCoroutine(Load());
    }

    public void ExitGame()
    {
        if (NetworkServer.active)
        {
            NetworkManager.singleton.StopServer();
        }
        if (NetworkClient.active)
        {
            NetworkManager.singleton.StopClient();
        }
    }

    public void StartHost()
    {
        host = true;
        //NetworkManager.singleton.StartHost();
        LoadSceneNetWork("World", "MainUI");

    }

    public void StartClient()
    {
        host = false;
        LoadSceneNetWork("World", "MainUI");

    }



}
