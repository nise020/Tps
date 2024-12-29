using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public partial class Gun : Actor
{

    [SerializeField, Tooltip("레이저 사이트 길이")] float gunRazer;
    [SerializeField, Tooltip("레이저 사이트 확인여부")] bool razerOn;

    [SerializeField, Tooltip("총구")] GameObject gunHoleObj;
    [SerializeField, Tooltip("총")] GameObject gunObj;
    [SerializeField, Tooltip("총알")] GameObject bulletObj;
    [SerializeField, Tooltip("총알 생섭탭")] Transform creatTabObj;
    [SerializeField, Tooltip("총 회전 감도")] float gunRotSpeed = 0.0f;
    [SerializeField, Tooltip("레이저 끝부분")] GameObject razerEndObj;
    [SerializeField, Tooltip("총 회전 On,Off")] bool angleOn = true;
    Camera cam;
    LineRenderer gunLazer;
    
    public void attackReady()
    {
        Vector3 pos = beforeMyGunTrs;
        Vector3 rot = beforeMyGunRot;
        transform.position = new Vector3(pos.x + 2.5f, pos.y, pos.z);
        transform.rotation = Quaternion.Euler(rot.x, -270, rot.z);
        angleOn = false;
    }
    protected virtual void GunTargetRaycast() 
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            string layerName = LayerMask.LayerToName(hit.collider.gameObject.layer);
            if (layerName != ("Cover"))//엄폐물에 안 닿기
            {
                AimGun(hit);
                GunAttack(hit);
            }
        }
    }
    public void AimGun(RaycastHit _hit)
    {
        Vector3 targetPos = _hit.point;
        //Vector3 targetPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = (targetPos - gunObj.transform.position);
        Quaternion startRot = Quaternion.LookRotation(gunObj.transform.forward);
        Quaternion endRot = Quaternion.LookRotation(dir.normalized);
        gunObj.transform.rotation = Quaternion.Lerp(startRot, endRot, gunRotSpeed * Time.deltaTime);

        // 상하 각도 제한
        Vector3 eulerRotation = gunObj.transform.eulerAngles;
        eulerRotation.x = clampAngle(eulerRotation.x, -45f, 45f);
        eulerRotation.z = 0f;
        gunObj.transform.eulerAngles = eulerRotation;

        Debug.DrawLine(gunHoleObj.transform.position, targetPos, Color.red);
    }

    private float clampAngle(float _angle, float _min, float _max)
    {
        if (_angle > 180) // -180 ~ 180도로 변환
        {
            _angle -= 360; 
        }
        return Mathf.Clamp(_angle, _min, _max);
    }

    public void GunAttack(RaycastHit _hit)
    {
        if (bullet==0) { return; }
        //if (_hit.collider == LayerMask.LayerToName("Monster")) {  return; }
        GameObject go = Instantiate(bulletObj, gunHoleObj.transform.position, 
            Quaternion.identity, creatTabObj);
        Bullet_Player plBullet = go.GetComponent<Bullet_Player>();
        plBullet.targetPos = _hit.point;
        
        bullet--;
        //go.transform.position += _hit.point;
    }

    public void Lazer() 
    {
        gunLazer = GetComponent<LineRenderer>();
    }
    
    
}
