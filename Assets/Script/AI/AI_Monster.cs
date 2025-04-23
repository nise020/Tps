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
    public override void State()
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
                Move(targetPos);
                break;
            case MonsterAiState.Attack:
                Attack();
                break;
            case MonsterAiState.Reset:
                Reset();
                break;
        }
    }
    protected override void Create()//����
    {
        MONSTER.init();
        aIState = MonsterAiState.Search;
    }

    //float viewDistance = 10f;
    //float viewAngle = 60f;
    //public float sphereRadius = 1.0f; // �� ������
    //public LayerMask playerLayer;
    protected override void Search()//������ ��� ã��
    {
        MONSTER.MovePoint(targetPos);
        Debug.Log($"Search");
        if (MONSTER.TargetSearch() == true)
        {
            aIState = MonsterAiState.Move;
        }
    }
 
    //���
    protected override void Move(Vector3 _pos)//�̵�
    {
        Debug.Log($"Move");
        if (MONSTER.TargetAttackMove()) 
        {
            aIState = MonsterAiState.Attack;
        }
    }

    protected override void Attack()//����
    {
        MONSTER.MonsterAttack();

        //aIState = MonsterAiState.Reset;
    }
    protected override void Reset()//����Ŭ ��(���� �ٽ� ���� ��� Ž��)
    {
        aIState = MonsterAiState.Search;
    }
    
}
