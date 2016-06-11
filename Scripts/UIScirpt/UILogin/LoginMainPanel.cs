using UnityEngine;
using System.Collections;

public class LoginMainPanel : UIScene
{

    UISceneWidget[] mButtons;
    void Start()
    {
        base.Start();
        var root = Global.FindChild(transform, "Grid");
        mButtons = root.GetComponentsInChildren<UISceneWidget>();
        if (mButtons != null)
        {
            for (int i = 0; i < mButtons.Length; i++)
            {
                mButtons[i].OnMouseClick += OnButtonOnClick;

            }
        }

    }

    private void OnButtonOnClick(UISceneWidget obj)
    {
        switch (obj.name)
        {
            case "Single player Button":
                UIManager.instance.GetUI<UISinglePanel>(UIName.UISinglePanel).SetVisible(true);
                UIManager.instance.GetUI<UIOnlinePanel>(UIName.UIOnlinePanel).SetVisible(false);
                break;
            case "Multiplayer game Button":
                UIManager.instance.GetUI<UISinglePanel>(UIName.UISinglePanel).SetVisible(false);
                UIManager.instance.GetUI<UIOnlinePanel>(UIName.UIOnlinePanel).SetVisible(true);
                break;
            case "Configure Button":

                break;
            case "Exit Button":

                break;
            default:
                break;
        }


    }



}
