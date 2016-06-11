using UnityEngine;
using System.Collections;

public interface IDynamicManager:IObjManager
{
    /// <summary>
    /// 注册动力系统
    /// </summary>
    /// <param name="system"></param>
    void RegistDynamic(IDynamic system);

    /// <summary>
    /// 移除动力系统
    /// </summary>
    /// <param name="system"></param>
    void RemoveDynamic(IDynamic system);

}
