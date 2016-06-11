using UnityEngine;
using System.Collections;

public class PartDuoUIScene : UIScene
{
    Transform flag;
    Transform arrow;
    public static PartDuoUIScene instance;
    void Start()
    {
        base.Start();
        instance = this;
        flag = Global.FindChild(transform, "Spriteflag");
        arrow = Global.FindChild(transform, "arrow");

    }

    /// <summary>
    /// 设置方向UI
    /// </summary>
    /// <param name="value">方向值</param>
    public void SetDirectionUI(float value)
    {
        flag.transform.localPosition = new Vector3(value * 145, 0, 0);
    }

    public void SetForwardUI(float value)
    {
        arrow.transform.localPosition = new Vector3(60, value * 232, 0);
    }

}
