using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public partial class Gun : Soljer
{
    protected virtual void Start()
    {
        cam = Camera.main;
        GunBulletType();
        beforeTrs = transform.position;
        beforeRot = transform.rotation.eulerAngles;
        gunRot = gunObj.transform.rotation.eulerAngles;
    }


    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)))
        {
            if (angleOn == true) 
            {
                attackReady();
            }
            else if (angleOn == false) 
            {
                GunTargetRaycast();
                razerOn = true;
            }
            

        }
        else if ((Input.GetMouseButtonUp(0)))
        {
            bool pos = Vector3.Distance(transform.position, beforeTrs) > 0.1f;
            bool rot = Quaternion.Angle(transform.rotation, Quaternion.Euler(beforeRot)) > 0.1f;
            bool gun_Rot = Quaternion.Angle(gunObj.transform.rotation, Quaternion.Euler(gunRot)) > 0.1f;

            Debug.Log($"Position Changed: {pos}");
            Debug.Log($"Rotation Changed: {rot}");
            Debug.Log($"Gun Rotation Changed: {gun_Rot}");
            Debug.Log($"Angle Difference: {Quaternion.Angle(gunObj.transform.rotation, Quaternion.Euler(gunRot))}");
            
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
        //else { return; }
    }

    
}
