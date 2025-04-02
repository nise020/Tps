using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Bullet_Monster : MonoBehaviour
{
    Bullet BULLET = new Bullet();
    bulletType BulletType = bulletType.Mobbullet;
    public Vector3 targetPos;//������ ��ǥ

    [Header("�Ѿ� ���� �׸�")]
    int bulletdamage = 1;
    float speed = 10.0f;
    bool hideCheck = false;
    bool coroutinRun = false;
    //RaycastHit hit;//�Ѿ��� ���� ��ǥ

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Player) ||
                other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Cover))
        {
            gameObject.transform.localPosition = new Vector3();
            gameObject.SetActive(false);
        }
    }

    IEnumerator invisible() 
    {
        if (coroutinRun)
            yield break;  // �̹� ���� ���̶�� �ߴ�

        coroutinRun = true;
        //hideCheck = !hideCheck;//false
        yield return new WaitForSeconds(0.5f);
        //hideCheck = !hideCheck;//true
        coroutinRun = false;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        gameObject.transform.position = BULLET.moveing(transform.position, targetPos, BulletType, speed);
    }

}
