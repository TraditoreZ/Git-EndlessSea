using UnityEngine;
using System.Collections;
using Develop;
public class InputController : MonoSingleton<InputController>
{
    public delegate void DelInput();
    public event DelInput OnMouseLeftDown;
    public event DelInput OnMouseRightDown;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        OnMouseLeftDown();
    //    }
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        OnMouseRightDown();
    //    }





    //}
}
