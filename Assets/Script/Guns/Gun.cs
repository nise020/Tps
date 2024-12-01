using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Gun : Soljer
{
    private void Start()
    {
        cam = Camera.main;
        GunBulletType();
        beforeTrs = transform.position;
        beforeRot = transform.rotation.eulerAngles;
        gunRot = gunObj.transform.rotation.eulerAngles;
    }


    // Update is called once per frame
    protected virtual void Update()
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
                //gunAttack();//수정필요
                razerOn = true;
            }
        }
        else if ((Input.GetMouseButtonUp(0)))//위치 초기화,수정 필요(Quaternion)
        {
            bool pos = Vector3.Distance(transform.position, beforeTrs) > 0.1f;
            bool rot = Quaternion.Angle(transform.rotation, Quaternion.Euler(beforeRot)) > 0.1f;
            bool gun_Rot = Quaternion.Angle(gunObj.transform.rotation, Quaternion.Euler(gunRot)) > 0.1f;

            Debug.Log($"{pos}");
            Debug.Log($"{rot}");
            Debug.Log($"{gun_Rot}");
            Debug.Log($"Angle :{Quaternion.Angle(gunObj.transform.rotation, Quaternion.Euler(gunRot))}");
            
            if (pos || rot || gun_Rot) 
            {
                transform.position = beforeTrs;
                transform.rotation = Quaternion.Euler(beforeRot);
                gunObj.transform.rotation = Quaternion.Euler(gunRot);
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
