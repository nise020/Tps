using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Gun : MonoBehaviour
{
    void Start()
    {
        cam = Camera.main;
        GunBulletType();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if ((Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            GunTargetRaycast();
            RazerOn = true;

        }
        else
        {
            RazerOn = false;
        }
    }
}
