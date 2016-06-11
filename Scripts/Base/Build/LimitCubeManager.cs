using Develop;

public class LimitCubeManager : MonoSingleton<LimitCubeManager>
{
    public CubeDirectionType[] Cube;
    public CubeDirectionType[] CubeHollow;
    public CubeDirectionType[] CubeHollowThin;
    public CubeDirectionType[] Cylinder;
    public CubeDirectionType[] CylinderTube;
    public CubeDirectionType[] CylinderTubeThin;
    public CubeDirectionType[] PrismOctagon;
    public CubeDirectionType[] PrismPentagon;
    public CubeDirectionType[] PrismTriangle;
    public CubeDirectionType[] Pyramid;
    public CubeDirectionType[] PyramidCorner;
    public CubeDirectionType[] Wedge;
    public CubeDirectionType[] Part;


    public CubeDirectionType[] GetCubeDirectionTypes(ItemType itemType)
    {
        CubeDirectionType[] currtTypes = null;

        switch (itemType)
        {
            case ItemType.Cube:
                currtTypes = Cube;
                break;
            case ItemType.Wedge:
                currtTypes = Wedge;
                break;
            case ItemType.CubeHollowThin:
                currtTypes = CubeHollowThin;
                break;
            case ItemType.CubeHollow:
                currtTypes = CubeHollow;
                break;
            case ItemType.Cylinder:
                currtTypes = Cylinder;
                break;
            case ItemType.CylinderTube:
                currtTypes = CylinderTube;
                break;
            case ItemType.CylinderTubeThin:
                currtTypes = CylinderTubeThin;
                break;
            case ItemType.PrismOctagon:
                currtTypes = PrismOctagon;
                break;
            case ItemType.PrismPentagon:
                currtTypes = PrismPentagon;
                break;
            case ItemType.PrismTriangle:
                currtTypes = PrismTriangle;
                break;
            case ItemType.Pyramid:
                currtTypes = Pyramid;
                break;
            case ItemType.PyramidCorner:
                currtTypes = PyramidCorner;
                break;
            default:
                currtTypes = Part;
                break;
        }


        return currtTypes;
    }


}
