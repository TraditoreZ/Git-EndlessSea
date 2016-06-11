using UnityEngine;
using System.Collections;

public class Stone : AbstractItem
{
    public override void Awake()
    {
        base.Awake();

        life = 200.0f;
    }
   
}
