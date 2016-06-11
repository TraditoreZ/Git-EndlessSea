using UnityEngine;
using ControlSystem;

public class RegisterWeapon : IRegisterPart
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
