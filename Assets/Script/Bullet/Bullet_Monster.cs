using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Bullet_Monster : MonoBehaviour
{
    Bullet BULLET = new Bullet();
    bulletType BulletType = bulletType.Mobbullet;
    public Vector3 targetPos;//���Ͱ� ������ ��ǥ

    [Header("�Ѿ� ���� �׸�")]
    int bulletdamage = 1;
    float speed = 10.0f;
    //RaycastHit hit;//�Ѿ��� ���� ��ǥ

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
