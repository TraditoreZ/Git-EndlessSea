using UnityEngine;
using System.Collections;

namespace Aid
{
    public interface IAid
    {
        bool CheckObstacle();
        void SetColor(Color color);
        Color GetColor();
    }
}
