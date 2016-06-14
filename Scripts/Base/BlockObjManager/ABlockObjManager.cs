using System;
using System.Collections.Generic;
using Assets.Scripts.Base.Build;
using Base.BlockObjManager;
using UnityEngine;

namespace Assets.Scripts.Base.BlockObjManager
{
    public class ABlockObjManager : MonoBehaviour, IBodyDynamic, IObjManager
    {
        protected List<CubeInfo> items = new List<CubeInfo>();
        protected List<APartSystem> parts = new List<APartSystem>();
        protected Rigidbody Body;
        private Vector3 localCenterOfMass;

        public virtual void AddCube(Transform cube)
        {
            CheckRigibody();

            var cubeinfo = cube.GetComponent<CubeInfo>();
            if (cubeinfo != null)
            {
                items.Add(cubeinfo);
                Body.mass += cubeinfo.GetMass();
                //_gravity = PhysicsManager.GetGravity(Mathf.RoundToInt(body.mass));
                //注册信息
                RegisterPart(cubeinfo.itemType, cubeinfo.transform);
                // 刷新重力
                UpdateCenterOfMass();

            }
        }

        void FixedUpdate()
        {
            GenerateGravity();
        }

        public virtual void RemoveCube(Transform cube)
        {
            CheckRigibody();

            var cubeinfo = cube.GetComponent<CubeInfo>();
            if (cubeinfo != null)
            {
                items.Remove(cubeinfo);
                Body.mass -= cubeinfo.GetMass();
                RemovePart(cubeinfo.itemType, cubeinfo.transform);
                UpdateCenterOfMass();
            }
        }

        public List<CubeInfo> GetCubeList()
        {
            return items;
        }

        public virtual void CheckCube()
        {

        }

        protected void CheckRigibody()
        {
            if (Body == null)
            {
                Body = transform.GetComponent<Rigidbody>();
            }
        }

        private void RegisterPart(ItemType itemType, Transform transform)
        {
            if (BuildingManager.SeleteItemsType(itemType) == ItemsTpye.Part)
            {
                APartSystem part = transform.GetComponent<APartSystem>();
                if (part != null)
                    parts.Add(part);

            }
        }

        private void RemovePart(ItemType itemType, Transform transform)
        {
            if (BuildingManager.SeleteItemsType(itemType) == ItemsTpye.Part)
            {
                APartSystem part = transform.GetComponent<APartSystem>();
                if (part != null)
                    parts.Remove(part);
            }
        }

        public virtual void SetBodyDynamic(Vector3 fore, Vector3 point)
        {
            Body.AddForceAtPosition(fore, point, ForceMode.Force);
        }

        public T GetPartSystem<T>()
            where T : APartSystem
        {
            for (int i = 0; i < parts.Count; i++)
            {
                if (parts[i].GetType() == typeof(T))
                    return parts[i] as T;
            }
            return default(T);
        }
        public T[] GetPartsSystem<T>()
            where T : APartSystem
        {
            List<T> temPart = new List<T>();
            for (int i = 0; i < parts.Count; i++)
            {
                if (parts[i].GetType() == typeof(T))
                    temPart.Add(parts[i] as T);
            }
            return temPart.ToArray();
        }

        private void UpdateCenterOfMass()
        {
            Vector3 centerMass = PhysicsManager.SeekCenterMass(CollectionHelper.Select(items.ToArray(), p => p.GetParticle()), CollectionHelper.Select(items.ToArray(), p => p.GetMass()));
            localCenterOfMass = new Vector3((float)Math.Round((centerMass - transform.position).x, 2), (float)Math.Round((centerMass - transform.position).y, 2), (float)Math.Round((centerMass - transform.position).z, 2));
        }

        private Vector3 GetWorldCenterMass()
        {
            return Body.transform.position + localCenterOfMass;
        }

        /// <summary>
        /// 重力生产
        /// </summary>
        private void GenerateGravity()
        {
            Body.AddForceAtPosition(-Vector3.up * 9.8f, GetWorldCenterMass(), ForceMode.Force);
        }

        //绘制重心点
        void OnDrawGizmos()
        {
            if (!enabled) return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(GetWorldCenterMass(), 1f);
        }


    }

}