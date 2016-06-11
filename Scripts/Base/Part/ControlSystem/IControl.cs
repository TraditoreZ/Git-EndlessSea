using UnityEngine;
using System.Collections;

namespace ControlSystem
{
    public interface IControl
    {
        /// <summary>使用配件</summary>
        void UserPart(Transform player);
        /// <summary>退出配件</summary>
        void ExitPart(Transform player);

        void AddEvent(IObjControllerManager objManager);

        void RemoveEvent(IObjControllerManager objManager);
    }
}