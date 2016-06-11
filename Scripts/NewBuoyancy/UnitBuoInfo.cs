using UnityEngine;
using System.Collections;
[SerializeField]
/// <summary>
/// 单元浮力信息
/// </summary>
public class UnitBuoInfo
{
    public UnitBuoInfo(BuoyanceType type, int x, int y, int z)
    {
        Type = type;
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public UnitBuoInfo(CubeInfo cube, BuoyanceType type, int x, int y, int z)
    {
        this.cube = cube;
        Type = type;
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public CubeInfo cube;
    public int x;
    public int y;
    public int z;
    BuoyanceType Type;

}
