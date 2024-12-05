using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public partial class AiMonster : AiBase
{
    private Monster MONSTER;
    private eMobType MOBTYPE;
    //Monster Monster;
    //FSM
    //ĳ���Ϳ��� AI�� ȣ���� �ʿ�

    public AiMonster(Monster _monster, eMobType _eNum)
    {
        this.MONSTER = _monster;
        this.MOBTYPE = _eNum;
    }
    protected override void Type()
    {
        //Monster
        //switch (MobType)
        //{
        //    case eMobType.Defolt:
        //        // Defolt = GetComponent<DefoltMob>();//class
        //        break;
        //    case eMobType.Flying:
        //        // Flying = GetComponent<FlyingMob>();//class
        //        break;
        //    case eMobType.Huge:
        //        // Huge = GetComponent<HugeMob>();//class
        //        break;
        //}
    }

    public void Pattern()
    {

    }
    public override void state()
    {
        switch (aIState)
        {
            case eAI.Create:
                Create();
                break;
            case eAI.Search:
                Search();
                break;
            case eAI.Move:
                Move();
                break;
            case eAI.Attack:
                Attack();
                break;
            case eAI.Reset:
                Reset();
                break;
        }
    }
    protected override void Create()//����
    {
        Type();
        //init(Monster);
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
            //base.Move();
            base.Attack();
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
                Flying.DirectAttackSkill();
                break;
            case eMobType.Huge:
                Huge.jumpSkill();
                break;
        }
        if (nextPatternOn == true)
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
