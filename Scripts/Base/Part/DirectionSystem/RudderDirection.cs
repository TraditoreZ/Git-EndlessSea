using UnityEngine;
using System.Collections;
using System;
public class RudderDirection : MonoBehaviour, IDirectionSystem
{
    [SerializeField]
    private float _maxAngle = 60;
    [SerializeField]
    private float _maxForce = 200;
    private PartInfo partinfo;
    public float maxAngle
    {
        get
        {
            return _maxAngle;
        }
    }
    [SerializeField]
    private float _currtAngle;
    public float currtAngle
    {
        get
        {
            return _currtAngle;
        }
    }

    private Transform axle;
    /// <summary>
    /// 转向力
    /// </summary>
    /// <returns></returns>
    public Vector3 DirectionFore()
    {
        return -axle.up * _maxForce;
    }

    public void RotateDirection(float angle)
    {
        _currtAngle = -angle * _maxAngle;
        axle.localRotation = Quaternion.Euler(new Vector3(0, 0, _currtAngle));

    }
    /// <summary>
    /// 返回质点
    /// </summary>
    /// <returns></returns>
    public Vector3 GetParticle()
    {
        return partinfo.GetParticle();
    }
    void Start()
    {
        axle = Global.FindChild(transform, "Box002");
        partinfo = GetComponent<PartInfo>();
    }

}
