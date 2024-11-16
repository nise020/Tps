using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public abstract partial class Monster : Actor
{
    [Header("������ ��ü")]
    [SerializeField] GameObject MobGrenade;//��ô��
    [SerializeField] GameObject MobBullet;//��ô��
    [SerializeField] GameObject AttackArm;//������ ����� ��
    [SerializeField] GameObject bulletTab;//�Ѿ� ������
    bool NumberOn = false;
    int number;
    Vector3 targetpos;
    [Header("������ Ÿ��")]
    [SerializeField] List<GameObject> playerObj;

    [Header("�⺻ Ÿ�̸�")]
    public float Patterntimer = 0f;
    float Patternltime = 10.0f;

    [Header("���� Ÿ�̸�")]
    public float Attecktimer = 0f;
    float Attecktime = 3.0f;
    int NomalSpeed = 3;

    [Header("��ų Ÿ�̸�")]
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
            Attecktimer += Time.deltaTime;//���� ���ð�
            if (Attecktimer > Attecktime) 
            {
                nomalAttack();
            }
            Debug.Log($"{Attecktimer}");
        }
        else if (number == 1) 
        {
            Throutimer += Time.deltaTime;//��ų ���簣
            if (Throutimer > Throutime)
            {
                GrenadeAttack();
            }   
            Debug.Log($"{Throutimer}");
        }
        //yield return null;
    }
    public void nomalAttack()//Ƚ�� �߰�
    {
        int count = playerObj.Count;//������ �÷��̾� ����
        number = Random.Range(0, count);//�������� Ÿ�� ����
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
