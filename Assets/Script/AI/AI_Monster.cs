using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public partial class AiMonster : AiBase
{
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
    bool attackCheck = false;
    List<Vector3> searchPosObj;
    Vector3 startPos = Vector3.zero;
    
    int moveNumber = 0;
    //Monster_Skill SKILL = new Monster_Skill();

    //private eMobType MOBTYPE;
    //Monster Monster;
    //FSM
    //ĳ���Ϳ��� AI�� ȣ���� �ʿ�

    
    public override void State(ref AI _aIState)
    {
        switch (aIState)
        {
            case AI.Create:
                Create();
                break;
            case AI.Search:
                Search();
                break;
            case AI.Move:
                Move();
                break;
            case AI.Attack:
                Attack();
                break;
            case AI.Reset:
                Reset();
                break;
        }
        _aIState = aIState;
    }
    protected override void Create()//����
    {
        animator = MONSTER.mobAnimator;

        searchPosObj = MONSTER.movePos;//List

        creatTab = Shared.BattelMgr.creatTab;
        eyePos = MONSTER.eyeObj.transform;
        startPos = MONSTER.gameObject.transform.position;
        aIState = AI.Search;
    }

    float viewDistance = 5f;
    float viewAngle = 60f;   

    protected override void Search()//������ ��� ã��
    {
        if (searchAnim == false)
        {
            searchAnim = true;
            animator.SetInteger("Search", 1);
        }

        MONSTER.readySearch(ref searching);//��ġ
        //Debug.Log($"Search");
        Collider[] hitColliders = Physics.OverlapSphere(MONSTER.gameObject.transform.position, viewDistance);

        if (hitColliders == null) { return; }

        foreach (Collider col in hitColliders)
        {
            if (col.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Player))
            {
                Vector3 directionToTarget = (col.transform.position - MONSTER.gameObject.transform.position).normalized;
                float angleBetween = Vector3.Angle(MONSTER.gameObject.transform.forward, directionToTarget);

                if (angleBetween < viewAngle * 0.5f) // �þ߰� ���� �ִ��� Ȯ��
                {
                    moveAnim = false;
                    targetPos = col.transform.position;//Vector3
                    aIState = AI.Attack;

                    searchAnim = false;//clear
                }
            }
        }

    }
    //Vector3

    //DIstance: �Ÿ� ���ϱ�
    //Dot ����: ����, ��ġ��
    //Cross ����: �� �ݻ�
    //Normalize ����ȭ: ���⸸ ���ϰ� ���� 1�� �����

    //���
    protected override void Move()//�̵�
    {
        //MONSTER.readySearch(ref searching);
    }

    protected override void Attack()//����
    {
        animator.SetInteger("Attack", 1);
        Debug.Log($"Attack");
        //Pattern();

        if (moveAnim == false)//Animation Event
        {
            moveAnim = true;
            animator.SetInteger("Search", 0);// Serching X
            animator.SetInteger("Walk", 0);
            if (MobType == MonsterType.Sphere) 
            {
                animator.SetInteger("Close", 1);
            }
        }

        Pattern(MobType);

    }
    public void Pattern(MonsterType _enum)
    {
        if (_enum == MonsterType.Sphere)//��ü �� ���
        {
            if (moveing == false) return;
            if (moveing == true)//Animation Event
            {
                MONSTER.DirectAttack(MONSTER.gameObject, targetPos);
            }
            //��� �̵��ϴ� ���� ����
            Vector3 myPos = MONSTER.gameObject.transform.position;
            float distanse = Vector3.Distance(myPos, targetPos);
            float targetvalue = MONSTER.attackDistanse;//�����Ÿ�

            if (distanse < 1.0f)
            {
                animator.SetInteger("Close", 0);
                animator.SetInteger("AttackDilray", 1);
                moveing = false;
                aIState = AI.Reset;
            }

        }
        else if (_enum == MonsterType.Spider)//�Ź��� ��� 
        {
            if (attackCheck == false)
            {
                GameObject go = Delivery.Instantiator(MONSTER.MobGrenade, eyePos.position, Quaternion.identity, creatTab);
                //���ҽ� ��Ȱ��
                MONSTER.granaidAttack(MONSTER.gameObject.transform.position, targetPos, go);
                animator.SetInteger("Attack", 0);
                attackCheck = true;
            }


            //�߰������� ������ �ϱ� ������ AddForce�� �߰��ؾ���
            //Instantiator�� �ƴ� SetActive�� ����ؼ� ���ҽ��� ���� �ؾ���
            //aIState = AI.Reset;
        }
        else if (_enum == MonsterType.Dron)//����� ��� 
        {
            MONSTER.DirectAttack(MONSTER.gameObject, targetPos);
            animator.SetInteger("Attack", 0);
            aIState = AI.Reset;
        }
    }



    protected override void Reset()//����Ŭ ��(���� �ٽ� ���� ��� Ž��)
    {
        Debug.Log($"Reset");
        moveing = false;
        attackOn = true;
        targetNumber = 0;
        aIState = AI.Search;

    }

    
}
