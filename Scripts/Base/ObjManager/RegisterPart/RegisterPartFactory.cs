using UnityEngine;
using System.Collections;

public class RegisterPartFactory
{
    public static IRegisterPart SeletePart(ItemType type)
    {
        IRegisterPart registerPart = null;
        switch (type)
        {
            case ItemType.none:
                break;
            case ItemType.Cube:
                break;
            case ItemType.Wedge:
                break;
            case ItemType.CubeHollowThin:
                break;
            case ItemType.CubeHollow:
                break;
            case ItemType.Cylinder:
                break;
            case ItemType.CylinderTube:
                break;
            case ItemType.CylinderTubeThin:
                break;
            case ItemType.PrismOctagon:
                break;
            case ItemType.PrismPentagon:
                break;
            case ItemType.PrismTriangle:
                break;
            case ItemType.Pyramid:
                break;
            case ItemType.PyramidCorner:
                break;
            case ItemType.Cannon:
                registerPart = new RegisterWeapon();
                break;
            case ItemType.Duo:
                registerPart = new RegisterDuo();
                break;
            case ItemType.Fan:
                registerPart = new RegisterFan();
                break;
            case ItemType.Rudder:
                registerPart = new RegisterRudder();
                break;
            default:
                break;
        }
        return registerPart;
    }

}

