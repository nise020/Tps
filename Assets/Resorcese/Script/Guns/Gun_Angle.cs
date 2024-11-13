using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public partial class Gun : MonoBehaviour
{
    enum GunTags 
    {
        MG,//머신건
        SMG,//기간단총
        SR,//저격총
    }
    [SerializeField, Tooltip("총의 타입")] GunTags GunEnumType;

    [SerializeField, Tooltip("레이저 사이트 길이")] float GunRazer;
    [SerializeField, Tooltip("레이저 사이트 확인여부")] bool RazerOn;

    [SerializeField, Tooltip("총구")] GameObject GunHole;
    [SerializeField, Tooltip("총")] GameObject GunObj;
    [SerializeField, Tooltip("총 회전 감도")] float GunRotSpeed = 0.0f;
    [SerializeField, Tooltip("레이저 끝부분")] GameObject RazerEndObj;
    Camera cam;

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
        GunObj.transform.rotation = Quaternion.Slerp(StartRotation, EndRotation, GunRotSpeed * Time.deltaTime);
        
        Debug.Log($"{targetPoint}");
        Debug.Log($"{Dir}");
        //GunObj.transform.rotation = Quaternion.LookRotation(Dir);
        Debug.Log($"{EndRotation}");

        
        Debug.DrawLine(GunHole.transform.position, targetPoint, Color.red);
    }
 

}
