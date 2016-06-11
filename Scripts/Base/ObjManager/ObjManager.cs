using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using ControlSystem;

public class ObjManager : MonoBehaviour, IObjManager, IDynamicManager, IObjControllerManager, IDirectionManager
{
    [SerializeField]
    private List<CubeInfo> cubes;

    private Rigidbody body;
    public Transform m_centerOfMass;
    private UnitArray unitArray;
    /// <summary>物体重力 （N）</summary>
    public float gravity { get { return _gravity; } }
    [SerializeField]
    private float _gravity;
    //// <summary>携程锁</summary>
    //private bool ergodicLook;

    [SerializeField]
    /// <summary>动力系统</summary>
    private List<IDynamic> dynamics = new List<IDynamic>();
    [SerializeField]
    /// <summary>动力合力</summary>
    private Vector3 dynamicForce = Vector3.zero;
    [SerializeField]
    /// <summary>动力点</summary>
    private Vector3 dynamicCenter = Vector3.zero;
    //加速程度 -1  -- 1
    float Forward;
    //转速程度 -1  -- 1
    float Direction;
    // 转向合力
    [SerializeField]
    Vector3 maxDirectionForce;
    //尾舵中心点
    private Vector3 DirectionCenter;
    #region  基本信息
    private void CheckCubes()
    {
        cubes = transform.GetComponentsInChildren<CubeInfo>().ToList();
        Debugger.Log(transform.name + " CubesCount:" + cubes.Count);
    }

    public void AddCube(Transform cube)
    {
        if (body == null)
        {
            body = transform.GetComponent<Rigidbody>();
        }

        var cubeinfo = cube.GetComponent<CubeInfo>();
        if (cubeinfo != null)
        {
            cubes.Add(cubeinfo);
            body.mass += cubeinfo.GetMass();
            _gravity = PhysicsManager.GetGravity(Mathf.RoundToInt(body.mass));
            //注册信息
            RegisterPart(cubeinfo.itemType, cubeinfo.transform);
            // 刷新重力
            UpdateGravity();



        }
    }

    public void RemoveCube(Transform cube)
    {
        if (cubes == null)
            return;
        var cubeinfo = cube.GetComponent<CubeInfo>();
        cubes.Remove(cubeinfo);
        body.mass -= cubeinfo.GetMass();
        _gravity = PhysicsManager.GetGravity(Mathf.RoundToInt(body.mass));
        if (body.mass <= 1)
        {
            Destroy(gameObject);
            return;
        }
        CheckIntegrity();

    }
    /// <summary>
    /// 检测方块完整度
    /// </summary>
    private void CheckIntegrity()
    {

        UpdateGravity();
    }


    public List<CubeInfo> GetCubeList()
    {
        return cubes;
    }

    //绘制重心点
    void OnDrawGizmos()
    {
        if (!enabled) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(body.worldCenterOfMass, 1f);
        Gizmos.DrawWireCube(dynamicCenter, Vector3.one);
    }
    #endregion
    #region 重力和物理系统




    /// <summary>
    /// 当物体变化时刷新重心
    /// </summary>
    private void UpdateGravity()
    {
        body.centerOfMass = m_centerOfMass.localPosition;
        SetGravity(PhysicsManager.SeekCenterMass(CollectionHelper.Select(cubes.ToArray(), p => p.GetParticle()), CollectionHelper.Select(cubes.ToArray(), p => p.GetMass())));
    }
    /// <summary>
    /// 当物体增加时刷新重心   -------------------------------这个浮力加权有问题
    /// </summary>
    /// <param name="newCube">新物体</param>
    private void UpdateGravity(CubeInfo newCube)
    {
        body.centerOfMass = m_centerOfMass.localPosition;
        SetGravity(PhysicsManager.SeekCenterMass(m_centerOfMass.transform.position, Mathf.RoundToInt(body.mass), newCube.GetParticle(), newCube.GetMass()));
    }

    /// <summary>
    /// 设置重心                           
    /// </summary>
    private void SetGravity(Vector3 center)
    {
        Vector3 centerMass = center;
        Vector3 localCenterMass = new Vector3((float)Math.Round((centerMass - transform.position).x, 2), (float)Math.Round((centerMass - transform.position).y, 2), (float)Math.Round((centerMass - transform.position).z, 2));
        m_centerOfMass.localPosition = localCenterMass;
        body.centerOfMass = localCenterMass;
    }

    void FixedUpdate()
    {
        UpdateDynamic();
        DirectionUpdate();

        body.centerOfMass = m_centerOfMass.localPosition;
        //运动学刚体模拟施加力
        //Gravity();

    }
    private void Gravity()
    {
        body.AddForceAtPosition(-Vector3.up * 9.8f, m_centerOfMass.position);
    }
    /// <summary>
    /// 刷新方向动力学
    /// </summary>
    private void DirectionUpdate()
    {
        if (directions != null)
        {
            for (int i = 0; i < directions.Count; i++)
            {
                body.AddForceAtPosition(directions[i].DirectionFore() * Direction * Forward, directions[i].GetParticle(), ForceMode.Force);
                directions[i].RotateDirection(Direction);
            }
        }
    }
    /// <summary>
    /// 刷新动力学
    /// </summary>
    private void UpdateDynamic()
    {
        Vector3 currtdynamicForce = Vector3.zero;
        Vector3[] centers = new Vector3[dynamics.Count];
        for (int i = 0; i < dynamics.Count; i++)
        {
            body.AddForceAtPosition(dynamics[i].GetDynamic() * Forward, dynamics[i].GetCenterMass(), ForceMode.Force);
        }



    }
    #endregion




    #region   配件注册
    /// <summary>
    /// 注册配件信息
    /// </summary>
    private void RegisterPart(ItemType type, Transform transform)
    {
        var part = RegisterPartFactory.SeletePart(type);
        if (part != null)
        {
            part.Register(this, transform);
        }
    }
    private void RemovePart(ItemType type, Transform transform)
    {
        var part = RegisterPartFactory.SeletePart(type);
        if (part != null)
        {
            part.Remove(this, transform);
        }
    }

    //注册动力学
    public void RegistDynamic(IDynamic system)
    {
        if (system != null && !dynamics.Contains(system))
        {
            dynamics.Add(system);
            UpdateDynamic();
            Debugger.Log("RegistDynamic:" + dynamics.Count);
        }
    }
    //移除动力学
    public void RemoveDynamic(IDynamic system)
    {
        if (system != null && dynamics.Contains(system))
        {
            dynamics.Remove(system);
            UpdateDynamic();
        }
    }

    //注册配件控制器
    public void RegistController(IControl controller)
    {
        controller.AddEvent(this as IObjControllerManager);
    }
    //移除配件控制器
    public void RemoveController(IControl controller)
    {
        controller.RemoveEvent(this as IObjControllerManager);
    }

    public void DuoMoveControl(float forward, float direction)
    {
        Forward = forward;
        Direction = direction;
    }

    private List<IDirectionSystem> directions = new List<IDirectionSystem>();

    //注册方向系统
    public void RegistDirection(IDirectionSystem system)
    {
        directions.Add(system);
    }
    //移除方向系统
    public void RemoveDirection(IDirectionSystem system)
    {
        directions.Remove(system);
    }

    #endregion
}
