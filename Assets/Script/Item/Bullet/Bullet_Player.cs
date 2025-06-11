using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public partial class Bullet_Player : Weapon
{
    public override WeaponType WeaponType => WeaponType.Main;

    Bullet BULLET = new Bullet();
    BulletType BulletType = BulletType.Playerbullet;
    public Vector3 targetPos;//총의 방향
    public Vector3 WeaponTrs;

    [Header("총알 관련 항목")]
    int targetnumber;
    int bulletDamage = 1;
    float speedValue = 100.0f;

    bool hideCheck = false;
    bool coroutinRun = false;
    //Vector3 targetPos;//몬스터가 공격할 목표
    RaycastHit hit;//총알이 맞출 목표
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Delivery.LayerNameEnum(LayerName.Monster))
        {
            //Monster monster = new Monster();
            Monster monster = other.gameObject.GetComponentInParent<Monster>();
            Shared.BattelManager.DamageCheck(character, monster,this);
            gameObject.transform.localPosition = new Vector3();
            gameObject.SetActive(false);

        }
        else 
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
        //gameObject.transform.position = BULLET.moveing(transform.position, targetPos, BulletType, speedValue);
        gameObject.transform.position += WeaponTrs.normalized * speedValue * Time.deltaTime;
    }
}
