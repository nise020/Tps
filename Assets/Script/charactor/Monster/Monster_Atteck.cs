using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract partial class Monster : Charactor
{
    [Header("공격할 물체(공통)")]
    [SerializeField] GameObject MobGrenade;//투척물 프리팹
    [SerializeField] protected GameObject MobBullet;//일반공격 총알 프리팹
    [SerializeField] protected GameObject AttackArm;//공격의 시작점이 될 팔
    [SerializeField] protected GameObject bulletTab;//총알 저장탭
    protected bool NumberOn = false;
    protected int number;
    Vector3 targetpos;
    [Header("공격할 타켓(공통)")]
    [SerializeField] protected List<GameObject> playerObj;//플레이어 위치 정보
    [SerializeField] protected List<GameObject> coverObj;//플레이어 위치 정보

    [Header("기본 타이머")]
    //ublic int Patternt = 0f;
    protected float Patterntimer = 0f;
    protected float Patternltime = 10.0f;

    [Header("일반 공격 횟수(DefoltMob)")]
    protected int AttackCount = 0;
    protected int AttackMaxCount = 6;

    [Header("스킬 타이머")]
    protected int ThroutCount = 0;
    protected int ThroutMaxCount = 3;

    //[Header("AI 패턴용")]
    //public bool aiNextPattern = false;

    protected virtual void MobAttackTimecheck() 
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
            MobAttackTimer(number);
        }
        
    }
    protected void MobAttackTimer(int number) 
    {
        if (number == 0) 
        {
            nomalAttack();
        }
        else if (number == 1) 
        {
            GrenadeAttack();
        }
        //yield return null;
    }
    protected virtual void nomalAttack()
    {
        targetOn(ref number);
        if (playerObj[number] == null) { return; }
        GameObject go = Instantiate(MobBullet, AttackArm.transform.position, Quaternion.identity, bulletTab.transform);
        Mob_Bullet bullet = go.GetComponent<Mob_Bullet>();
        chaTargetTrs = playerObj[number].transform;
        bullet.Initialize(chaTargetPos);//
        AttackCount++;
        if (AttackCount >= AttackMaxCount) 
        {
            Patterntimer = 0f;
            AttackCount = 0;
            NumberOn = false;
            //aiNextPattern = true;
        }
    }
    protected virtual void GrenadeAttack() 
    {
        targetOn(ref number);
        if (playerObj[number] == null) { return; }
        GameObject go = Instantiate(MobGrenade, AttackArm.transform.position, Quaternion.identity, bulletTab.transform);
        Mob_Bullet bullet = go.GetComponent<Mob_Bullet>();
        chaTargetTrs = playerObj[number].transform;
        bullet.Initialize(chaTargetPos);//
        ThroutCount++;
        if (ThroutCount >= ThroutMaxCount)
        {
            Patterntimer = 0f;
            ThroutCount = 0;
            NumberOn = false;
            //aiNextPattern = true;
        }
    }
    protected virtual void targetOn(ref int _value) 
    {
        int count = playerObj.Count;//공격할 플레이어 정렬
        _value = Random.Range(0, count);//랜덤으로 타겟 번호 선정
    }
    
}
