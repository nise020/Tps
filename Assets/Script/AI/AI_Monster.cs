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
    public SearchState searchingState = SearchState.None;



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
        MONSTER.Compomentinit();
        aIState = MonsterAiState.Search;
    }

    protected override void Search()//������ ��� ã��
    {
        MONSTER.MovePoint(targetPos);

        Debug.Log($"{MONSTER},Search");

        if(searchingState == SearchState.Wait) return;

        if (MONSTER.TargetSearch() == true)
        {
            aIState = MonsterAiState.Move;//Target On
        }
    }
 
    //���
    protected override void Move(Vector3 _pos)//�̵�
    {
        Debug.Log($"{MONSTER},Move");

        if (searchingState == SearchState.Move) 
        {
            aIState = MonsterAiState.Search;//Re Search
            return;
        }

        if (MONSTER.TargetAttackMove(out searchingState))//��ǥ�� �������� ���� �ʿ�
        {
            if (searchingState == SearchState.Stop) 
            {
                aIState = MonsterAiState.Attack;
            }
        }
        
    }

    protected override void Attack()//����
    {
        Debug.Log($"{MONSTER},Attack");

        MONSTER.MonsterAttack(out searchingState);//

        aIState = MonsterAiState.Reset;
    }
    protected override void Reset()//����Ŭ ��(���� �ٽ� ���� ��� Ž��)
    {
        searchingState = SearchState.Wait;
        aIState = MonsterAiState.Search;
    }

    public void searchingStateUpdate(SearchState _state) 
    {
        searchingState = _state;
    }
}
