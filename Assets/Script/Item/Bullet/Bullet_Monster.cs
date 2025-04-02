using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Bullet_Monster : MonoBehaviour
{
    Bullet BULLET = new Bullet();
    bulletType BulletType = bulletType.Mobbullet;
    public Vector3 targetPos;//공격할 목표

    [Header("총알 관련 항목")]
    int bulletdamage = 1;
    float speed = 10.0f;
    bool hideCheck = false;
    bool coroutinRun = false;
    //RaycastHit hit;//총알이 맞출 목표

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
            yield break;  // 이미 실행 중이라면 중단

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
