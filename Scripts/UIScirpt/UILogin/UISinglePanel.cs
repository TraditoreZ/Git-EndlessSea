using UnityEngine;
using System.Collections;

public class UISinglePanel : UIScene
{

    private UISceneWidget creatButton;
    private UISceneWidget loadButton;
    void Start()
    {
        base.Start();
        creatButton = Global.FindChild<UISceneWidget>(transform, "CreatButton");
        loadButton = Global.FindChild<UISceneWidget>(transform, "LoadButton");
        if (creatButton != null)
        {
            creatButton.OnMouseClick = OnCreatButtonClick;
        }
        if (loadButton != null)
        {
            loadButton.OnMouseClick = OnLoadButtonClick;
        }



    }

    private void OnCreatButtonClick(UISceneWidget obj)
    {
        GameMain.instance.LoadScene("World", "MainUI");
    }
    private void OnLoadButtonClick(UISceneWidget obj)
    {

    }


}
