using System;
using UnityEngine;

namespace Assets.Scripts.Base.Part.DynamicSystem
{
    public class FanSystem : MonoBehaviour, IDynamic
    {
        private float force = 2000;

        private Transform centermass;
        void Start()
        {
            centermass = Global.FindChild(transform, "Particle");
        }


        Vector3 IDynamic.Dynamic
        {
            get
            {
                return transform.up * force;
            }

            set
            {

            }
        }

        Vector3 IDynamic.CenterMass
        {
            get
            {
                if (centermass != null)
                {
                    return centermass.position;
                }
                return Vector3.zero;
            }

            set
            {

            }
        }
    }

    class FanSystemImpl : FanSystem
    {
    }
}
