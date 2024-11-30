using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract partial class Monster : Charactor
{
    [Header("������ ��ü(����)")]
    [SerializeField] GameObject MobGrenade;//��ô�� ������
    [SerializeField] protected GameObject MobBullet;//�Ϲݰ��� �Ѿ� ������
    [SerializeField] protected GameObject AttackArm;//������ �������� �� ��
    [SerializeField] protected GameObject bulletTab;//�Ѿ� ������
    protected bool NumberOn = false;
    protected int number;
    Vector3 targetpos;
    [Header("������ Ÿ��(����)")]
    [SerializeField] protected List<GameObject> playerObj;//�÷��̾� ��ġ ����
    [SerializeField] protected List<GameObject> coverObj;//�÷��̾� ��ġ ����

    [Header("�⺻ Ÿ�̸�")]
    //ublic int Patternt = 0f;
    protected float Patterntimer = 0f;
    protected float Patternltime = 10.0f;

    [Header("�Ϲ� ���� Ƚ��(DefoltMob)")]
    protected int AttackCount = 0;
    protected int AttackMaxCount = 6;

    [Header("��ų Ÿ�̸�")]
    protected int ThroutCount = 0;
    protected int ThroutMaxCount = 3;

    //[Header("AI ���Ͽ�")]
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
        int count = playerObj.Count;//������ �÷��̾� ����
        _value = Random.Range(0, count);//�������� Ÿ�� ��ȣ ����
    }
    
}
