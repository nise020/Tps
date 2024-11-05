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
        MG,//�ӽŰ�
        SMG,//�Ⱓ����
        SR,//������
    }
    [SerializeField, Tooltip("���� Ÿ��")] public GunTags GunType;
    [SerializeField, Tooltip("������ ����Ʈ ����")] Color GunColor;
    [SerializeField, Tooltip("������ ����Ʈ ����")] public float GunRazer;
    [SerializeField, Tooltip("������ ����Ʈ Ȯ�ο���")] public bool RazerOn;

    [SerializeField, Tooltip("�ѱ�")] GameObject GunHole;
    [SerializeField, Tooltip("������ ���κ�")] GameObject RazerEndObj;
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
    /// ���� ����
    /// </summary>
    public void gunAngle(GunTags vlaue)//���� Ÿ�� �������� ��������
    {
        if (vlaue != GunTags.MG) 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);//��ŸƮ����
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
                //Ȯ���ʿ�
            }
        }
        
    }
    /// <summary>
    /// ���� ����
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
    /// ������ ����Ʈ ���
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
