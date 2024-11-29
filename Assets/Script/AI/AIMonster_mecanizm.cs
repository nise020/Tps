using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AIMonster : AiBase
{
    protected override void Type() 
    {
        switch (MobType)
        {
            case eMobType.Defolt:
                Defolt = GetComponent<DefoltMob>();//class
                break;
            case eMobType.Flying:
                Flying = GetComponent<FlyingMob>();//class
                break;
            case eMobType.Huge:
                Huge = GetComponent<HugeMob>();//class
                break;
        }
    }

    public void pattern() 
    {

    }
    protected override void Create()//����
    {
        Type();
        if (nextPatternOn == true)
        {
            nextPatternOn = false;
            base.Search();
        }
    }
    protected override void Search()//������ ��� ã��
    {
        //Ÿ�� ã�� �߰�
        if (nextPatternOn == true)
        {
            nextPatternOn = false;
            base.Move();
        }
    }
    protected override void Move()//�̵�
    {
        switch (MobType)
        {
            case eMobType.Defolt:
                //Defolt.nomalAttack();
                break;
        }
        if (nextPatternOn == true)
        {
            nextPatternOn = false;
            base.Attack();
        }
    }
    protected override void Attack()//����
    {
        switch (MobType)
        {
            case eMobType.Defolt:
                Defolt.attack();
                break;
            case eMobType.Flying:
                Flying.DirectAttack();
                break;
            case eMobType.Huge:
                Huge.jumpAttack();
                break;
        }
        if (nextPatternOn==true) 
        {
            nextPatternOn = false;
            base.Reset();
        }
        
        
    }
    protected override void Reset()//����Ŭ ��(���� �ٽ� ���� ��� Ž��)
    {
        if (nextPatternOn == true)
        {
            nextPatternOn = false;
            base.Move();
        }
    }
}
