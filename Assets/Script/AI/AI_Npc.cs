using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class AI_Npc : AiBase
{
    Vector3 tagetPos = new Vector3(0f,0f,0f);
    float viewDistance = 10f;
    float viewAngle = 60f;
    Player FollowPlayer;
    Monster TagetMonster;
    public Action<bool> AttackEvent;

    public void init(Player _player)
    {
        PLAYER = _player;
       //GameEvents.DefenderStateCheck += DefenderState;
    }
    
    public void DefenderState(bool isDefenderDead)//Reset
    {
        if (isDefenderDead)
        {
            npcAi = NpcAiState.Search;
        }
        else
        {
            npcAi = NpcAiState.Move;
        }
    }
    public void ChangePlayer(Player _player) 
    {
        FollowPlayer = _player;
    }
    public override void State()
    {
        switch (npcAi)
        {
            case NpcAiState.Search:
                Search();
                break;
            case NpcAiState.Move:
                Move(tagetPos);
                break;
            case NpcAiState.Attack:
                Attack();
                break;
            case NpcAiState.Reset:
                Reset();
                break;
        }
    }
    
    protected override void Search()
    {

        //Debug.Log($"npcAi={npcAi}\n_player = {_player}");
        if (PLAYER.SearchCheck(out tagetPos) == true)
        {
            npcAi = NpcAiState.Move;
        }
        else //Not Find Monster
        {
            PLAYER.Ai_Move(FollowPlayer);
        }
    }
    protected override void Move(Vector3 _pos)
    {
        //Vector3 pos = Vector3.zero;
        //Debug.Log($"npcAi={npcAi}");
        float value = PLAYER.TargetPosition_Move(_pos);
        if (PLAYER.AttackDistanseCheck(value) == true)//Move
        {
            npcAi = NpcAiState.Attack;
        }
    }
    protected override void Attack()
    {
        //Debug.Log($"npcAi={npcAi}");
        PLAYER.Ai_Attack();//일정 타임 마다 동작
        npcAi = NpcAiState.Reset;
    }
    
    
    protected override void Reset()
    {
        tagetPos = new Vector3();
    }

}
