using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Develop;

public class UIName
{
    /// <summary>登陆场景主界面</summary>
    public const string UILoginMainPanel = "MainPanel";
    /// <summary>单机游戏界面</summary>
    public const string UISinglePanel = "SinglePanel";
    /// <summary>线上游戏界面</summary>
    public const string UIOnlinePanel = "OnlinePanel";


    /// <summary>主场景</summary>
    public const string UIGameVersion = "UIScene_GameVersion";
    /// <summary>工具栏</summary>
    public const string UIToolBarPanel = "ToolbarPanel";
    /// <summary>中间系统提示信息</summary>
    public const string UISYSPromptPanel= "Panel_SYSPrompt ";
    /// <summary>方向舵UI</summary>
    public const string Part_Duo_Panel = "Part_Duo_Panel ";

}
public class UIManager : MonoSingleton<UIManager>
{

    private Dictionary<string, UIScene> mUIScene = new Dictionary<string, UIScene>();
    private Dictionary<UIAnchor.Side, GameObject> mUIAnchor = new Dictionary<UIAnchor.Side, GameObject>();

    public void InitializeUIs()
    {
        mUIAnchor.Clear();
        Object[] objs = FindObjectsOfType(typeof(UIAnchor));
        if (objs != null)
        {
            foreach (Object obj in objs)
            {
                UIAnchor uiAnchor = obj as UIAnchor;
                if (!mUIAnchor.ContainsKey(uiAnchor.side))
                    mUIAnchor.Add(uiAnchor.side, uiAnchor.gameObject);
            }
        }
        mUIScene.Clear();
        Object[] uis = FindObjectsOfType(typeof(UIScene));
        if (uis != null)
        {
            foreach (Object obj in uis)
            {
                UIScene ui = obj as UIScene;
                ui.SetVisible(false);
                mUIScene.Add(ui.gameObject.name, ui);
            }
        }
    }

    public void SetVisible(string name, bool visible)
    {
        if (visible && !IsVisible(name))
        {
            OpenScene(name);
        }
        else if (!visible && IsVisible(name))
        {
            CloseScene(name);
        }
    }

    public bool IsVisible(string name)
    {
        UIScene ui = GetUI(name);
        if (ui != null)
            return ui.IsVisible();
        return false;
    }
    private UIScene GetUI(string name)
    {
        UIScene ui;
        return mUIScene.TryGetValue(name, out ui) ? ui : null;
    }

    public T GetUI<T>(string name) where T : UIScene
    {
        return GetUI(name) as T;
    }

    private bool isLoaded(string name)
    {
        if (mUIScene.ContainsKey(name))
        {
            return true;
        }
        return false;
    }

    private void OpenScene(string name)
    {
        if (isLoaded(name))
        {
            mUIScene[name].SetVisible(true);
        }
    }
    private void CloseScene(string name)
    {
        if (isLoaded(name))
        {
            mUIScene[name].SetVisible(false);
        }
    }

    /// <summary>
    /// 显示一级界面
    /// </summary>
    /// <param name="visible"></param>
    public void SetGamePanelVisible(string uiname, bool visible)
    {
        SetVisible(uiname, visible);
    }


}
