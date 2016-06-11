using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using ControlSystem;

public class PlayerController : NetworkBehaviour
{
    private RaycastHit rayHit;
    private float RayLengh = 2;
    private BuildingManager buildManager;

    /// <summary>
    /// 当前载具
    /// </summary>
    private IControl currtControl;
    /// <summary>
    /// 是否正在使用载具
    /// </summary>
    private bool usingPart;
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        var cameraPos = Global.FindChild(transform, "CameraPos");
        if (cameraPos != null && Camera.main != null)
        {
            Camera.main.transform.parent = cameraPos;
            Camera.main.transform.localPosition = Vector3.zero;
            Camera.main.transform.localRotation = Quaternion.identity;
        }
        buildManager = transform.GetComponent<BuildingManager>();
        buildManager.enabled = true;
        buildManager.PlayerCamera = Camera.main;
        foreach (var item in gameObject.GetComponentsInChildren<Transform>())
        {
            item.gameObject.layer = LayerMask.NameToLayer("PlayerSelf");
        }
        usingPart = false;
    }


    void Update()
    {
        if (usingPart)
        {
            ExitPart();
        }
        else
        {
            UpdateRayCast();
        }



    }

    private void UpdateRayCast()
    {
        if (Physics.Raycast(buildManager.ray, out rayHit, RayLengh))
        {

            if (rayHit.collider.tag == "Part")
            {
                var locaParent = rayHit.collider.GetComponent<PartLocationParent>();
                if (locaParent != null && locaParent.parent.GetComponent<PartInfo>() != null && locaParent.parent.GetComponent<PartInfo>().Use)
                {
                    UsePart(locaParent);
                    UIManager.instance.GetUI<UISYSPrompt>(UIName.UISYSPromptPanel).Show();
                }

            }


            //将来有其他需求写在if else中间  留最后一个else关闭ui
        }
        else
        {
            if (UIManager.instance.GetUI<UISYSPrompt>(UIName.UISYSPromptPanel) != null)
            {
                UIManager.instance.GetUI<UISYSPrompt>(UIName.UISYSPromptPanel).Hide();
            }
        }
    }


    /// <summary>使用配件</summary> 
    private void UsePart(PartLocationParent locationParent)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            var partControl = locationParent.parent.GetComponent<IControl>();
            partControl.UserPart(transform);
            usingPart = true;
            currtControl = partControl;

        }
    }

    /// <summary>
    /// 离开配件
    /// </summary>
    /// <param name="locationParent"></param>
    private void ExitPart()
    {
        if (Input.GetKeyDown(KeyCode.F) && usingPart == true && currtControl != null)
        {
            currtControl.ExitPart(transform);
            usingPart = false;
            currtControl = null;
        }
    }

}
