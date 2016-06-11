using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public interface IDirectionManager : IObjManager
{
    /// <summary>
    /// 注册方向系统
    /// </summary>
    /// <param name="system"></param>
    void RegistDirection(IDirectionSystem system);

    /// <summary>
    /// 移除方向系统
    /// </summary>
    /// <param name="system"></param>
    void RemoveDirection(IDirectionSystem system);


}
