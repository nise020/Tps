using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public partial class AiMonster : AiBase
{
    //Monster_Skill SKILL;
    bool attackOn = true;
    int targetNumber;
    //Monster_Skill SKILL = new Monster_Skill();
  
    //private eMobType MOBTYPE;
    //Monster Monster;
    //FSM
    //ĳ���Ϳ��� AI�� ȣ���� �ʿ�


    public void Pattern()
    {
        switch (MobType)
        {
            case eMobType.Defolt://�Ϲ� ��Ÿ(Ƚ������)
                SKILL.NomalAttack(targetNumber, MONSTER.MobBullet, MONSTER.soljerObj, MONSTER.AttackArm.transform.position,
                    MONSTER.creatTabObj);
                break;
            case eMobType.Flying://���� ����
                MONSTER.gameObject.transform.position = SKILL.DirectAttackSkill
                    (targetNumber, MONSTER.soljerObj, MONSTER.gameObject.transform.position);
                break;
            case eMobType.Huge://���� ����
                SKILL.JumpSkill(targetNumber, MONSTER.soljerObj, ref attackOn, 
                    MONSTER.gameObject.transform.position, MONSTER.mobRigid);
                break;
        }
    }

    public override void State(ref eAI _aIState)
    {
        switch (_aIState)
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
        _aIState = aIState;
    }
    protected override void Create()//����
    {
        //SKILL = new Monster_Skill();
        aIState = eAI.Search;
    }
    protected override void Search()//������ ��� ã��
    {

        //�ʿ��� ���
        //1.Ÿ�̸�
        //2.Ÿ�̸� ����� ������ Ÿ�� ��ǥ Ȯ��(���� ��ġ �ٲ㵵 ���󰡴� ����� �ƴ�)
        if (MONSTER == null)
        {
            Debug.LogError("MONSTER�� null�Դϴ�.");
            return;
        }

        if (SKILL == null)
        {
            Debug.LogError("SKILL�� null�Դϴ�.");
            return;
        }

        SKILL.targetOn(ref targetNumber,MONSTER.soljerObj);
        aIState = eAI.Attack;
    }
    protected override void Attack()//����
    {
        Pattern();
        if (nextPatternOn == true)
        {
            nextPatternOn = false;
            aIState = eAI.Reset;
        }
    }
    protected override void Reset()//����Ŭ ��(���� �ٽ� ���� ��� Ž��)
    {
        attackOn = true;
        targetNumber = 0;
        aIState = eAI.Search;
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


}
