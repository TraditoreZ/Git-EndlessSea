using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Base.Build;
using Ceto;

public class ObjBuoYancy : MonoBehaviour
{
    [SerializeField]
    private IObjManager manager;
    [SerializeField]
    private List<CubeInfo> underWaterCubes;
    /// <summary>
    /// 浮心
    /// </summary>
    private Vector3 BuoCenter;
    private float cubeRadius;
    private float seaLevel;
    /// <summary>快速刷新为刚体周期  慢速为自定义速度</summary>
    public bool FastRefresh = false;

    //F=ρgv      （1000kg/m^3)(10N/kg)(1m^3) =10000牛顿 =1吨力
    //船舶 V={S1+S2 根号(S1+S2)} /3*H              = M 立方

    [SerializeField]
    /// <summary>当前浮力  单位/N</summary>
    private float currtBuoYancy;
    private Rigidbody objBody;

    void Start()
    {
        manager = transform.GetComponent<IObjManager>();
        cubeRadius = BuildingManager.instance.cubeSize / 2;
        underWaterCubes = new List<CubeInfo>();
        seaLevel = Ocean.Instance.level;
        objBody = GetComponent<Rigidbody>();
        //慢速刷新
        if (!FastRefresh)
        {
            InvokeRepeating("SeletUnderWaterCube", 1, 0.5f);
        }
    }




    void FixedUpdate()
    {

        if (FastRefresh)
        {
            SeletUnderWaterCube();
        }


        UpdateBuoYancy();

    }

    private void UpdateBuoYancy()
    {
        if (underWaterCubes == null || underWaterCubes.Count <= 0)
        {
            return;
        }
        var buoYances = CollectionHelper.Select<CubeInfo, float>(underWaterCubes.ToArray(), p => p.GetBuoYance());
        BuoCenter = PhysicsManager.SeekCenterMass(CollectionHelper.Select<CubeInfo, Vector3>(underWaterCubes.ToArray(), p => p.GetParticle()), buoYances);
        currtBuoYancy = 0;
        for (int i = 0; i < buoYances.Length; i++)
        {
            currtBuoYancy += buoYances[i];
        }
        if (BuoCenter != null && BuoCenter != Vector3.zero)
        {
            objBody.AddForceAtPosition(Vector3.up * currtBuoYancy, BuoCenter, ForceMode.Force);
        }

    }


    private void SeletUnderWaterCube()
    {
        List<CubeInfo> cubeUnder = new List<CubeInfo>();
        for (int i = 0; i < manager.GetCubeList().Count; i++)
        {
            if (manager.GetCubeList()[i].transform.position.y < seaLevel + cubeRadius)
            {

                //水下方块
                cubeUnder.Add(manager.GetCubeList()[i]);


            }
        }
        underWaterCubes = cubeUnder;


    }
    //绘制重心点
    void OnDrawGizmos()
    {
        if (!enabled) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(BuoCenter, 0.5f);
    }


}
