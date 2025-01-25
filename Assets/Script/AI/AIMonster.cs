using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.XR;

public partial class AiMonster : AiBase
{
    List<Player> target;
    Transform creatTab;
    bool attackOn = true;
    int targetNumber;//������ ��ǥ�� ��ȣ
    float timer = 0.0f;
    float time = 5.0f;
    MobAnim mobAnim = MobAnim.Null;
    Transform eyePos;
    Vector3 targetPos;
    Animator animator;
    string moveAnim = ($"{MobAnim.Move}");
    string attackAnim = ($"{MobAnim.Attack}");
    string serchAnim = ($"{MobAnim.Serch}");
    string dilrayAnim = ($"{MobAnim.AttackDilray}");

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
        eyePos = MONSTER.eyeObj.transform;
        animator = MONSTER.Mobanimator;
        aIState = eAI.Search;
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
        animator.SetInteger(serchAnim,1);

        if (Physics.Raycast(eyePos.position,
           eyePos.transform.forward, out RaycastHit hit))
        {
            string text1 = ($"{LayerTag.Player}");//enum
            string text2 = ($"{LayerTag.Cover}");
            int layer = hit.collider.gameObject.layer;
            string name = LayerMask.LayerToName(layer);

            if (name == text1)
            {
                targetPos = hit.point;
                animator.SetInteger(serchAnim, 0);
                aIState = eAI.Move;
            }
            else if (name == text2)
            {
                return;
            }
        }
    }
    protected override void Move()//�̵�
    {
        animator.SetInteger(moveAnim, 1);
        Vector3 myPos = MONSTER.gameObject.transform.position;
        float distanse = Vector3.Distance(myPos, targetPos);
        float targetvalue = MONSTER.attackDistanse;
        if (distanse < targetvalue) 
        {
            aIState = eAI.Attack;
        }
    }

    protected override void Attack()//����
    {
        animator.SetInteger(attackAnim, 1);
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
    


}
