using UnityEngine;
using System.Collections;
using System;

public class RotateCubeFactory : IRotateFactory
{
    private Vector3[] vec;
    private int idex = 1;
    private float PartRotate = 0;
    public RotateCubeFactory()
    {
        vec = new Vector3[9];

        vec[0] = new Vector3(0, 90, 0);
        vec[1] = new Vector3(0, 90, 0);
        vec[2] = new Vector3(0, 90, 0);
        vec[3] = new Vector3(0, 0, -90);
        vec[4] = new Vector3(0, 0, -90);
        vec[5] = new Vector3(0, 0, -90);
        vec[6] = new Vector3(-90, 0, 0);
        vec[7] = new Vector3(-90, 0, 0);
        vec[8] = new Vector3(-90, 0, 0);

    }

    public Vector3 GetRotate()
    {
        if (idex == vec.Length - 1)
            idex = 0;
        var currt = vec[idex];
        idex++;
        return currt;
    }

    public float GetPartRotate()
    {
        return PartRotate;
    }
    public void SetPartRotate()
    {
        if (PartRotate >= 360)
            PartRotate -= 270;
        else
            PartRotate += 90;
    }
}
