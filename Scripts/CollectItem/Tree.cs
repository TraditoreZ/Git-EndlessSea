using UnityEngine;
using System.Collections;

public class Tree : AbstractItem
{
    public override void Awake()
    {
        base.Awake();

        life = 100.0f;
    }

}
