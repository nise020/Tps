using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public partial class Player_Bullet : Gun
{
    Bullet BULLET = new Bullet();
    public bulletType BulletType = bulletType.Playerbullet;
    public Vector3 targetPos;//몬스터가 공격할 목표
    //public float Speed = 0.0f;

    [Header("총알 관련 항목")]
    int targetnumber;
    int bulletDamage = 1;
    float speed = 10.0f;
    //Vector3 targetPos;//몬스터가 공격할 목표
    RaycastHit hit;//총알이 맞출 목표



    private void Update()
    {
       transform.position = BULLET.moveing(transform.position, targetPos, BulletType, speed);
    }

    protected override void GunTargetRaycast()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {

        }
        //Vector3 ray = cam.ScreenToWorldPoint(Input.mousePosition);
        //if (Physics.Raycast(transform.position,ray, out RaycastHit hit))
    }
}
