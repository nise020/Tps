using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public partial class MainCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //ShakeOn();
    }


    private void ShakeOn() 
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            //Shared.ShakeCamera.Shake(0);
            camZoom();
        }
    }
    public void camZoom() 
    {
        float ScaleTime = 0.2f;
        float SlowTime = 3f;
        float SlowTimeTimeConvertSlow = ScaleTime * SlowTime;
        ZoomEndStage(0f, -1.5f, 2f, SlowTime - 1.5f, 1f, Vector3.zero);
    }
}
