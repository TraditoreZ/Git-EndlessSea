using UnityEngine;
using System.Collections.Generic;

public interface IObjManager
{
    void AddCube(Transform cube);

    void RemoveCube(Transform cube);

    List<CubeInfo> GetCubeList();

}
