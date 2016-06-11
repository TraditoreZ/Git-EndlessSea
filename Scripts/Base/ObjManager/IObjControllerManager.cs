using UnityEngine;
using System.Collections;
using ControlSystem;

public interface IObjControllerManager : IObjManager
{
    //注册配件控制器
    void RegistController(IControl controller);

    //移除配件控制器
    void RemoveController(IControl controller);

    //舵管理器
    void DuoMoveControl(float forward, float direction);


}
