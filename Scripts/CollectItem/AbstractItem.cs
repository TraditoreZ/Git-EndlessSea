using UnityEngine;
using System.Collections;

public abstract class AbstractItem:MonoBehaviour
{
    protected float life;//被收集物体的生命值

    protected bool beDestory;//是否被销毁

    public virtual void Awake()
    {
        life = 100.0f;
    }

    /// <summary>
    /// 受击检测
    /// </summary>
    public virtual void ShowBeHit(float impact)
    {
        this.life = this.life - impact;

        this.life = Mathf.Clamp(life, 0.0f, life);

        if ((this.life == 0) && beDestory == false)
        {
            beDestory = true;

            Destroy(this.gameObject);
        }
    }

    public void Destory()
    { 
        //将次收集物品加入到背包中

        //Player.BagManager.Add();
    }

}
