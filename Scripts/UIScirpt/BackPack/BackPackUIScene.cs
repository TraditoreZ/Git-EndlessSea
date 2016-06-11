using UnityEngine;
using System.Collections;

public class BackPackUIScene : UIScene {


	void Start ()
    {
        base.Start();
        Transform itemRoot = Global.FindChild(transform, "Grid");
        var objItem=Resources.Load("UIPrefab/ItemBox");
        if(objItem!=null)
        {
            for (int i = 0; i < 40; i++)
            {
                GameObject Item = Instantiate(objItem) as GameObject;
                Item.transform.parent = itemRoot;
                Item.transform.localScale = new Vector3(1, 1, 1);
            }
        }
       
	
	}
	
}
