using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public partial class Gun : Soljer
{
    
    [SerializeField, Tooltip("���� Ÿ��")] protected GunTags GunEnumType;

    [SerializeField, Tooltip("������ ����Ʈ ����")] float GunRazer;
    [SerializeField, Tooltip("������ ����Ʈ Ȯ�ο���")] bool RazerOn;

    [SerializeField, Tooltip("�ѱ�")] GameObject GunHole;
    [SerializeField, Tooltip("��")] GameObject GunObj;
    [SerializeField, Tooltip("�� ȸ�� ����")] float GunRotSpeed = 0.0f;
    [SerializeField, Tooltip("������ ���κ�")] GameObject RazerEndObj;
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
    /// ���� ����
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

    //protected virtual void Chargeing()//�ѱ� �׼�,(���ε� �ƴ�!)
    //{
    //    GunHoleObj.SetActive(false);
    //    ChargeingTimer += Time.deltaTime;
    //    if (ChargeingTimer > ChargeingTime)//���콺 ������ ���� �ʿ�
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
