using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class AI_Auto : AiBase
{
    Transform tagetTrs;
    float viewDistance = 10f;
    float viewAngle = 60f;
    Player FollowPlayer;
    Monster TagetMonster;
    public Action<bool> AttackEvent;
    bool TagetAive = false;

    public void init(Player _player)
    {
        PLAYER = _player;
    }
    public bool AtttackCheck() 
    {
        return TagetAive;
    }
    public void TargetStateUpdate(bool isDefenderDead)//Reset
    {
        if (isDefenderDead)
        {
            TagetAive = false;
            TagetMonster = null;
            tagetTrs = null;
            Debug.Log($"Monster = {TagetMonster}");
        }
        if (aIState == AiState.Attack || aIState == AiState.Reset || aIState == AiState.Move)
        {
            aIState = AiState.Search;
        }
    }
    public void ChangePlayer(Player _player) 
    {
        FollowPlayer = _player;
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
                Move(tagetTrs.position);
                break;
            case AiState.Attack:
                Attack();
                break;
            case AiState.Reset:
                Reset();
                break;
        }
    }
    
    protected override void Search()
    {
        if (PLAYER.SearchCheck(out TagetMonster) == true)
        {
            tagetTrs = TagetMonster.BodyObjectLoad();
            TagetAive = true;
            aIState = AiState.Move;
        }
        else //Not Find Monster
        {
            Debug.Log($"npcAi={aIState}");
            PLAYER.Ai_Move(FollowPlayer);
        }
    }
    protected override void Move(Vector3 _pos)
    {
        //_pos <- 목표물과의 거리
        //value = 거리를 수치로
        Debug.Log($"npcAi={aIState}");

        float value = PLAYER.TargetDistanseCheck(_pos);

        if (PLAYER.AttackDistanseCheck(value) == true)//Move
        {
            aIState = AiState.Attack;
        }
        else 
        {
            PLAYER.Ai_TargetMove(_pos, value);
        }
    }
    protected override void Attack()
    {
        if (!TagetAive || tagetTrs == null) 
        {
            aIState = AiState.Search;
            return;
        }
        Debug.Log($"npcAi={aIState}");
        PLAYER.Ai_Attack(tagetTrs);//일정 타임 마다 동작

        aIState = AiState.Reset;
    }
    
    
    protected override void Reset()
    {
        float value = PLAYER.TargetDistanseCheck(tagetTrs.position);

        if (PLAYER.AttackDistanseCheck(value) == true)//Move
        {
            aIState = AiState.Attack;
        }
        else
        {
            aIState = AiState.Move;
        }
    }

}
