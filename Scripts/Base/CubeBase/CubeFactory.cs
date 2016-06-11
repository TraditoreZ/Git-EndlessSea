using UnityEngine;
using System.Collections.Generic;
using Develop;
using System;

public class CubeFactory : MonoSingleton<CubeFactory>
{
    private Dictionary<CubeType, Material> cubeMaterials;
    private Dictionary<CubeType, PhysicMaterial> cubePhysicMaterials;
    void Start()
    {
        cubeMaterials = new Dictionary<CubeType, Material>();
        cubePhysicMaterials = new Dictionary<CubeType, PhysicMaterial>();


        string[] cubetypes = Enum.GetNames(typeof(CubeType));
        for (int i = 0; i < Enum.GetNames(typeof(CubeType)).GetLength(0); i++)
        {
            string path = string.Empty;
            path = cubetypes[i];
            var matobj = Resources.Load("Materials/" + path);
            if (matobj != null)
            {
                cubeMaterials.Add((CubeType)(i), matobj as Material);
            }
            var physicObj = Resources.Load("PhysicsMaterials/" + path);
            if (matobj != null)
            {
                cubePhysicMaterials.Add((CubeType)(i), physicObj as PhysicMaterial);
            }


        }
    }

    public Material GetCubeMaterial(CubeType type)
    {
        if (cubeMaterials.ContainsKey(type))
        {
            return cubeMaterials[type];
        }
        else
        {
            return null;
        }
    }

    public PhysicMaterial GetCubePhysicMaterial(CubeType type)
    {
        if (cubePhysicMaterials.ContainsKey(type))
        {
            return cubePhysicMaterials[type];
        }
        else
        {
            return null;
        }
    }
}
