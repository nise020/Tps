using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public partial class AiMonster : AiBase
{
    List<Player> target;
    Transform creatTab;
    bool attackOn = true;
    int targetNumber;//������ ��ǥ�� ��ȣ
    float timer = 0.0f;
    float time = 5.0f;
    //MobAnim mobAnimState = MobAnim.Idle;
    Transform eyePos;
    Vector3 targetPos;
    Animator animator;
    bool searchAnim = false;
    bool moveAnim = false;
    public bool moveing = false;
    public bool searching = false;
    bool moveChack = false;
    List<GameObject> searchPosObj;
    Vector3 startPos = Vector3.zero;
    Vector3 myPos = Vector3.zero;
    int moveNumber = 0;
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
            case eMobType.Sphere:
                SKILL.JumpSkill(targetNumber, target, ref attackOn,
                    MONSTER.gameObject.transform.position, MONSTER.mobRigid);
                break;
        }
    }

    public override void State(ref eAI _aIState)
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
        _aIState = aIState;
    }
    protected override void Create()//����
    {
        animator = MONSTER.mobAnimator;

        searchPosObj = MONSTER.movePosObj;//List

        creatTab = Shared.BattelMgr.creatTab;
        eyePos = MONSTER.eyeObj.transform;
        startPos = MONSTER.gameObject.transform.position;
        aIState = eAI.Search;
    }

    protected override void Search()//������ ��� ã��
    {
        if (searchAnim == false)
        {
            searchAnim = true;
            animator.SetInteger("Search", 1);
        }
        if (Physics.Raycast(eyePos.position,
           eyePos.transform.forward, out RaycastHit hit))//target Serching
                                                         //�÷��̾ �ɷ�����
        {
            int layer = hit.collider.gameObject.layer;
            string name = LayerMask.LayerToName(layer);

            if (name == "Player")
            {
                moveAnim = false;
                targetPos = hit.point;//Vector3
                aIState = eAI.Attack;//next Pattern

                searchAnim = false;//clear

            }
        }
        MONSTER.readySearch(ref searching);
    }

    protected override void Move()//�̵�
    {
        Debug.Log($"Move");
    }

    protected override void Attack()//����
    {
        animator.SetInteger("Attack", 1);
        Debug.Log($"Attack");
        //Pattern();

        if (moveAnim == false)
        {
            moveAnim = true;
            animator.SetInteger("Search", 0);// Serching X
            animator.SetInteger("Walk", 0);
            animator.SetInteger("Close", 1);
        }
        if (moveing == true)
        {
            Vector3 myPos = MONSTER.gameObject.transform.position;
            float speed = MONSTER.moveSpeed;
            MONSTER.gameObject.transform.position += (targetPos - myPos).normalized * speed * Time.deltaTime;

            float distanse = Vector3.Distance(myPos, targetPos);
            float targetvalue = MONSTER.attackDistanse;//�����Ÿ�
            if (distanse < targetvalue)
            {
                animator.SetInteger("Close", 0);
                animator.SetInteger("AttackDilray", 1);
                aIState = eAI.Reset;
            }
        }

        //if (nextOn_Off == true)
        //{
        //    animator.SetInteger("AttackDilray", 1);
        //    nextOn_Off = false;
        //    aIState = eAI.Reset;
        //}
    }
    protected override void Reset()//����Ŭ ��(���� �ٽ� ���� ��� Ž��)
    {
        Debug.Log($"Reset");
        attackOn = true;
        targetNumber = 0;
        aIState = eAI.Search;
    }


}
