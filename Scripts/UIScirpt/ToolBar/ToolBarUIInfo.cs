using UnityEngine;
using System.Collections;

public class ToolBarUIInfo : MonoBehaviour
{
    private UISprite itemSp;
    private UISprite selected;
    public ItemType itemType;
    public CubeType materialType;
    [SerializeField]
    private ItemInfo itemInfo;
    void Start ()
    {
        itemSp = Global.FindChild<UISprite>(transform, "item");
        selected = Global.FindChild<UISprite>(transform, "Selected");
        selected.alpha = 0;
        itemSp.spriteName = null;


    }
	

    public void SelectedTool(bool isSelected)
    {
        if (isSelected)
        {
            selected.alpha = 1;
        }
        else
        {
            selected.alpha = 0;
        }
        
    }



}
