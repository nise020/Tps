using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public partial class Bullet_Player : MonoBehaviour
{
    Bullet BULLET = new Bullet();
    BulletType BulletType = BulletType.Playerbullet;
    public Vector3 targetPos;//������ ��ǥ
    //public float Speed = 0.0f;

    [Header("�Ѿ� ���� �׸�")]
    int targetnumber;
    int bulletDamage = 1;
    float speed = 100.0f;

    bool hideCheck = false;
    bool coroutinRun = false;
    //Vector3 targetPos;//���Ͱ� ������ ��ǥ
    RaycastHit hit;//�Ѿ��� ���� ��ǥ
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Delivery.LayerNameEnum(LayerName.Monster) ||
                other.gameObject.layer == Delivery.LayerNameEnum(LayerName.Cover))
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
        yield return new WaitForSeconds(1);
        //hideCheck = !hideCheck;//true
        coroutinRun = false;
        gameObject.SetActive(false);
    }


    private void Update()
    {
        if (gameObject.activeSelf && !coroutinRun)
        {
            StartCoroutine(invisible());
        }
        gameObject.transform.position = BULLET.moveing(transform.position, targetPos, BulletType, speed);
    }
}
