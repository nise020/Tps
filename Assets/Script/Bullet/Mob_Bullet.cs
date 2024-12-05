using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Mob_Bullet : Monster
{
    Bullet BULLET = new Bullet();
    public bulletType BulletType = bulletType.Mobbullet;
    public Vector3 targetPos;//몬스터가 공격할 목표
    GunTags gunTags = GunTags.MG;
    //public float Speed = 0.0f;

    [Header("총알 관련 항목")]
    int targetnumber;
    int damage = 1;
    float speed = 10.0f;
    RaycastHit hit;//총알이 맞출 목표

    //protected override void OnTriggerEnter(Collider other)
    //{
    //    base.OnTriggerEnter(other);
    //}

    private void Update()
    {
        transform.position = BULLET.moveing(transform.position, targetPos, BulletType, speed);
    }

 
}
