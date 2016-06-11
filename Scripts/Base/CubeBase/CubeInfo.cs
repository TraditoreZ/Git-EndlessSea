using UnityEngine;
using System.Collections;
using System;
using Ceto;

public class CubeInfo : MonoBehaviour, IMassParticle, ICubeMass
{


    public ItemType itemType
    {
        get { return mtype; }
    }
    protected ItemType mtype;

    [HideInInspector]
    private Collider m_collider;
    private float density;
    //方块大小
    private float size;
    //方块体积
    private float halfSize;
    private float vol;
    //方块能生产的最大浮力
    private float maxBuo;

    //创建
    public virtual void Instance(ItemType type, CubeType cubeType)
    {
        size = BuildingManager.instance.cubeSize;
        halfSize = size / 2;
        mtype = type;
        m_collider = GetComponent<Collider>();
        density = PhysicsManager.GetDensity(cubeType);
        //如果是默认材质则不需要赋予材质
        if (cubeType == CubeType.none)
            return;

        transform.GetComponent<MeshRenderer>().material = CubeFactory.instance.GetCubeMaterial(cubeType);

        if (m_collider != null)
            m_collider.material = CubeFactory.instance.GetCubePhysicMaterial(cubeType);


        vol = (transform.localScale.x * transform.localScale.y * transform.localScale.z);
        maxBuo = 1000 * 9.8f * vol;

    }
    /// <summary>
    /// 取质心
    /// </summary>
    /// <returns></returns>
    public virtual Vector3 GetParticle()
    {
        return transform.position;
    }
    /// <summary>
    /// 取质量
    /// </summary>
    /// <returns></returns>
    public virtual int GetMass()
    {
        if (density != 0)
        {
            float vol = (transform.localScale.x * transform.localScale.y * transform.localScale.z);
            return Mathf.RoundToInt(density * 1000 * vol);
        }
        else
        {
            return 0;
        }

    }



    /// <summary>
    /// 取浮力(自带水面计算)
    /// </summary>
    /// <returns></returns>
    public float GetBuoYance()
    {

        float levelHeigh = Ocean.Instance.QueryWaves(transform.position.x, transform.position.z);
        float cubeY = transform.position.y - halfSize;
        return Mathf.Lerp(0, maxBuo, Mathf.Abs((levelHeigh - cubeY)) / size);

        //    if (cubeY > waveHeigh + size / 2)
        //    {
        //        return 0;
        //    }
        //    else if (cubeY < -(waveHeigh + size / 2))
        //    {
        //        return maxBuo;
        //    }
        //    else
        //    {
        //        // 0.25    0
        //        //  max/2      max
        //        if (cubeY >= waveHeigh)
        //        {
        //            return Mathf.RoundToInt(Mathf.Lerp(0, (maxBuo / 2), (cubeY - waveHeigh) * (1 / size)));
        //        }
        //        else
        //        {
        //            //  -0.25   0
        //            //    max   max/2
        //            return Mathf.RoundToInt(Mathf.Lerp((maxBuo / 2), maxBuo, (waveHeigh - cubeY) * (1 / size)));
        //        }
        //    }

    }
}
