using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : MonoBehaviour
{
    [SerializeField] GameObject MobGrenade;//투척물
    [SerializeField] GameObject AttackArm;//공격을 사용할 팔
    bool NumberOn = false;
    int number;
    [Header("기본 타이머")]
    float Nomaltimer = 0f;
    float Nomaltime = 10.0f;

    [Header("공격 타이머")]
    float Attecktimer = 0f;
    float Attecktime = 3.0f;

    [Header("스킬 타이머")]
    float Throutimer = 0f;
    float Throutime = 5.0f;
    public void MobAttack(GameObject target) 
    {
        Nomaltimer += Time.deltaTime;
        if (Nomaltimer >= Nomaltime) 
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
            Attecktimer += Time.deltaTime;
        }
        else if (number == 1) 
        {
            Throutimer += Time.deltaTime;
        }
    }
}
