using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract partial class Monster : Charactor
{
    [Header("������ ��ü")]
    [SerializeField] GameObject MobGrenade;//��ô�� ������
    [SerializeField] GameObject MobBullet;//�Ϲݰ��� �Ѿ� ������
    [SerializeField] GameObject AttackArm;//������ �������� �� ��
    [SerializeField] GameObject bulletTab;//�Ѿ� ������
    bool NumberOn = false;
    protected int number;
    Vector3 targetpos;
    [Header("������ Ÿ��")]
    [SerializeField] protected List<GameObject> playerObj;//�÷��̾� ��ġ ����

    [Header("�⺻ Ÿ�̸�")]
    //ublic int Patternt = 0f;
    float Patterntimer = 0f;
    float Patternltime = 10.0f;

    [Header("�Ϲ� ���� Ƚ��")]
    public int AttackCount = 0;
    public int AttackMaxCount = 0;

    [Header("��ų Ÿ�̸�")]
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
        targetOn(ref number);
        if (playerObj[number] == null) { return; }
        GameObject go = Instantiate(MobBullet, AttackArm.transform.position, Quaternion.identity, bulletTab.transform);
        Mob_Bullet bullet = go.GetComponent<Mob_Bullet>();
        chaTargetTrs = playerObj[number].transform;
        bullet.Initialize(chaTargetTrs);
        if (AttackCount >= AttackMaxCount) 
        {
            Patterntimer = 0f;
            AttackCount = 0;
            NumberOn = false;
        }
    }
    protected void targetOn(ref int _value) 
    {
        int count = playerObj.Count;//������ �÷��̾� ����
        _value = Random.Range(0, count);//�������� Ÿ�� ��ȣ ����
    }
    protected void GrenadeAttack() 
    {

    }
    //protected void targetNumber()
    //{
    //    chaTargetTrs = playerObj[number].transform;
    //}
}
