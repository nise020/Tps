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
    Transform tagetTrs;
    Player TagetPlayer;
    public Action<bool> AttackEvent;
    bool TagetAive = false;
    SearchState searchingState = SearchState.None;
    public void DefenderState(bool isDefenderDead)//Reset
    {
        if (isDefenderDead)
        {
            TagetAive = false;
            TagetPlayer = null;
            tagetTrs = null;
            Debug.Log($"Monster = {TagetPlayer}");
        }
        if (aIState == MonsterAiState.Attack || aIState == MonsterAiState.Reset || aIState == MonsterAiState.Move)
        {
            aIState = MonsterAiState.Search;
        }

        Debug.Log($"[Dead] 타겟 초기화 완료, 상태 초기화");
    }
    public void ChangeTarget(Player _player)
    {
        TagetPlayer = _player;
    }
    public override void State()
    {
        switch (aIState)
        {
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

    protected override void Search()//공격할 대상 찾기
    {
        MONSTER.MovePoint(targetPos);

        //Debug.Log($"{MONSTER},Search");

        if(searchingState == SearchState.Wait) return;

        if (MONSTER.TargetSearch() == true)
        {
            aIState = MonsterAiState.Move;//Target On
        }
    }
 
    //행렬
    protected override void Move(Vector3 _pos)//이동
    {
        //Debug.Log($"{MONSTER},Move");

        if (searchingState == SearchState.Move) 
        {
            aIState = MonsterAiState.Search;//Re Search
            return;
        }

        if (MONSTER.TargetAttackMove(out searchingState))//목표를 놓쳤을때 로직 필요
        {
            if (searchingState == SearchState.Stop) 
            {
                aIState = MonsterAiState.Attack;
            }
        }


        float value = MONSTER.TargetDistanseCheck(_pos);

        if (PLAYER.AttackDistanseCheck(value) == true)//Move
        {
            aIState = MonsterAiState.Reset;
        }
        else
        {
            //PLAYER.AI_TargetMove(_pos, value);
        }

    }

    protected override void Attack()//공격
    {
        //Debug.Log($"{MONSTER},Attack");
        if (!TagetAive || tagetTrs == null)
        {
            aIState = MonsterAiState.Search;
            return;
        }
        MONSTER.MonsterAttack(out searchingState);//

        aIState = MonsterAiState.Reset;
    }
    protected override void Reset()//사이클 끝(보통 다시 공격 대상 탐색)
    {
        float value = MONSTER.TargetDistanseCheck(tagetTrs.position);

        if (MONSTER.AttackDistanseCheck(value) == true)//Move
        {
            aIState = MonsterAiState.Attack;
        }
        else
        {
            aIState = MonsterAiState.Move;
        }
        //searchingState = SearchState.Wait;
        //aIState = MonsterAiState.Search;
    }

    public void searchingStateUpdate(SearchState _state) 
    {
        searchingState = _state;
    }
}
