using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public partial class Monster : MonoBehaviour
{
    [Header("기본 타이머")]
    [SerializeField] GameObject MobGrenade;//투척물
    [SerializeField] GameObject MobBullet;//투척물
    [SerializeField] GameObject AttackArm;//공격을 사용할 팔
    [SerializeField] GameObject bulletTab;//총알 저장탭
    bool NumberOn = false;
    int number;
    [Header("기본 타이머")]
    float Patterntimer = 0f;
    float Patternltime = 10.0f;

    [Header("공격 타이머")]
    float Attecktimer = 0f;
    float Attecktime = 3.0f;
    int NomalSpeed = 3;

    [Header("스킬 타이머")]
    float Throutimer = 0f;
    float Throutime = 5.0f;
    int ThrouSpeed = 3;

    public void MobAttackTimecheck() 
    {
        Patterntimer += Time.deltaTime;
        if (Patterntimer >= Patternltime) 
        {
            if(NumberOn == false) 
            {
                number = Random.Range(0, 1);
            }
            MobAttackTimer(number);
        }
    }
    public void MobAttackTimer(int number) 
    {
        if (number == 0) 
        {
            Attecktimer += Time.deltaTime;//공격 대기시간
            if(Attecktimer >= Attecktime) 
            {
                Attecktimer = 0f; 
            }  

        }
        else if (number == 1) 
        {
            Throutimer += Time.deltaTime;//스킬 대기사간
            if(Throutimer >= Throutime) 
            {
                Attecktimer = 0f; 
            }  

        }
    }
    public void nomalAttack() 
    {
        //GameObject targrt = shared.BattelMgr.player;
        int count = shared.BattelMgr.player.Length;//공격할 플레이어 정렬
        int targetNumber = Random.Range(0, count);//랜덤으로 타겟 선정
        GameObject go = Instantiate(MobBullet, AttackArm.transform.position, Quaternion.identity, bulletTab.transform);
        Vector3 Dir = (transform.position - shared.BattelMgr.player[targetNumber].transform.position).normalized;
        go.transform.position = Dir * NomalSpeed * Time.deltaTime;
    }
    public void GrenadeAttack() 
    {

    }
}
