using UnityEngine;
using System.Collections;

public interface IRegisterPart
{
    void Register(IObjManager objmanager,Transform targer);
    void Remove(IObjManager objmanager,Transform targer);
}
