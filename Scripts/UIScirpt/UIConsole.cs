using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIConsole : MonoBehaviour
{

    // Use this for initialization
    private UILabel label;

    private Queue<string> queue = new Queue<string>();
    private bool isShow = false;
    void Start()
    {

        label = Global.FindChild<UILabel>(transform, "LabelConsole");
        label.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (isShow)
                    CloseConsole();
                else
                    OpenConsole();

            }
        }
    }


    private void OpenConsole()
    {
        label.gameObject.SetActive(true);
        Debugger.consoleDebug += ShowDebug;
        isShow = true;
    }
    private void CloseConsole()
    {
        label.gameObject.SetActive(false);
        Debugger.consoleDebug -= ShowDebug;
        isShow = false;
    }
    private void ShowDebug(string message)
    {
        string console = "";
        string[] tests;
        queue.Enqueue(message);
        if (queue.Count >= 24)
        {
            queue.Dequeue();
        }
        tests = queue.ToArray();
        for (int i = 0; i < tests.Length; i++)
        {
            console += tests[i] + "\r\n";
        }
        label.text = console;
    }



}
