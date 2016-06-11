using UnityEngine;
using System.Collections;

public class LoadingLogo : MonoBehaviour
{

    public float speed = -25;


    void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime, Space.Self);

    }
}
