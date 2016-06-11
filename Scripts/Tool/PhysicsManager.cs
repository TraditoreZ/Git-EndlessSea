using UnityEngine;
using System.Collections;

public static class PhysicsManager
{

    /// <summary>
    /// 根据质点和质量求质心
    /// </summary>
    /// <param name="r">r1 r2 r3 ...</param>
    /// <param name="m">m1 m2 m3 ...</param>
    /// <returns></returns>
    public static Vector3 SeekCenterMass(Vector3[] r, int[] m)
    {
        if (r.Length != m.Length)
            return Vector3.zero;

        Vector3 Emr = Vector3.zero;
        int Em = 0;

        for (int i = 0; i < m.Length; i++)
        {
            Emr += m[i] * r[i];

            Em += m[i];
        }

        return Emr / Em;

    }
    /// <summary>
    /// 根据质点和质量求质心
    /// </summary>
    /// <param name="r">r1 r2 r3 ...</param>
    /// <param name="m">m1 m2 m3 ...</param>
    /// <returns></returns>
    public static Vector3 SeekCenterMass(Vector3[] r, float[] m)
    {
        if (r.Length != m.Length)
            return Vector3.zero;

        Vector3 Emr = Vector3.zero;
        float Em = 0;

        for (int i = 0; i < m.Length; i++)
        {
            Emr += m[i] * r[i];

            Em += m[i];
        }

        return Emr / Em;

    }
    /// <summary>
    /// 增加一新物体求质心 
    /// </summary>
    /// <param name="objCenter">被增加物体质心</param>
    /// <param name="objMass">被增加物体质量</param>
    /// <param name="blockCenter">新增物体的质心</param>
    /// <param name="bolckMass">新增物体的质量</param>
    /// <returns></returns>
    public static Vector3 SeekCenterMass(Vector3 objCenter, int objMass, Vector3 blockCenter, int bolckMass)
    {

        return ((objCenter * objMass) + (blockCenter * bolckMass)) / (objMass + bolckMass);
    }
    /// <summary>
    /// 快速求中心
    /// </summary>
    /// <param name="r">中心点</param>
    /// <returns></returns>
    public static Vector3 SeekCenter(Vector3[] r)
    {

        Vector3 Emr = Vector3.zero;
        int Em = 0;

        for (int i = 0; i < r.Length; i++)
        {
            Emr += r[i];

            Em += 1;
        }

        return Emr / Em;

    }

    /// <summary> 获取物体密度</summary>
    /// <param name="type">方块类型</param>
    /// <returns>密度 KG/M</returns>
    public static float GetDensity(CubeType type)
    {
        //    case CubeType.wooden:
        //        return 0.5f;
        //    case CubeType.stone:
        //        return 2.66f;
        //    case CubeType.Iron:
        //        return 7.8f;
        //  因游戏原因密度比不能差太大  最大2.5倍
        switch (type)
        {
            case CubeType.none:
                return 0;
            case CubeType.wooden:
                return 0.5f;
            case CubeType.stone:
                return 1.3f;
            case CubeType.Iron:
                return 2f;
            default:
                return 0;
        }
    }
    /// <summary> 获取物体重力 </summary>
    /// <param name="mass">质量</param>
    /// <returns>重力 （N）</returns>
    public static float GetGravity(int mass)
    {
        return mass * 9.8f;
    }

}
