using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public partial class Bullet_Player : MonoBehaviour
{
    Bullet BULLET = new Bullet();
    bulletType BulletType = bulletType.Playerbullet;
    public Vector3 targetPos;//������ ��ǥ
    //public float Speed = 0.0f;

    [Header("�Ѿ� ���� �׸�")]
    int targetnumber;
    int bulletDamage = 1;
    float speed = 20.0f;
    //Vector3 targetPos;//���Ͱ� ������ ��ǥ
    RaycastHit hit;//�Ѿ��� ���� ��ǥ



    private void Update()
    {
       transform.position = BULLET.moveing(transform.position, targetPos, BulletType, speed);
    }

    //protected override void GunTargetRaycast()
    //{
    //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    //    if (Physics.Raycast(ray, out hit))
    //    {

    //    }
    //    //Vector3 ray = cam.ScreenToWorldPoint(Input.mousePosition);
    //    //if (Physics.Raycast(transform.position,ray, out RaycastHit hit))
    //}
}
