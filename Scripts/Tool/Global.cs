using UnityEngine;
using System.Collections;

public static class Global 
  {
    public static Transform FindChild(Transform trans,string name)
    {
        var childs = trans.FindChild(name);
        if(childs!=null)
        {
            return childs;
        }
        int count = trans.childCount;
        Transform obj=null;
        for (int i = 0; i < count; i++)
        {
            var child = trans.GetChild(i);
            obj= FindChild(child, name);
            if (obj != null)
                return obj;
        }

        return null;
    }

    public static T FindChild<T>(Transform trans, string name) where T:Component
        
    {
      var obj=  FindChild(trans, name);
        if (obj == null)
            return default(T);
        return obj.GetComponent<T>();

    }










}
