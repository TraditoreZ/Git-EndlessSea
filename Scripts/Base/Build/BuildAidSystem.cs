using UnityEngine;
using System.Collections;
using Develop;
using Aid;
using System;
using System.Collections.Generic;
/// <summary>
/// 建造辅助系统
/// </summary>
public class BuildAidSystem : MonoSingleton<BuildAidSystem>
{
    public Color TrueColor;
    public Color FalseColor;


    private GameObject prefabObj;
    public Transform prefabTrans { get { return prefabObj.transform; } }


    public bool AllowCreae { get { return allowCreae; } }
    private bool allowCreae;

    private IAid IObstacle;
    private ItemType currtType;
    private Vector3 blockTemp;
    private Vector3 ppTemp;
    private Vector3 blockNormalTemp;
    private Vector3 allTemp;
    private Vector3 finalTemp;

    private IRotateFactory rotateFactory;
    //当前旋转角度
    private Vector3 currtRotate;

    void Start()
    {
        rotateFactory = new RotateCubeFactory();
    }

    void Update()
    {
        if (prefabObj == null) return;
        allowCreae = AllowCreation();
        WarningColor(allowCreae);


    }

    public void SwitchAid(ItemType type, CubeType matType)
    {
        currtType = type;
        GameObject newObj;
        string itemName = Enum.GetName(typeof(ItemType), type);
        if (!BuildingManager.instance.currtItemIsPart)
        {
            newObj = GameObjectPool.instance.CreateObject("Cube", Resources.Load("Aid/CubeAid") as GameObject, transform.position, transform.rotation);
            if (newObj != prefabObj)
            {
                GameObjectPool.instance.MyDestory(prefabObj);
                prefabObj = newObj;
            }
            // 方块操作
            ChangeCubeMesh(itemName);
        }
        else
        {
            //配件操作
            newObj = GameObjectPool.instance.CreateObject(itemName, Resources.Load("Aid/" + itemName + "Aid") as GameObject, transform.position, transform.rotation);
            if (newObj != prefabObj)
            {
                GameObjectPool.instance.MyDestory(prefabObj);
                prefabObj = newObj;
            }
        }
        IObstacle = prefabObj.GetComponent<IAid>();

        //如果是第一次生成物体到对象池则进行归零化处理
        if (prefabObj.transform.parent == null)
        {
            prefabObj.transform.parent = transform;
            prefabObj.transform.localPosition = Vector3.zero;
            prefabObj.transform.localRotation = Quaternion.identity;
        }



    }
    public void ChangeCubeMesh(string name)
    {

        var mesh = Resources.Load("Meshes/" + name);

        if (mesh != null)
        {
            prefabObj.transform.GetComponent<MeshFilter>().mesh = Instantiate(mesh) as Mesh;
        }
        else
            Debug.LogError("丢失方块纹理");
    }

    /// <summary>
    /// 显示隐藏cube
    /// </summary>
    /// <param name="show"></param>
    public void ShowAidObject(bool show)
    {
        ShowObj(show);

    }
    private void ShowObj(bool show)
    {
        if (prefabObj == null) return;

        if (prefabObj.activeSelf && !show)
        {
            prefabObj.SetActive(false);
        }
        else if (!prefabObj.activeSelf && show)
        {
            prefabObj.SetActive(true);
        }
    }


    /// <summary>
    /// 移动辅助系统
    /// </summary>
    /// <param name="targer">射线目标碰撞点</param>
    public void MoveBuildAidSystem(RaycastHit hitCube)
    {
        if (hitCube.collider.tag == "Terrain")
        {
            transform.position = hitCube.point + new Vector3(0, BuildingManager.instance.cubeSize / 2, 0);
        }
        else
        {
            blockTemp = hitCube.collider.transform.position;
            ppTemp = new Vector3(0, 1, 0);
            blockNormalTemp = hitCube.normal.normalized * 0.5f;
            allTemp = blockTemp + blockNormalTemp + ppTemp;
            finalTemp = allTemp - new Vector3(0, 1, 0);
            transform.position = finalTemp;
            transform.rotation = hitCube.collider.transform.rotation;
            PartPosUp(hitCube.normal.normalized);
        }


    }

