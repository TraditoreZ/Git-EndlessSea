using UnityEngine;
using System.Collections;
using Aid;
using System;

public class PartAidInfo : MonoBehaviour, IAid
{
    [HideInInspector]
    public Vector3 partPoint;

    private Color _color;
    void Awake()
    {
        partPoint = transform.position;
        meshR = transform.GetComponentsInChildren<MeshRenderer>();
        var colloders = transform.GetComponentsInChildren<Collider>();
        if (colloders != null)
        {
            checkColloders = new PartColloderCheck[colloders.Length];
            for (int i = 0; i < colloders.Length; i++)
            {
                checkColloders[i] = colloders[i].gameObject.AddComponent<PartColloderCheck>();
            }
        }
    }

    private PartColloderCheck[] checkColloders;
    private MeshRenderer[] meshR;
    public bool CheckObstacle()
    {
        int count = 0;
        for (int i = 0; i < checkColloders.Length; i++)
        {
            count += checkColloders[i].ObstacleCount;
        }
        return count == 0 ? true : false;
    }



    public void SetColor(Color color)
    {
        if (meshR == null)
        {
            return;
        }
        for (int i = 0; i < meshR.Length; i++)
        {
            meshR[i].material.SetColor("_Color", color);
        }
        _color = color;
    }

    public Color GetColor()
    {
        return _color;
    }
}
