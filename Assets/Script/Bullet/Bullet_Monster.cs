using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Bullet_Monster : MonoBehaviour
{
    Bullet BULLET = new Bullet();
    bulletType BulletType = bulletType.Mobbullet;
    public Vector3 targetPos;//몬스터가 공격할 목표

    [Header("총알 관련 항목")]
    int bulletdamage = 1;
    float speed = 10.0f;
    //RaycastHit hit;//총알이 맞출 목표

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Player) ||
                other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Cover))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        gameObject.transform.position = BULLET.moveing(transform.position, targetPos, BulletType, speed);
    }

 
}
