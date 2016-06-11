using UnityEngine;
using System.Collections;
public enum ItemsTpye
{
    none,
    /// <summary>
    /// 材料类型
    /// </summary>
    stuff,
    /// <summary>
    /// 方块类型
    /// </summary>
    Cube,
    /// <summary>
    /// 配件类型
    /// </summary>
    Part

}

public enum ItemType
{

    //0-1000 是原料   1000以后是半成品
    none = 0,

    //原料






    //半成品

    /// <summary> 方块</summary>
    Cube = 1001,
    /// <summary> 菱形</summary>
    Wedge = 1002,
    /// <summary> 空方块(薄)</summary>
    CubeHollowThin = 1003,
    /// <summary> 空方块(厚)</summary>
    CubeHollow = 1004,
    /// <summary> 圆柱</summary>
    Cylinder = 1005,
    /// <summary> 圆管（厚）</summary>
    CylinderTube = 1006,
    /// <summary> 圆管（薄）</summary>
    CylinderTubeThin = 1007,
    /// <summary> 8角体</summary>
    PrismOctagon = 1008,
    /// <summary> 5角体</summary>
    PrismPentagon = 1009,
    /// <summary> 等腰三角体</summary>
    PrismTriangle = 1010,
    /// <summary> 金字塔形</summary>
    Pyramid = 1011,
    /// <summary> 侧边补角体</summary>
    PyramidCorner = 1012,

    //配件
    Cannon = 2001,
    //方向舵
    Duo = 2002,
    //风帆
    Fan = 2003,
    //尾舵
    Rudder=2004
}
