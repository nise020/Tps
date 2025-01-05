using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public partial class AiMonster : AiBase
{
    List<Player> target;
    Transform creatTab;
    bool attackOn = true;
    int targetNumber;//������ ��ǥ�� ��ȣ
    float timer = 0.0f;
    float time = 5.0f;
    //Monster_Skill SKILL = new Monster_Skill();
  
    //private eMobType MOBTYPE;
    //Monster Monster;
    //FSM
    //ĳ���Ϳ��� AI�� ȣ���� �ʿ�


    public void Pattern()
    {
        switch (MobType)
        {
            case eMobType.Defolt://�Ϲ� ��Ÿ (Ƚ������)
                SKILL.NomalAttack(ref nextOn_Off, targetNumber, MONSTER.MobBullet, target, MONSTER.AttackArm.transform.position, creatTab);
                break;
            case eMobType.Flying://���� ����
                MONSTER.gameObject.transform.position = SKILL.DirectAttackSkill
                    (targetNumber, target, MONSTER.gameObject.transform.position);
                break;
            case eMobType.Huge://���� ����
                SKILL.JumpSkill(targetNumber, target, ref attackOn, 
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
        //target = Shared.BattelMgr.PLAYER;
        creatTab = Shared.BattelMgr.creatTab;
        aIState = eAI.Move;
    }
    private void searchTimer()
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            timer = 0.0f;
            aIState = eAI.Search;
        }
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

        SKILL.targetOn(ref targetNumber, target);
        if (target[targetNumber] == null)
        {
            return;
        }
        else { aIState = eAI.Attack; }
    }
    protected override void Attack()//����
    {
        Pattern();
        if (nextOn_Off == true)
        {
            nextOn_Off = false;
            aIState = eAI.Reset;
        }
    }
    protected override void Reset()//����Ŭ ��(���� �ٽ� ���� ��� Ž��)
    {
        attackOn = true;
        targetNumber = 0;
        aIState = eAI.Create;
    }
    protected override void Move()//�̵�
    {
        
        if (nextOn_Off == true)
        {
            nextOn_Off = false;
            base.Attack();
        }
    }


}
