using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
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
    public bool searchingOnOff = false;
    bool attackCheck = false;
    List<Vector3> searchPosObj;
    Vector3 startPos = Vector3.zero;
    
    int moveNumber = 0;
    public SearchState searching = SearchState.Move;



    //Monster_Skill SKILL = new Monster_Skill();

    //private eMobType MOBTYPE;
    //Monster Monster;
    //FSM
    //ĳ���Ϳ��� AI�� ȣ���� �ʿ�


    public override void State(ref MonsterAiState _aIState)
    {
        switch (aIState)
        {
            case MonsterAiState.Create:
                Create();
                break;
            case MonsterAiState.Search:
                Search();
                break;
            case MonsterAiState.Move:
                Move();
                break;
            case MonsterAiState.Attack:
                Attack();
                break;
            case MonsterAiState.Reset:
                Reset();
                break;
        }
        _aIState = aIState;
    }
    protected override void Create()//����
    {
        MONSTER.init();
        //animator = MONSTER.mobAnimator;

        //searchPosObj = MONSTER.movePos;//List

        //creatTab = Shared.MonsterManager.creatTab;
        //eyePos = MONSTER.eyeObj.transform;
        //startPos = MONSTER.gameObject.transform.position;
        aIState = MonsterAiState.Search;
    }

    float viewDistance = 10f;
    float viewAngle = 60f;
    public float sphereRadius = 1.0f; // �� ������
    public LayerMask playerLayer;
    protected override void Search()//������ ��� ã��
    {
        MONSTER.MovePoint();
        if (MONSTER.TargetSearch() == true)
        {
            aIState = MonsterAiState.Attack;
        }
    }
 
    //���
    protected override void Move()//�̵�
    {
        MONSTER.TargetAttackMove();
    }

    protected override void Attack()//����
    {
        MONSTER.Attack();

        aIState = MonsterAiState.Reset;
    }
    protected override void Reset()//����Ŭ ��(���� �ٽ� ���� ��� Ž��)
    {
        aIState = MonsterAiState.Search;
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

            if (distanse <= 1.0f)
            {
                moveing = false;
                aIState = MonsterAiState.Reset;
            }

        }
        else if (_enum == MonsterType.Spider)//�Ź��� ��� 
        {
            if (attackCheck == false)
            {
                GameObject go = Delivery.Instantiator(MONSTER.MobGrenade, eyePos.position, Quaternion.identity, creatTab);
                //���ҽ� ��Ȱ�� �ؾ� �ϱ� ������ �����ʿ�
                MONSTER.granaidAttack(MONSTER.gameObject.transform.position, targetPos, go);

                aIState = MonsterAiState.Reset;
                attackCheck = true;
            }
            //�߰������� ������ �ϱ� ������ AddForce�� �߰��ؾ���
            //Instantiator�� �ƴ� SetActive�� ����ؼ� ���ҽ��� ���� �ؾ���
            //aIState = AI.Reset;
        }
        else if (_enum == MonsterType.Dron)//����� ��� 
        {
            MONSTER.DirectAttack(MONSTER.gameObject, targetPos);
            animator.SetInteger(MonsterAnimParameters.Attack.ToString(), 0);
            aIState = MonsterAiState.Reset;
        }
    }




    
}
