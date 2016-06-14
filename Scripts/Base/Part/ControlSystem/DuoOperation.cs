using UnityEngine;
using System.Collections;
using ControlSystem;
using System;
using Assets.Scripts.Base.Build;
using UnityStandardAssets.Characters.FirstPerson;

public class DuoOperation : MonoBehaviour, IControl
{
    public delegate void MoveControl(float forward, float direction);
    public event MoveControl DuoMoveControl;

    private PartDuoUIScene duoUI;

    //舵转轴
    private Transform axis;
    private bool open = false;
    private Transform Player;
    private Transform playerPosPrefab;

    //前进方向变速器  范围-1 ---  1
    private float transmission;
    //转向器  范围-1 ---  1  左--右
    private float diverter;
    public void UserPart(Transform player)
    {
        open = true;
        Player = player;
        duoUI = PartDuoUIScene.instance;
        duoUI.SetVisible(true);
        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponent<playerAnimManager>().enabled = false;
        player.GetComponent<BuildingManager>().enabled = false;
        Camera.main.transform.localRotation = Quaternion.identity;
    }

    public void ExitPart(Transform player)
    {
        open = false;
        Player = null;
        duoUI.SetVisible(false);
        player.GetComponent<FirstPersonController>().enabled = true;
        player.GetComponent<playerAnimManager>().enabled = true;
        player.GetComponent<BuildingManager>().enabled = true;
    }

    // Use this for initialization
    void Start()
    {
        axis = Global.FindChild(transform, "Duo 1");
        playerPosPrefab = Global.FindChild(transform, "PlayerPos");
    }

    void Update()


    {
        if (!open)
        {
            return;
        }
        if (Player != null)
        {
            Player.position = playerPosPrefab.position;
            Player.rotation = playerPosPrefab.rotation;
        }




        if (Input.GetKeyDown(KeyCode.W))
        {
            if (transmission < 1)
            {
                transmission += 0.25f;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (transmission > -0.25f)
            {
                transmission -= 0.25f;
            }
        }
        //左右表达式
        diverter = Mathf.Clamp(diverter += Input.GetAxis("Horizontal") * 0.4f * Time.deltaTime, -1, 1);
        if (diverter < 1 && diverter > -1)
        {
            axis.Rotate(Vector3.forward * Time.deltaTime * -Input.GetAxis("Horizontal") * 200, Space.Self);
        }




        // 读取变速器调整UI
        duoUI.SetDirectionUI(diverter);
        duoUI.SetForwardUI(transmission);

        //发送事件
        if (DuoMoveControl != null)
        {
            DuoMoveControl(transmission, diverter);
        }

    }


    public void AddEvent(IObjControllerManager objManager)
    {
        DuoMoveControl += objManager.DuoMoveControl;
    }

    public void RemoveEvent(IObjControllerManager objManager)
    {
        DuoMoveControl -= objManager.DuoMoveControl;
    }
}
