using UnityEngine;
using System.Collections;
using Aid;
using System;

public class CubeAidInfo : MonoBehaviour, IAid
{
    [SerializeField]
    private int ColliderCount;
    [SerializeField]
    private float size;

    public LayerMask CheckLayers;

    private Material mat;

    private Color _color;

    void Awake()
    {
        size = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 6;
        mat = transform.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        var Colliders = Physics.OverlapSphere(transform.position, 0.2f, CheckLayers);

        ColliderCount = Colliders.Length;
    }

    public bool CheckObstacle()
    {
        return ColliderCount == 0 ? true : false;

    }

    public void SetColor(Color color)
    {
        mat.SetColor("_Color", color);
    }

    public Color GetColor()
    {
        return _color;
    }
}
