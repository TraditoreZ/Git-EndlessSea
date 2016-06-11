using UnityEngine;
using System.Collections;

public interface IRotateFactory
{
    Vector3 GetRotate();
    float GetPartRotate();
    void SetPartRotate();
}
