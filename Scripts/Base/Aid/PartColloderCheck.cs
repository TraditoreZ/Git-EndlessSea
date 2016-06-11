using UnityEngine;
using System.Collections;

public class PartColloderCheck : MonoBehaviour
{
    public int ObstacleCount;


    void OnTriggerEnter(Collider other)
    {
        if (other != null)
            ObstacleCount += 1;
    }

    void OnTriggerExit(Collider other)
    {
        if (other != null)
            ObstacleCount -= 1;
    }

    void OnEnable()
    {
        ObstacleCount = 0;
    }

    void OnDisable()
    {
        ObstacleCount = 0;
    }

}
