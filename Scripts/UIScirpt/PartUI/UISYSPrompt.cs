using UnityEngine;
using System.Collections;

public class UISYSPrompt : UIScene
{
    private UILabel label;
    private bool show = false;
    void Start()
    {
        base.Start();
        label = Global.FindChild<UILabel>(transform, "Label");

    }


    public void Show()
    {
        if (!show)
        {
            SetVisible(true);
            show = true;
        }
    }

    public void Hide()
    {
        if (show)
        {
            SetVisible(false);
            show = false;
        }
    }


}
