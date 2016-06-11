using UnityEngine;
using System.Collections;
using System;

public class ItemInfo : MonoBehaviour
{
    [SerializeField]
    private ItemType _type;
    public string itemName;
    public int itemCount;
    public string itemDes;

    private UISprite sprite;
    void Start()
    {
        Instance();
    }

    private void Instance()
    {
        sprite = Global.FindChild<UISprite>(transform, "item");
        sprite.spriteName = Enum.GetName(typeof(ItemType), _type);
    }

    public void ChangeType(ItemType type)
    {
        if (sprite == null)
        {
            Instance();
        }
        _type = type;
        sprite.spriteName = Enum.GetName(typeof(ItemType), _type);
    }



}
