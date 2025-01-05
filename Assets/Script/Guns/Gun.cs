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

    private void Start()
    {
        cam = UnityEngine.Camera.main;
        GunBulletType();
        beforeMyGunTrs = gameObject.transform.position;
        beforeMyGunRot = transform.rotation.eulerAngles;
        if (gunObj == null) { return; }
        gunObjRot = gunObj.transform.rotation.eulerAngles;
    }


    // Update is called once per frame
    private void Update()
    {
        bool value1 = Input.GetMouseButton(0);
        bool value2 = Input.GetKey(KeyCode.Space);
        if ((value1 || value2))
        {
            if (angleOn == true)
            {
                attackReady();
            }
            else if (angleOn == false)
            {
                GunTargetRaycast();
                //Shared.BattelMgr.
                //razerOn = true;
            }
        }
        else if ((Input.GetMouseButtonUp(0)))//위치 초기화,수정 필요(Quaternion)
        {
            bool pos = Vector3.Distance(transform.position, beforeMyGunTrs) > 0.1f;
            bool rot = Quaternion.Angle(transform.rotation, Quaternion.Euler(beforeMyGunRot)) > 0.1f;
            bool gun_Rot = Quaternion.Angle(gunObj.transform.rotation, Quaternion.Euler(gunObjRot)) > 0.1f;

            Debug.Log($"{pos}");
            Debug.Log($"{rot}");
            Debug.Log($"{gun_Rot}");
            Debug.Log($"Angle :{Quaternion.Angle(gunObj.transform.rotation, Quaternion.Euler(gunObjRot))}");

            if (pos || rot || gun_Rot)
            {
                //transform.position = beforeMyGunTrs;
                transform.rotation = Quaternion.Euler(beforeMyGunRot);
                gunObj.transform.rotation = Quaternion.Euler(gunObjRot);
                angleOn = true;
                razerOn = false;
            }
            else
            {
                Debug.Log("No changes detected. Skipping reset.");
                return;
            }

        }
        else { return; }
    }


}
