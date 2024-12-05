using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public partial class Gun : Soljer
{
    [SerializeField, Tooltip("���� Ÿ��")] protected GunTags GunEnumType;

    [SerializeField, Tooltip("������ ����Ʈ ����")] float gunRazer;
    [SerializeField, Tooltip("������ ����Ʈ Ȯ�ο���")] bool razerOn;

    [SerializeField, Tooltip("�ѱ�")] GameObject gunHoleObj;
    [SerializeField, Tooltip("��")] GameObject gunObj;
    [SerializeField, Tooltip("�Ѿ�")] GameObject bulletObj;
    [SerializeField, Tooltip("�Ѿ� ������")] Transform creatTabObj;
    [SerializeField, Tooltip("�� ȸ�� ����")] float gunRotSpeed = 0.0f;
    [SerializeField, Tooltip("������ ���κ�")] GameObject razerEndObj;
    [SerializeField, Tooltip("�� ȸ�� On,Off")] bool angleOn = true;
    protected Camera cam;
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
            AimGun(hit);
            gunAttack(hit);
        }
        //Vector3 ray = cam.ScreenToWorldPoint(Input.mousePosition);
        //if (Physics.Raycast(transform.position,ray, out RaycastHit hit))
    }
    public void gunAttack(RaycastHit _hit)
    {
        if (bullet==0) { return; }
        //if (_hit.collider == LayerMask.LayerToName("Monster")) {  return; }
        GameObject go = Instantiate(bulletObj, gunHoleObj.transform.position, 
            Quaternion.identity, creatTabObj);
        Player_Bullet plBullet = go.GetComponent<Player_Bullet>();
        plBullet.targetPos = _hit.point;
        
        bullet--;
        //go.transform.position += _hit.point;
    }
    /// <summary>
    /// ���� ����
    /// </summary>
    public void AimGun(RaycastHit _hit)
    {
        Vector3 targetPos = _hit.point;
        //Vector3 targetPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = (targetPos - gunObj.transform.position);
        Quaternion startRot = Quaternion.LookRotation(gunObj.transform.forward);
        Quaternion endRot = Quaternion.LookRotation(dir.normalized);
        gunObj.transform.rotation = Quaternion.Lerp(startRot, endRot, gunRotSpeed * Time.deltaTime);
        
        
        Debug.DrawLine(gunHoleObj.transform.position, targetPos, Color.red);
    }
    public void Lazer() 
    {
        gunLazer = GetComponent<LineRenderer>();
    }
    
    
}
