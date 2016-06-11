using UnityEngine;
using System.Collections;
using System;

public class FanSystem : MonoBehaviour, IDynamic
{
    private float force = 2000;

    private Transform centermass;
    void Start()
    {
        centermass = Global.FindChild(transform, "Particle");
    }


    public Vector3 GetCenterMass()
    {
        if (centermass != null)
        {
            return centermass.position;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 GetDynamic()
    {
        return transform.up * force;
    }

}
