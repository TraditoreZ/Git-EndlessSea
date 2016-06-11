using UnityEngine;
using System.Collections;

public class UIInfo : MonoBehaviour
{
    private UIManager mUiManager;
    void Start()
    {
        Object obj = FindObjectOfType(typeof(UIManager));
        if (obj != null)
            mUiManager = obj as UIManager;
        if (obj == null)
        {
            GameObject uimanager = new GameObject("UIManager");
            mUiManager = uimanager.AddComponent<UIManager>();
        }
        mUiManager.InitializeUIs();//初始化UI
        //mUiManager.SetGamePanelVisible(UIName.UIGameVersion, true);//显示开始游戏一级界面
        mUiManager.SetGamePanelVisible(UIName.UIToolBarPanel, true);

    }
}
