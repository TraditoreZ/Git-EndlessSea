﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Ceto;
using Develop;
using UnityEngine.SceneManagement;

public class WorldManager : MonoSingleton<WorldManager>
{

    // Use this for initialization
    public string terrainPath = "Terrain/";

    private Transform newPlayerPostation;
    [SerializeField]
    private Transform sun;
    void Start()
    {

        LoadTerrain();

        if (GameMain.online)
        {
            // LoadNetWorkPlayer();
            SceneManager.LoadSceneAsync(GameMain.uiSceneName, LoadSceneMode.Additive);
        }
        else
        {
            //LoadPlayer();
        }


        LoadSky();

        // LoadOcean();
    }

    void LoadTerrain()
    {
        var terrainObj = Resources.Load(terrainPath + "Birth Terrain");
        var terrainGameObj = Instantiate(terrainObj) as GameObject;
        newPlayerPostation = Global.FindChild(terrainGameObj.transform, "StartPosition");

    }


    void LoadPlayer()
    {
        var player = Resources.Load("Player/Player");
        GameObject gameObjPlayer = Instantiate(player) as GameObject;
        gameObjPlayer.transform.position = newPlayerPostation.position;
        gameObjPlayer.transform.rotation = newPlayerPostation.rotation;
        //NetworkManager.singleton.playerPrefab.
        gameObjPlayer.SetActive(true);
    }

    void LoadNetWorkPlayer()
    {
        Debug.Log("Net Player Create");

        Instantiate(NetworkManager.singleton.playerPrefab).SetActive(true);



    }


    void LoadSky()
    {
        //var skyObj = Resources.Load("World/Sky");
        //var sky = Instantiate(skyObj) as GameObject;
        //sky.name = "Tenkoku DynamicSky";
        //sun = Global.FindChild(sky.transform, "LIGHT_World");
        var skyObj = GameObject.Find("Tenkoku DynamicSky");
        if (skyObj)
        {
            var tenkoku = skyObj.GetComponent("Tenkoku Module") as TenkokuModule;
            if (tenkoku)
            {
                tenkoku.setTimeM = 300;
                tenkoku.autoTimeSync = true;
                tenkoku.calcTimeM = 300f;
                tenkoku.weather_cloudAltoStratusAmt = 0.144f;
                tenkoku.weather_cloudCirrusAmt = 0.4f;
                tenkoku.weather_cloudCumulusAmt = 0.100f;
                Debugger.Log("Sky Weather Begin");
            }
        }
    }

    void LoadOcean()
    {
        var oceanObj = Resources.Load("World/Ocean");
        var ocean = Instantiate(oceanObj) as GameObject;
        ocean.GetComponent<Ocean>().m_sun = sun.gameObject;
    }



}

