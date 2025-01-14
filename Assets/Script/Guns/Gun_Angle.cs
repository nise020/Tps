using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public partial class Gun : Actor
{
    Battel_UI ui;
    public bool reLoed = false;

    [SerializeField] float gunRazer;
    [SerializeField] bool razerOn;

    [SerializeField] GameObject gunHoleObj;//gunHole
    [SerializeField] GameObject gunObj;//gun
    [SerializeField] Bullet_Player bulletObj;//bullet
    [SerializeField] Transform creatTabObj;
    [SerializeField] float gunRotSpeed = 0.0f;
    [SerializeField] GameObject razerEndObj;
    [SerializeField] bool angleOn = true;
    UnityEngine.Camera cam;
    LineRenderer gunLazer;

    //private void attackReady()
    //{
    //    Vector3 pos = beforeMyGunTrs;
    //    Vector3 rot = beforeMyGunRot;
    //    //transform.position = new Vector3(pos.x + 2.5f, pos.y, pos.z);
    //    //transform.rotation = Quaternion.Euler(rot.x, -270, rot.z);
    //    angleOn = false;
    //}
    protected virtual void GunTargetRaycast() 
    {
        if (nowbullet == 0) { return; }
        Vector3 AimPos = Shared.BattelMgr.camAim.transform.position;
        Vector3 AimDirection = Shared.BattelMgr.camAim.transform.forward;

        if (Physics.Raycast(AimPos, AimDirection, out RaycastHit hit))
        {
            AimGun(hit.point);
            float value = Vector3.Dot(AimDirection.normalized, gunHoleObj.transform.forward.normalized);
            if (value < 0.01) 
            {
                GunAttack(gunHoleObj.transform.forward);//에러 
            }

        }
    }
    //public void reloedcheck() 
    //{
    //    if (nowbullet == 0 ||Input.GetKeyDown(KeyCode.Mouse1)) 
    //    {
    //        reLoed = !reLoed;
    //    }
    //    reloed();
    //}
    //public void reloed() 
    //{
    //    if (reLoed == true)
    //    {
    //        PLAYER.reloding();
    //    }
    //    else { return; }
    //}
    public void GunAttack(Vector3 _hit)
    {
        RapidTimer += Time.deltaTime;
        if (RapidTimer > RapidTime) 
        {
            Shared.BattelMgr.MOVECAM.cameraShakeAnim(true);

            GameObject go = Instantiate(bulletObj.gameObject, gunHoleObj.transform.position,
                Quaternion.identity, creatTabObj);

            Bullet_Player plBullet = go.GetComponent<Bullet_Player>();
            plBullet.targetPos = _hit;
            nowbullet--;
            RapidTimer = 0.0f;
        }
        //go.transform.position += _hit.point;
    }
    public void AimGun(Vector3 _hit)//Aim 오브젝트를 기준으로 바꿔야함
    {
        Vector3 targetPos = _hit;
        Debug.Log($" _hit = {_hit}");
        Vector3 distanse = (targetPos - gunObj.transform.position);
        Quaternion startRot = Quaternion.LookRotation(gunObj.transform.forward);
        Quaternion endRot = Quaternion.LookRotation(distanse.normalized);
        gunObj.transform.rotation = Quaternion.Lerp(startRot, endRot, gunRotSpeed);//* Time.deltaTime

        // 상하 각도 제한
        //Vector3 eulerRotation = gunObj.transform.eulerAngles;
        //eulerRotation.x = clampAngle(eulerRotation.x, -45f, 45f);
        //eulerRotation.z = 0f;
        //gunObj.transform.eulerAngles = eulerRotation;
        
        Debug.DrawLine(gunHoleObj.transform.position, targetPos, Color.red);
    }

    public void Lazer() 
    {
        gunLazer = GetComponent<LineRenderer>();
    }
    
    
}
