using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public partial class Gun : Soljer
{
    
    [SerializeField, Tooltip("총의 타입")] protected GunTags GunEnumType;

    [SerializeField, Tooltip("레이저 사이트 길이")] float GunRazer;
    [SerializeField, Tooltip("레이저 사이트 확인여부")] bool RazerOn;

    [SerializeField, Tooltip("총구")] GameObject GunHole;
    [SerializeField, Tooltip("총")] GameObject GunObj;
    [SerializeField, Tooltip("총 회전 감도")] float GunRotSpeed = 0.0f;
    [SerializeField, Tooltip("레이저 끝부분")] GameObject RazerEndObj;
    Camera cam;
    LineRenderer gunLazer;

    public void GunTargetRaycast() 
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            AimGun(hit);
        }
        //Vector3 ray = cam.ScreenToWorldPoint(Input.mousePosition);
        //if (Physics.Raycast(transform.position,ray, out RaycastHit hit))
    }
    /// <summary>
    /// 총의 각도
    /// </summary>
    public void AimGun(RaycastHit _hit)
    {
        Vector3 targetPoint = _hit.point;
        //Vector3 targetPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 Dir = (targetPoint - GunObj.transform.position);
        Quaternion StartRotation = Quaternion.LookRotation(GunObj.transform.forward);
        Quaternion EndRotation = Quaternion.LookRotation(Dir.normalized);
        GunObj.transform.rotation = Quaternion.Lerp(StartRotation, EndRotation, GunRotSpeed * Time.deltaTime);
        
        
        Debug.DrawLine(GunHole.transform.position, targetPoint, Color.red);
    }
    public void Lazer() 
    {
        gunLazer = GetComponent<LineRenderer>();
    }

    //protected virtual void Chargeing()//총구 액션,(리로드 아님!)
    //{
    //    GunHoleObj.SetActive(false);
    //    ChargeingTimer += Time.deltaTime;
    //    if (ChargeingTimer > ChargeingTime)//마우스 땟을때 동작 필요
    //    {
    //        if (GunType == GunTags.SR && Input.GetKeyUp(KeyCode.Mouse0))
    //        {
    //            Debug.Log("SR");
    //            GunHoleObj.SetActive(true);
    //            ChargeingTimer = 0.0f;
    //        }
    //        else
    //        {
    //            Debug.Log("Not SR");
    //            GunHoleObj.SetActive(true);
    //            ChargeingTimer = 0.0f;
    //        }
    //    }
    //}
}
