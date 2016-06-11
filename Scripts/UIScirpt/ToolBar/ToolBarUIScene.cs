using UnityEngine;
using System.Collections;
public delegate void CallUIToolChange(ItemType cubetye,CubeType materialType);
public class ToolBarUIScene : UIScene
{
    private UISceneWidget[] ToolBars;
    private ToolBarUIInfo[] ToolInfos;
    private ToolBarUIInfo currtTool;
    private CallUIToolChange uiToolDelegate;
    void Start()
    {
        base.Start();
        Transform toolbarRoot = Global.FindChild(transform, "GridTool");
        ToolBars = toolbarRoot.GetComponentsInChildren<UISceneWidget>();
        ToolInfos = toolbarRoot.GetComponentsInChildren<ToolBarUIInfo>();
        if (ToolBars!=null)
        {
            for (int i = 0; i < ToolBars.Length; i++)
            {
                ToolBars[i].OnMousePress += ButtonToolBarOnClick;
            }
        }

        //挂核心脚本委托  先做在这里
        //uiToolDelegate += CubeObjManager.instance.SetCubeType;
        uiToolDelegate += BuildingManager.instance.ChangeItemType;
    }


    private void ButtonToolBarOnClick(UISceneWidget eventObj,bool isDown)
    {
        Debug.Log(eventObj.name);
    }

    void Update()
    {
        OnToolKeyDown(KeyCode.Alpha1);
        OnToolKeyDown(KeyCode.Alpha2);
        OnToolKeyDown(KeyCode.Alpha3);
        OnToolKeyDown(KeyCode.Alpha4);
        OnToolKeyDown(KeyCode.Alpha5);
        OnToolKeyDown(KeyCode.Alpha6);
        OnToolKeyDown(KeyCode.Alpha7);
        OnToolKeyDown(KeyCode.Alpha8);
        OnToolKeyDown(KeyCode.Alpha9);
        OnToolKeyDown(KeyCode.Alpha0);
    }

    private void OnToolKeyDown(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            ToolBarUIInfo nextTool;
            int keyidex = (int)key - 49;
            // 把0键放到最后
            if (keyidex < 0)
                keyidex = 9;
            nextTool = ToolInfos[keyidex];

            if (nextTool!= currtTool)
            {
                if(currtTool!=null)
                    currtTool.SelectedTool(false);
                currtTool = nextTool;
                currtTool.SelectedTool(true);
                // 通过委托改变底层方块切换
                uiToolDelegate(currtTool.itemType,currtTool.materialType);
            }


        }
    }



}
