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

        Debug.Log($"[Dead] Ÿ�� �ʱ�ȭ �Ϸ�, ���� �ʱ�ȭ");
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

    protected override void Attack()//����
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
    protected override void Reset()//����Ŭ ��(���� �ٽ� ���� ��� Ž��)
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
