using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public partial class Gun : MonoBehaviour
{
    enum GunTags 
    {
        MG,//�ӽŰ�
        SMG,//�Ⱓ����
        SR,//������
    }
    [SerializeField, Tooltip("���� Ÿ��")] GunTags GunEnumType;

    [SerializeField, Tooltip("������ ����Ʈ ����")] float GunRazer;
    [SerializeField, Tooltip("������ ����Ʈ Ȯ�ο���")] bool RazerOn;

    [SerializeField, Tooltip("�ѱ�")] GameObject GunHole;
    [SerializeField, Tooltip("��")] GameObject GunObj;
    [SerializeField, Tooltip("�� ȸ�� ����")] float GunRotSpeed = 0.0f;
    [SerializeField, Tooltip("������ ���κ�")] GameObject RazerEndObj;
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
    /// ���� ����
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
