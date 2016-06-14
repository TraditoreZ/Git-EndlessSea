using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Base.Build
{
    public class BuildingManager : NetworkBehaviour
    {
        public static BuildingManager instance;
        public Camera PlayerCamera;
        public RaycastHit cubeHit;
        public RaycastHit aidHit;
        public float RayLengh = 10;
        public LayerMask BuildingLayer;
        public LayerMask AirLayer;
        public LayerMask DirectionLayer;
        public float cubeSize = 0.5f;
        public bool BuildingMode { get; private set; }
        public bool CubeMode { get; private set; }

        //当前物体类型
        [SerializeField]
        private ItemType _currtItemType;
        public ItemType currtItemType { get { return _currtItemType; } }
        public bool currtItemIsPart { get { return _currtItemIsPart; } }
        private bool _currtItemIsPart;


        //目标物体信息
        private Transform _targerTransform;
        private CubeInfo _targerInfo;
        public CubeInfo targerInfo { get { return _targerInfo; } }



        [SerializeField]
        private CubeType currtMaterialType;
        public Ray ray;
        private bool rayNotNull;

        void Start()
        {
            instance = this;
            BuildingMode = true;
            Cursor.lockState = CursorLockMode.Locked;

        }

        // Update is called once per frame
        void Update()
        {
            // ReSharper disable once PossibleLossOfFraction
            ray = PlayerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));


            //是否开启建造模式
            if (IsBuildingMode())
            {
                BuildingRay();
                OnButtonClick(true);
            }
            else
            {
                OnButtonClick(false);
            }


        }



        //建造射线并选择建造起点
        void BuildingRay()
        {

            if (Physics.Raycast(ray, out cubeHit, RayLengh, BuildingLayer))
            {
                BuildAidSystem.instance.ShowAidObject(true);
                rayNotNull = true;
                MoveAidSystemPos();
                LoadTargerInfo();
            }
            else
            {
                rayNotNull = false;
                BuildAidSystem.instance.ShowAidObject(false);
            }
        }


        /// <summary>
        /// 读取目标物体和信息
        /// </summary>
        private void LoadTargerInfo()
        {
            if (cubeHit.collider.tag == "Terrain")
            {
                _targerTransform = null;
                _targerInfo = null;
            }
            else
            {
                if (_targerTransform != cubeHit.collider.transform)
                {
                    _targerTransform = cubeHit.collider.transform;
                    _targerInfo = _targerTransform.GetComponent<CubeInfo>();

                }
            }

        }



        //开启建造模式的条件
        private bool IsBuildingMode()
        {
            //做接口
            if (BuildingMode && CubeMode)
                return true;
            BuildAidSystem.instance.ShowAidObject(false);
            return false;
        }

        private void MoveAidSystemPos()
        {
            BuildAidSystem.instance.MoveBuildAidSystem(cubeHit);
        }

        /// <summary>
        /// 获取目标当前被射方向
        /// </summary>
        /// <returns>方向</returns>
        public CubeDirectionType GetTargerCubeDirection()
        {
            Debug.Log("Test:" + rayNotNull);
            if (!rayNotNull) { return CubeDirectionType.Down; }
            if (cubeHit.normal.normalized - cubeHit.collider.transform.forward == Vector3.zero)
                return CubeDirectionType.Forward;
            if (cubeHit.normal.normalized + cubeHit.collider.transform.forward == Vector3.zero)
                return CubeDirectionType.Back;
            if (cubeHit.normal.normalized - cubeHit.collider.transform.right == Vector3.zero)
                return CubeDirectionType.Right;
            if (cubeHit.normal.normalized + cubeHit.collider.transform.right == Vector3.zero)
                return CubeDirectionType.Left;
            if (cubeHit.normal.normalized - cubeHit.collider.transform.up == Vector3.zero)
                return CubeDirectionType.Up;
            if (cubeHit.normal.normalized + cubeHit.collider.transform.up == Vector3.zero)
                return CubeDirectionType.Down;
            return CubeDirectionType.Down;

        }
        /// <summary>
        /// 获取预制方块建造点方向
        /// </summary>
        /// <returns>方向枚举</returns>
        public CubeDirectionType GetSelfCubeDirection()
        {
            if (!rayNotNull) { return CubeDirectionType.Down; }
            if (cubeHit.normal.normalized - BuildAidSystem.instance.prefabTrans.forward == Vector3.zero)
                return CubeDirectionType.Back;
            if (cubeHit.normal.normalized + BuildAidSystem.instance.prefabTrans.forward == Vector3.zero)
                return CubeDirectionType.Forward;
            if (cubeHit.normal.normalized - BuildAidSystem.instance.prefabTrans.right == Vector3.zero)
                return CubeDirectionType.Left;
            if (cubeHit.normal.normalized + BuildAidSystem.instance.prefabTrans.right == Vector3.zero)
                return CubeDirectionType.Right;
            if (cubeHit.normal.normalized - BuildAidSystem.instance.prefabTrans.up == Vector3.zero)
                return CubeDirectionType.Down;
            if (cubeHit.normal.normalized + BuildAidSystem.instance.prefabTrans.up == Vector3.zero)
                return CubeDirectionType.Up;
            return CubeDirectionType.Down;
        }


        // 当切换物品时
        public void ChangeItemType(ItemType type, CubeType matType)
        {
            _currtItemType = type;
            currtMaterialType = matType;
            if ((int)type > 1000)
            {
                // 判断是方块还是配件 供全局调用
                if ((int)type > 1000 && (int)type < 2000)
                    _currtItemIsPart = false;

                else if ((int)type > 2000)
                    _currtItemIsPart = true;
                //-----------------------------------
                CubeMode = true;
                BuildAidSystem.instance.SwitchAid(type, matType);
            }
            else
            {
                CubeMode = false;
            }



        }
        private void OnButtonClick(bool open)
        {
            if (!open) return;
            if (Input.GetMouseButtonDown(0))
            {

            }



            if (Input.GetMouseButtonUp(0))
            {
                if (cubeHit.collider != null)
                {
                    CreateObj();
                }

            }

            if (Input.GetMouseButtonDown(1))
            {

            }

            if (Input.GetMouseButtonUp(1))
            {
                if (cubeHit.collider != null && cubeHit.collider.tag != "Terrain")
                {
                    DeleteObj();
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                BuildAidSystem.instance.RotateObj();
            }


        }

        private void DeleteObj()
        {
            ObjParentPro(cubeHit.transform, cubeHit.collider.transform, false);
            Destroy(cubeHit.collider.gameObject);
        }

        private void CreateObj()
        {
            if (!BuildAidSystem.instance.AllowCreae || cubeHit.collider == null)
            {
                Debugger.LogError("无法创建，播放警示音!");
                //错误接口
                return;
            }
            //如果什么都不是则返回
            var itemsType = SeleteItemsType(_currtItemType);
            string path = null;
            switch (itemsType)
            {

                case ItemsTpye.none:
                    return;
                case ItemsTpye.stuff:
                    return;
                case ItemsTpye.Cube:
                    path = "Cube/";
                    break;
                case ItemsTpye.Part:
                    path = "Part/";
                    break;
            }

            var obj = Resources.Load(path + Enum.GetName(typeof(ItemType), _currtItemType));
            if (obj == null)
            {
                Debugger.LogError("生成方块不存在，请检查物体或路径");
                return;

            }
            GameObject cube = (Instantiate(obj) as GameObject);
            if (cube != null)
            {
                cube.SetActive(false);
                cube.transform.parent = BuildAidSystem.instance.prefabTrans;
                cube.transform.localPosition = Vector3.zero;
                cube.transform.localRotation = Quaternion.identity;
                cube.transform.parent = null;
                cube.SetActive(true);
                SetCubeInstance(cube.transform);
                ParentSys(cube.transform);
            }
        }
        /// <summary>
        /// 对父级的操作
        /// </summary>
        private void ParentSys(Transform newObj)
        {
            switch (cubeHit.collider.tag)
            {
                case "Cube":
                    newObj.parent = cubeHit.collider.transform.parent;
                    ObjParentPro(cubeHit.collider.transform.parent, newObj, true);
                    break;
                case "Terrain":
                    var objs = Instantiate(Resources.Load("Obj/PlayerObj")) as GameObject;
                    if (objs != null)
                    {
                        objs.transform.position = newObj.position;
                        objs.transform.rotation = newObj.rotation;
                        newObj.parent = objs.transform;
                        ObjParentPro(objs.transform, newObj, true);
                    }
                    break;
            }
        }
        /// <summary>
        /// 船属性 父级属性
        /// </summary>
        private void ObjParentPro(Transform objs, Transform cube, bool addCube)
        {
            if (addCube)
                objs.GetComponent<IObjManager>().AddCube(cube);
            else
                objs.GetComponent<IObjManager>().RemoveCube(cube);
        }
        /// <summary>
        /// 进行生成物体的初始化
        /// </summary>
        /// <param name="targer"></param>
        private void SetCubeInstance(Transform targer)
        {
            targer.GetComponent<CubeInfo>().Instance(_currtItemType, currtMaterialType);
        }

        /// <summary>
        /// 根据物品的类型定位是哪种物品 例如: 原料  方块  配件
        /// </summary>
        /// <param name="type">物品种类</param>
        /// <returns>物品类型</returns>
        public static ItemsTpye SeleteItemsType(ItemType type)
        {
            if ((int)type > 1 && (int)type < 1000)
                return ItemsTpye.stuff;
            if ((int)type > 1000 && (int)type < 2000)
                return ItemsTpye.Cube;
            if ((int)type > 2000)
                return ItemsTpye.Part;
            return ItemsTpye.none;
        }

    }
}
