using ControlSystem;
using System;
using UnityEngine;

public class RegisterDuo : IRegisterPart
{
    public void Register(IObjManager objmanager, Transform targer)
    {
        (objmanager as IObjControllerManager).RegistController(targer.GetComponent<IControl>());
    }

    public void Remove(IObjManager objmanager, Transform targer)
    {
        (objmanager as IObjControllerManager).RemoveController(targer.GetComponent<IControl>());
    }
}