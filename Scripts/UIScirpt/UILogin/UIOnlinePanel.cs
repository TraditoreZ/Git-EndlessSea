using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class UIOnlinePanel : UIScene
{

    private Transform hostPanel;
    private Transform joinPanel;

    private UISceneWidget hostButton;
    private UISceneWidget joinButton;
    private UISceneWidget createButton;
    private UISceneWidget loadButton;
    private UISceneWidget joinToButton;
    private UIInput inputIp;

    void Start()
    {
        base.Start();

        hostPanel = Global.FindChild(transform, "HostSecPan");
        joinPanel = Global.FindChild(transform, "JoinSecPan");

        hostButton = Global.FindChild<UISceneWidget>(transform, "HostButton");
        joinButton = Global.FindChild<UISceneWidget>(transform, "JoinButton");
        createButton = Global.FindChild<UISceneWidget>(transform, "CreatButton");
        loadButton = Global.FindChild<UISceneWidget>(transform, "LoadButton");
        joinToButton = Global.FindChild<UISceneWidget>(transform, "JoinToButton");
        inputIp = Global.FindChild<UIInput>(transform, "IP");
        if (hostButton != null)
        {
            hostButton.OnMouseClick += OnButtonClick;
        }
        if (joinButton != null)
        {
            joinButton.OnMouseClick += OnButtonClick;
        }
        if (createButton != null)
        {
            createButton.OnMouseClick += OnButtonClick;
        }
        if (loadButton != null)
        {
            loadButton.OnMouseClick += OnButtonClick;
        }
        if (joinToButton != null)
        {
            joinToButton.OnMouseClick += OnButtonClick;
        }
        hostPanel.gameObject.SetActive(false);
        joinPanel.gameObject.SetActive(false);

    }

    private void OnButtonClick(UISceneWidget obj)
    {
        switch (obj.name)
        {
            case "HostButton":
                hostPanel.gameObject.SetActive(true);
                joinPanel.gameObject.SetActive(false);
                break;
            case "JoinButton":
                hostPanel.gameObject.SetActive(false);
                joinPanel.gameObject.SetActive(true);
                break;
            case "CreatButton":

                GameMain.instance.StartHost();

                break;
            case "LoadButton":

                break;
            case "JoinToButton":
                if (inputIp.value != null)
                {
                    NetworkManager.singleton.networkAddress = inputIp.value;
                    GameMain.instance.StartClient();
                }

                break;

            default:
                break;
        }
    }



}
