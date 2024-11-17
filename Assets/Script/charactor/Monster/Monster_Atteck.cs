using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract partial class Monster : charactor
{
    [Header("공격할 물체")]
    [SerializeField] GameObject MobGrenade;//투척물 프리팹
    [SerializeField] GameObject MobBullet;//일반공격 총알 프리팹
    [SerializeField] GameObject AttackArm;//공격의 시작점이 될 팔
    [SerializeField] GameObject bulletTab;//총알 저장탭
    bool NumberOn = false;
    int number;
    Vector3 targetpos;
    [Header("공격할 타켓")]
    [SerializeField] List<GameObject> playerObj;//플레이어 위치 정보

    [Header("기본 타이머")]
    //ublic int Patternt = 0f;
    float Patterntimer = 0f;
    float Patternltime = 10.0f;

    [Header("일반 공격 횟수")]
    public int AttackCount = 0;
    public int AttackMaxCount = 0;

    [Header("스킬 타이머")]
    public int ThroutCount = 0;
    public int ThroutMaxCount = 0;

    protected void MobAttackTimecheck() 
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
    protected override void nomalAttack()
    {
        targetOn();
        if (playerObj[number] == null) { return; }
        GameObject go = Instantiate(MobBullet, AttackArm.transform.position, Quaternion.identity, bulletTab.transform);
        Mob_Bullet bullet = go.GetComponent<Mob_Bullet>();
        targetNumber();
        bullet.Initialize(ActTargetTrs);
        if (AttackCount >= AttackMaxCount) 
        {
            Patterntimer = 0f;
            AttackCount = 0;
            NumberOn = false;
        }
    }
    protected void targetOn() 
    {
        int count = playerObj.Count;//공격할 플레이어 정렬
        number = Random.Range(0, count);//랜덤으로 타겟 선정
    }
    protected void GrenadeAttack() 
    {

    }
    public void targetNumber()
    {
        ActTargetTrs = playerObj[number].transform;
    }
}
