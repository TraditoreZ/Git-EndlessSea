using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.Base.Part.DynamicSystem;

public class RegisterFan : IRegisterPart
{
    public void Register(IObjManager objmanager, Transform targer)
    {
        (objmanager as IDynamicManager).RegistDynamic(targer.GetComponent<IDynamic>());
    }

    public void Remove(IObjManager objmanager, Transform targer)
    {
        (objmanager as IDynamicManager).RemoveDynamic(targer.GetComponent<IDynamic>());
    }
}
