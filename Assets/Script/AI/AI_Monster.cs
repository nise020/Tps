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
    SearchState searchingState = SearchState.None;

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

        //Debug.Log($"{MONSTER},Search");

        if(searchingState == SearchState.Wait) return;

        if (MONSTER.TargetSearch() == true)
        {
            aIState = MonsterAiState.Move;//Target On
        }
    }
 
    //���
    protected override void Move(Vector3 _pos)//�̵�
    {
        //Debug.Log($"{MONSTER},Move");

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
        //Debug.Log($"{MONSTER},Attack");

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
