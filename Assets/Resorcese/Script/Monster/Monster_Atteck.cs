using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public partial class Monster : MonoBehaviour
{
    [Header("�⺻ Ÿ�̸�")]
    [SerializeField] GameObject MobGrenade;//��ô��
    [SerializeField] GameObject MobBullet;//��ô��
    [SerializeField] GameObject AttackArm;//������ ����� ��
    [SerializeField] GameObject bulletTab;//�Ѿ� ������
    bool NumberOn = false;
    int number;
    [Header("�⺻ Ÿ�̸�")]
    float Patterntimer = 0f;
    float Patternltime = 10.0f;

    [Header("���� Ÿ�̸�")]
    float Attecktimer = 0f;
    float Attecktime = 3.0f;
    int NomalSpeed = 3;

    [Header("��ų Ÿ�̸�")]
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
            Attecktimer += Time.deltaTime;//���� ���ð�
            if(Attecktimer >= Attecktime) 
            {
                Attecktimer = 0f; 
            }  

        }
        else if (number == 1) 
        {
            Throutimer += Time.deltaTime;//��ų ���簣
            if(Throutimer >= Throutime) 
            {
                Attecktimer = 0f; 
            }  

        }
    }
    public void nomalAttack() 
    {
        //GameObject targrt = shared.BattelMgr.player;
        int count = shared.BattelMgr.player.Length;//������ �÷��̾� ����
        int targetNumber = Random.Range(0, count);//�������� Ÿ�� ����
        GameObject go = Instantiate(MobBullet, AttackArm.transform.position, Quaternion.identity, bulletTab.transform);
        Vector3 Dir = (transform.position - shared.BattelMgr.player[targetNumber].transform.position).normalized;
        go.transform.position = Dir * NomalSpeed * Time.deltaTime;
    }
    public void GrenadeAttack() 
    {

    }
}
