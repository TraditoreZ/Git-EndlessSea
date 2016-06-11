using UnityEngine;
using System.Collections;

public interface IDirectionSystem
{
    float maxAngle { get; }
    float currtAngle { get; }
    Vector3 DirectionFore();
    void RotateDirection(float angle);
    Vector3 GetParticle();
}
