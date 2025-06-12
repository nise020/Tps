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

    float range = 10.0f;
    private void Awake()
    {
        ItemStateData.WeaponType = WeaponclassType.Bullet;
        Power = 100;
    }
    private void OnTriggerEnter(Collider other)
    {
        Character character = other.gameObject.GetComponentInParent<Character>();
        if (character == null)
        {
            gameObject.transform.localPosition = new Vector3();
            gameObject.SetActive(false);
        }
        else 
        {
            for (int i = 0; i < targetToAttackList.Count; i++)
            {
                if (character.gameObject == targetToAttackList[i].gameObject)
                {
                    Shared.BattelManager.DamageCheck(CHARACTER, character, this);
                    gameObject.transform.localPosition = new Vector3();
                    gameObject.SetActive(false);
                    return;
                }
            }
        }

        //if (other.gameObject.layer == Delivery.LayerNameEnum(LayerName.Monster))
        //{
        //    //Monster monster = new Monster();
        //    Monster monster = other.gameObject.GetComponentInParent<Monster>();
        //    Shared.BattelManager.DamageCheck(CHARACTER, monster,this);
        //    gameObject.transform.localPosition = new Vector3();
        //    gameObject.SetActive(false);

        //}
        //else 
        //{
        //    gameObject.transform.localPosition = new Vector3();
        //    gameObject.SetActive(false);
        //}
    }
    public IEnumerator MoveBullet(Transform bullet, Vector3 direction, float speed, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            bullet.position += direction * speed * Time.deltaTime;
            time += Time.deltaTime;
            yield return null;
        }

        bullet.gameObject.SetActive(false);
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


    //private void Update()
    //{
    //    if (gameObject.activeSelf && !coroutinRun)
    //    {
    //        StartCoroutine(invisible());
    //    }
    //    //gameObject.transform.position = BULLET.moveing(transform.position, targetPos, BulletType, speedValue);
    //    gameObject.transform.position += WeaponTrs.normalized * speedValue * Time.deltaTime;
    //}
}
