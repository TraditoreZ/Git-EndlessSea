using UnityEngine;
using System.Collections;
using System;

public class RegisterRudder : IRegisterPart
{
    public void Register(IObjManager objmanager, Transform targer)
    {
        (objmanager as IDirectionManager).RegistDirection(targer.GetComponent<IDirectionSystem>());
    }

    public void Remove(IObjManager objmanager, Transform targer)
    {
        (objmanager as IDirectionManager).RemoveDirection(targer.GetComponent<IDirectionSystem>());
    }
}
