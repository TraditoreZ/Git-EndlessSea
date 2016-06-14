using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.Base.Build;
using ControlSystem;
using UnityStandardAssets.Characters.FirstPerson;

public class CannonOperation : MonoBehaviour, IControl
{
    private Transform horizontalTran;
    private Transform verticalTran;
    [SerializeField]
    private bool useing;
    private float speed = 5;
    private GameObject bullet;
    private Transform firePos;
    private Transform playerPos;
    private Transform EyePos;
    private Transform playerCameraParent;
    private bool fireCD;
    private int cdTime = 5;
    public Transform fireFx;
    void Start()
    {
        horizontalTran = Global.FindChild(transform, "Cylinder001");
        verticalTran = Global.FindChild(transform, "gun");
        firePos = Global.FindChild(transform, "Fire");
        playerPos = Global.FindChild(transform, "PlayerPos");
        EyePos = Global.FindChild(transform, "EyePos");
        bullet = Resources.Load("Expendables/CannonBullet") as GameObject;
        fireCD = true;
    }



    void Update()
    {
        if (!useing)
            return;

        horizontalTran.Rotate(Vector3.forward * Time.deltaTime * Input.GetAxis("Horizontal") * speed, Space.Self);
        verticalTran.Rotate(Vector3.right * Time.deltaTime * -Input.GetAxis("Vertical") * speed, Space.Self);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (fireCD)
            {
                Fire();
            }
        }


    }

    public void UserPart(Transform player)
    {
        useing = true;
        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponent<playerAnimManager>().enabled = false;
        player.GetComponent<BuildingManager>().enabled = false;
        player.transform.position = playerPos.position;
        player.transform.rotation = playerPos.rotation;
        playerCameraParent = Camera.main.transform.parent;
        Camera.main.transform.parent = EyePos;
        Camera.main.transform.localPosition = Vector3.zero;
        Camera.main.transform.localRotation = Quaternion.identity;
    }

    public void ExitPart(Transform player)
    {
        useing = false;
        player.GetComponent<FirstPersonController>().enabled = true;
        player.GetComponent<playerAnimManager>().enabled = true;
        player.GetComponent<BuildingManager>().enabled = true;
        Camera.main.transform.parent = playerCameraParent;
        Camera.main.transform.localPosition = Vector3.zero;
        Camera.main.transform.localRotation = Quaternion.identity;
    }


    public void AddEvent(IObjControllerManager objManager)
    {

    }

    public void RemoveEvent(IObjControllerManager objManager)
    {

    }

    private void Fire()
    {
        StartCoroutine(FireCD(cdTime));
        GameObjectPool.instance.CreateObject("CannonBullet", bullet, firePos.position, firePos.rotation);
        //Instantiate(bullet, firePos.position, firePos.rotation);
    }
    private IEnumerator FireCD(int time)
    {
        fireCD = false;
        fireFx.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        fireCD = true;
        fireFx.gameObject.SetActive(false);
    }

}
