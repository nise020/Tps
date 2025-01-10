using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        ShakeOn();
    }


    private void ShakeOn() 
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Shared.ShakeCamera.Shake(0);
        }
    }
}
