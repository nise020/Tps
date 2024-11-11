using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : MonoBehaviour
{
    [SerializeField] GameObject MobGrenade;//��ô��
    [SerializeField] GameObject AttackArm;//������ ����� ��
    bool NumberOn = false;
    int number;
    [Header("�⺻ Ÿ�̸�")]
    float Nomaltimer = 0f;
    float Nomaltime = 10.0f;

    [Header("���� Ÿ�̸�")]
    float Attecktimer = 0f;
    float Attecktime = 3.0f;

    [Header("��ų Ÿ�̸�")]
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