    //上次操作配件的方向
    private Vector3 lastPartDes;
    //上次操作配件类型
    private ItemType lastItemType;
    /// <summary>
    /// 配件永远朝向射线点
    /// </summary>
    private void PartPosUp(Vector3 currtCube)
    {
        if (BuildingManager.instance.currtItemIsPart)
        {
            // 加个判断  切换物品的时候更新一次
            if (lastPartDes != null && lastPartDes != currtCube || lastItemType != BuildingManager.instance.currtItemType || lastPartDes == null)
            {
                currtRotate = Vector3.zero;
                prefabTrans.localRotation = Quaternion.identity;
                switch (BuildingManager.instance.GetTargerCubeDirection())
                {
                    case CubeDirectionType.Up:
                        currtRotate = new Vector3(0, 0, 0);
                        break;
                    case CubeDirectionType.Down:
                        currtRotate = new Vector3(0, 0, -180);
                        break;
                    case CubeDirectionType.Left:
                        currtRotate = new Vector3(0, 0, 90);
                        break;
                    case CubeDirectionType.Right:
                        currtRotate = new Vector3(0, 0, -90);
                        break;
                    case CubeDirectionType.Forward:
                        currtRotate = new Vector3(90, 0, 0);
                        break;
                    case CubeDirectionType.Back:
                        currtRotate = new Vector3(-90, 0, 0);
                        break;
                    default:
                        break;
                }
                prefabTrans.localRotation = Quaternion.Euler(new Vector3(currtRotate.x, prefabTrans.localRotation.y, currtRotate.z));

                lastPartDes = currtCube;
                lastItemType = BuildingManager.instance.currtItemType;
            }
        }
    }

    private void WarningColor(bool allow)
    {
        if (allow)
        {
            if (IObstacle.GetColor() != TrueColor)
                IObstacle.SetColor(TrueColor);
        }
        else
        {
            if (IObstacle.GetColor() != FalseColor)
                IObstacle.SetColor(FalseColor);

        }
    }
    #region  可否创建物体判断
    /// <summary>
    /// 是否可以创建物体
    /// </summary>
    /// <returns>可以创建</returns>
    private bool AllowCreation()
    {
        //将来条件加在这里  有一条不满足都无法创建物体  每帧都要判断  可以做成列表
        if (!CheckObstacle() || !CheckDirection() || !CheckPartOnTerrain())
        {
            return false;
        }
        return true;
    }
    private bool CheckObstacle()
    {
        return IObstacle.CheckObstacle();
    }

    /// <summary>
    /// 检测两物体接触面可否建造 只判断方块
    /// </summary>
    /// <returns>可以建造</returns>
    private bool CheckDirection()
    {
        if (BuildingManager.instance.targerInfo != null)
        {
            bool targer = false;
            bool self = false;
            if (BuildingManager.instance.currtItemIsPart)
            {
                self = true;
            }
            else
            {
                CubeDirectionType directiontype = BuildingManager.instance.GetSelfCubeDirection();
                var directionTypes = LimitCubeManager.instance.GetCubeDirectionTypes(currtType);
                for (int i = 0; i < directionTypes.Length; i++)
                {
                    if (directionTypes[i] == directiontype)
                    {
                        self = true;
                    }
                }
            }
            if (BuildingManager.SeleteItemsType(BuildingManager.instance.targerInfo.itemType) == ItemsTpye.Cube)
            {
                var targerDirTypes = LimitCubeManager.instance.GetCubeDirectionTypes(BuildingManager.instance.targerInfo.itemType);
                for (int i = 0; i < targerDirTypes.Length; i++)
                {
                    if (targerDirTypes[i] == BuildingManager.instance.GetTargerCubeDirection())
                    {
                        targer = true;
                    }
                }
            }
            else
            {
                // 目标配件  当前方向是否可以建造
                targer = true;
            }


            if (!targer || !self)
            {
                return false;
            }
            else
                return true;

        }
        else
            return true;

    }
    /// <summary>
    /// 检测如果是配件， 则不允许建立在地形上                  --------------------------------------------如果配件想建在配件上 需要这里处理一下  （例如:风帆扩展）
    /// </summary>
    /// <returns></returns>
    private bool CheckPartOnTerrain()
    {
        if (BuildingManager.SeleteItemsType(BuildingManager.instance.currtItemType) == ItemsTpye.Part)
        {
            if (BuildingManager.instance.cubeHit.collider.tag == "Cube")  //---BUG
                return true;
            else
                return false;
        }
        return true;
    }

    #endregion
    public void RotateObj()
    {
        //非原料才能旋转
        if (BuildingManager.SeleteItemsType(BuildingManager.instance.currtItemType) == ItemsTpye.Cube)
        {
            int protect = 0;
            Vector3 NextRotate = rotateFactory.GetRotate();
            do
            {
                prefabObj.transform.Rotate(NextRotate, Space.Self);
                protect++;
                if (protect > 8)
                    break;
            } while (!CheckDirection());

        }
        else if (BuildingManager.SeleteItemsType(BuildingManager.instance.currtItemType) == ItemsTpye.Part)
        {
            rotateFactory.SetPartRotate();
            prefabTrans.Rotate(Vector3.up * 90);
        }

    }


}
