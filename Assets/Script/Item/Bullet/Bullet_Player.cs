using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public partial class Bullet_Player : MonoBehaviour
{
    Bullet BULLET = new Bullet();
    BulletType BulletType = BulletType.Playerbullet;
    public Vector3 targetPos;//공격할 목표
    //public float Speed = 0.0f;

    [Header("총알 관련 항목")]
    int targetnumber;
    int bulletDamage = 1;
    float speed = 100.0f;

    bool hideCheck = false;
    bool coroutinRun = false;
    //Vector3 targetPos;//몬스터가 공격할 목표
    RaycastHit hit;//총알이 맞출 목표
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
            yield break;  // 이미 실행 중이라면 중단

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
