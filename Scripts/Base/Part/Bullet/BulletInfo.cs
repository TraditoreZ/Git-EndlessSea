using UnityEngine;
using System.Collections;

public class BulletInfo : MonoBehaviour
{


    public float Power = 10;//这个代表发射时的速度/力度等，可以通过此来模拟不同的力大小
    public float Gravity = -10;//这个代表重力加速度
    public float lifeTime = 10;

    private Vector3 MoveSpeed;//初速度向量
    private Vector3 GritySpeed;//重力的速度向量，t时为0
    private float dTime;//已经过去的时间

    private Vector3 currentAngle;
    // Use this for initialization
    void OnEnable()
    {
        //通过一个公式计算出初速度向量
        //角度*力度
        MoveSpeed = transform.forward * Power;
        currentAngle = Vector3.zero;
        dTime = 0;
        GritySpeed = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        //计算物体的重力速度
        //v = at ;
        GritySpeed.y = Gravity * (dTime += Time.deltaTime);
        //位移模拟轨迹
        transform.position += (MoveSpeed + GritySpeed) * Time.deltaTime;
        currentAngle.z = Mathf.Atan((MoveSpeed.y + GritySpeed.y) / MoveSpeed.x) * Mathf.Rad2Deg;
        transform.eulerAngles = currentAngle;


        //超过时间自动销毁
        if (dTime > lifeTime)
        {
            BulletDestory();
        }
    }

    private void BulletDestory()
    {
        GameObjectPool.instance.MyDestory(gameObject);
    }



}
