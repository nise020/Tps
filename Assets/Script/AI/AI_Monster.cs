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
    //SearchState searchingState = SearchState.None;
    public void TargetStatUpdate(bool isDefenderDead)//Reset
    {
        if (isDefenderDead)
        {
            TagetAive = false;
            TagetPlayer = null;
            tagetTrs = null;
            Debug.Log($"Monster = {TagetPlayer}");
        }
        if (aIState == AiState.Attack || aIState == AiState.Reset || aIState == AiState.Move)
        {
            aIState = AiState.Search;
        }

        Debug.Log($"[Dead] 타겟 초기화 완료, 상태 초기화");
    }
    public void ChangeTarget(Player _player)
    {
        TagetPlayer = _player;
    }
    public override void State()
    {
        if (!TagetAive && aIState != AiState.Search)
        {
            aIState = AiState.Search;
        }

        switch (aIState)
        {
            case AiState.Search:
                Search();
                break;
            case AiState.Move:
                Move(targetPos);
                break;
            case AiState.Attack:
                Attack();
                break;
            case AiState.Reset:
                Reset();
                break;
        }
    }

    protected override void Search()//공격할 대상 찾기
    {
        MONSTER.MovePoint();

        //Debug.Log($"{MONSTER},Search");

        //if(searchingState == SearchState.Wait) return;

        if (MONSTER.TargetSearch(out TagetPlayer) == true)
        {
            tagetTrs = TagetPlayer.BodyObjectLoad();
            TagetAive = true;
            aIState = AiState.Move;//Target On
        }
    }
 
    //행렬
    protected override void Move(Vector3 _pos)//이동
    {
        //Debug.Log($"{MONSTER},Move");

        //if (searchingState == SearchState.Move) 
        //{
        //    aIState = MonsterAiState.Search;//Re Search
        //    return;
        //}

        //if (MONSTER.TargetAttackMove(out searchingState))//목표를 놓쳤을때 로직 필요
        //{
        //    if (searchingState == SearchState.Stop) 
        //    {
        //        aIState = MonsterAiState.Attack;
        //    }
        //}


        float value = MONSTER.TargetDistanseCheck(_pos);

        if (MONSTER.AttackDistanseCheck(value) == true)//Move
        {
            aIState = AiState.Attack;
        }
        else
        {
            MONSTER.Ai_TargetMove(_pos, value);
            //PLAYER.AI_TargetMove(_pos, value);
        }

    }

    protected override void Attack()//공격
    {
        //Debug.Log($"{MONSTER},Attack");
        if (!TagetAive || tagetTrs == null)
        {
            aIState = AiState.Search;
            return;
        }
        MONSTER.Ai_Attack(tagetTrs);//

        aIState = AiState.Reset;
    }
    protected override void Reset()//사이클 끝(보통 다시 공격 대상 탐색)
    {
        float value = MONSTER.TargetDistanseCheck(tagetTrs.position);

        if (MONSTER.AttackDistanseCheck(value) == true)//Move
        {
            aIState = AiState.Attack;
        }
        else
        {
            aIState = AiState.Move;
        }
    }

    public void searchingStateUpdate(SearchState _state) 
    {
        //searchingState = _state;
    }
}
