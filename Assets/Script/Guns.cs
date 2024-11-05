using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Guns : MonoBehaviour
{
    public enum GunTags 
    {
        MG,//머신건
        SMG,//기간단총
        SR,//저격총
    }
    [SerializeField, Tooltip("총의 타입")] public GunTags GunType;
    [SerializeField, Tooltip("레이저 사이트 색깔")] Color GunColor;
    [SerializeField, Tooltip("레이저 사이트 길이")] public float GunRazer;
    [SerializeField, Tooltip("레이저 사이트 확인여부")] public bool RazerOn;

    [SerializeField, Tooltip("총구")] GameObject GunHole;
    [SerializeField, Tooltip("레이저 끝부분")] GameObject RazerEndObj;
    [SerializeField, Tooltip("Auto Butten")] Button AutoBut;
    int bullet;
    Camera cam;
    void Start()
    {
        cam = Camera.main;
        GunBulletType();
    }

    private void GunBulletType()
    {
        if (GunType == GunTags.MG)
        {
            bullet = 30;
        }
        else if(GunType == GunTags.SMG) 
        {
            bullet = 300;
        }
        else if(GunType == GunTags.SR) 
        {
            bullet = 5;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButton(0)||Input.GetKeyDown(KeyCode.Space))&& AutoBut == false)
        {
            RazerOn = true;
            gunAngle(GunType);
            TargetingRazer(RazerOn);
            //gunAngle();
            gunAngles();
        }
        else 
        {
            RazerOn = false;
        }
    }
    /// <summary>
    /// 총의 각도
    /// </summary>
    public void gunAngle(GunTags vlaue)//몬스터 타격 관련으로 수정예정
    {
        if (vlaue != GunTags.MG) 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);//스타트지점
            RaycastHit Rayhit;
            if (Physics.Raycast(ray, out Rayhit))
            {
                Vector3 Hitpos = Rayhit.point;
                Vector3 pos = (Hitpos - GunHole.transform.position).normalized;
                Quaternion Angle = Quaternion.LookRotation(pos);
                transform.rotation = Angle;
                //Debug.Log($"{algle}");
                //transform.rotation = algle;
                Debug.DrawLine(transform.position, Hitpos, Color.red);
                //확인필요
            }
        }
        
    }
    /// <summary>
    /// 총의 각도
    /// </summary>
    public void gunAngles() 
    {
        Vector3 pos = transform.position - RazerEndObj.transform.position;
        //Vector3 mousePos = cam.ScreenToWorldPoint(pos);
        //Quaternion algleA = Quaternion.LookRotation(RazerEndObj.transform.forward);
        //Quaternion algleB = Quaternion.LookRotation(pos.normalized);
        transform.rotation = Quaternion.LookRotation(pos.normalized);
    }
    /// <summary>
    /// 레이저 사이트 기능
    /// </summary>
    /// <param name="value"></param>
    public void TargetingRazer(bool value) 
    {
        if(value == true) 
        {
            if(value==true) 
            {
                RazerOn = true;
                //Debug.DrawLine(GunHole.transform.position, GunHole.transform.position - new Vector3(0, 0, -GunRazer),Color.red);
                //Debug.DrawLine(GunHole.transform.position, mo,Color.red);
                //Gizmos.DrawSphere(transform.position, transform.localPosition - new Vector3(GunRazer, 0, 0);
            }

        }
    }
}
