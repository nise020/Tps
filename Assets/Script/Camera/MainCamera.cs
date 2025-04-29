using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static ShakeCamera;

public partial class MainCamera : MonoBehaviour
{
    ShakeCamera shakeCamera;
    private void Start()
    {
        shakeCamera = GetComponent<ShakeCamera>();
    }
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space)) 
    //    {
    //        shakeCamera.Shake(0);
    //    }
    //}


    public void ShakeOn() 
    {
        //camZoom();
        //StartCoroutine(shakeCamera.ShakeCoroutin());
    }
    public void camZoom() 
    {
        float ScaleTime = 0.2f;
        float SlowTime = 3f;
        float SlowTimeTimeConvertSlow = ScaleTime * SlowTime;
        ZoomEndStage(0f, -1.5f, 2f, SlowTime - 1.5f, 1f, Vector3.zero);
    }
}
