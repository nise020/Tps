using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public abstract partial class Monster : Actor
{
    [Header("공격할 물체")]
    [SerializeField] GameObject MobGrenade;//투척물
    [SerializeField] GameObject MobBullet;//투척물
    [SerializeField] GameObject AttackArm;//공격을 사용할 팔
    [SerializeField] GameObject bulletTab;//총알 저장탭
    bool NumberOn = false;
    int number;
    Vector3 targetpos;
    [Header("공격할 타켓")]
    [SerializeField] List<GameObject> playerObj;

    [Header("기본 타이머")]
    public float Patterntimer = 0f;
    float Patternltime = 10.0f;

    [Header("공격 타이머")]
    public float Attecktimer = 0f;
    float Attecktime = 3.0f;
    int NomalSpeed = 3;

    [Header("스킬 타이머")]
    public float Throutimer = 0f;
    float Throutime = 5.0f;
    int ThrouSpeed = 3;

    IEnumerator MobAttackTimecheck() 
    {
        Patterntimer += Time.deltaTime;
        int number = 0;
        if (Patterntimer >  Patternltime) 
        {
            if (NumberOn == false)
            {
                number = Random.Range(0, 1);
                NumberOn = true;
            }
        }
        MobAttackTimer(number);
        Debug.Log($"{Patterntimer}");
        yield return null;
    }
    void MobAttackTimer(int number) 
    {
        if (number == 0) 
        {
            Attecktimer += Time.deltaTime;//공격 대기시간
            if (Attecktimer > Attecktime) 
            {
                nomalAttack();
            }
            Debug.Log($"{Attecktimer}");
        }
        else if (number == 1) 
        {
            Throutimer += Time.deltaTime;//스킬 대기사간
            if (Throutimer > Throutime)
            {
                GrenadeAttack();
            }   
            Debug.Log($"{Throutimer}");
        }
        //yield return null;
    }
    public void nomalAttack()//횟수 추가
    {
        int count = playerObj.Count;//공격할 플레이어 정렬
        number = Random.Range(0, count);//랜덤으로 타겟 선정
        if (playerObj[number] == null) { return; }
        GameObject go = Instantiate(MobBullet, AttackArm.transform.position, Quaternion.identity, bulletTab.transform);
        Mob_Bullet bullet = go.GetComponent<Mob_Bullet>();
        targetNumber();
        bullet.Initialize(ActTargetTrs);

        Patterntimer = 0f;
        Attecktimer = 0f;
        NumberOn = false;
    }
    public void targetNumber()
    {
        ActTargetTrs = playerObj[number].transform;
    }
    public void GrenadeAttack() 
    {

    }
}
