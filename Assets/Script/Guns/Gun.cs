using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Gun : Actor
{
    Vector3 beforeMyGunTrs;//해당스크립트의 pos
    Vector3 beforeMyGunRot;//해당스크립트의 rot
    Vector3 gunObjRot;//총 오브젝트의 pos
    Player PLAYER;
    private void Start()
    {
        ui = Shared.BattelMgr.ui;
        PLAYER = GetComponentInParent<Player>();
        cam = UnityEngine.Camera.main;
        GunBulletType();
        beforeMyGunTrs = gameObject.transform.position;
        beforeMyGunRot = transform.rotation.eulerAngles;
        if (gunObj == null) { return; }
        gunObjRot = gunObj.transform.rotation.eulerAngles;
        creatTabObj = Shared.BattelMgr.creatTab;
    }


    // Update is called once per frame
    private void Update()
    {
        bool value1 = Input.GetMouseButton(0);
        //bool value2 = Input.GetKey(KeyCode.Space);
        if ((value1))
        {
            GunTargetRaycast();
        }
        else if ((Input.GetMouseButtonUp(0)))//위치 초기화,수정 필요(Quaternion)
        {
            //bool gun_Rot = Quaternion.Angle(gunObj.transform.rotation, Quaternion.Euler(gunObjRot)) > 0.1f;

            //if (gun_Rot)
            //{
            //    gunObj.transform.rotation = Quaternion.Euler(gunObjRot);
            //}
            //else
            //{
            //    Debug.Log("No changes detected. Skipping reset.");
            //    return;
            //}
            Shared.BattelMgr.MOVECAM.cameraShakeAnim(false);
        }
        else { return; }
        bulletreloed();
    }


}
