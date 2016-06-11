using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>单元矩阵信息</summary>
public class UnitArray : MonoBehaviour
{
    [SerializeField]
    /// <summary> 单元矩阵 </summary>
    private List<UnitBuoInfo> unitArray;



    private void InstanceUnitArray()
    {
        unitArray = new List<UnitBuoInfo>();
        StartCoroutine(UpdateUnitArray());
    }

    public void AddUnitInfo(CubeInfo cubeinfo)
    {
        if (unitArray == null)
        {
            InstanceUnitArray();
        }
        //只有方块才被列入单元矩阵信息
        if (BuildingManager.SeleteItemsType(cubeinfo.itemType) != ItemsTpye.Cube)
        {
            return;
        }

        //计算坐标
        int x = Mathf.RoundToInt(CollectionHelper.Round(cubeinfo.transform.localPosition.x, 2) * 2);
        int y = Mathf.RoundToInt(CollectionHelper.Round(cubeinfo.transform.localPosition.y, 2) * 2);
        int z = Mathf.RoundToInt(CollectionHelper.Round(cubeinfo.transform.localPosition.z, 2) * 2);

        unitArray.Add(new UnitBuoInfo(BuoyanceType.cube, x, y, z));

    }

    private IEnumerator UpdateUnitArray()
    {
        while (true)
        {
            var currtArray = unitArray;

            for (int i = 0; i < currtArray.Count; i++)
            {





                yield return null;
            }
        }
    }



}
