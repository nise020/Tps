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

    protected NpcAiState npcAi = NpcAiState.Search;
    public void init(Player _player)
    {
        PLAYER = _player;
    }
    
    public void DefenderState(bool isDefenderDead)//Reset
    {
        if (isDefenderDead)
        {
            TagetAive = false;
            TagetMonster = null;
            tagetTrs = null;
            Debug.Log($"Monster = {TagetMonster}");
        }
        if (npcAi == NpcAiState.Attack || npcAi == NpcAiState.Reset || npcAi == NpcAiState.Move)
        {
            npcAi = NpcAiState.Search;
        }

        Debug.Log($"[Dead] Ÿ�� �ʱ�ȭ �Ϸ�, ���� �ʱ�ȭ");
    }
    public void ChangePlayer(Player _player) 
    {
        FollowPlayer = _player;
    }
    public override void State()
    {
        if (!TagetAive && npcAi != NpcAiState.Search)
        {
            npcAi = NpcAiState.Search;
        }

        switch (npcAi)
        {
            case NpcAiState.Search:
                Search();
                break;
            case NpcAiState.Move:
                Move(tagetTrs.position);
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
        if (PLAYER.SearchCheck(out TagetMonster) == true)
        {
            tagetTrs = TagetMonster.BodyObjectLoad();
            TagetAive = true;
            npcAi = NpcAiState.Move;
        }
        else //Not Find Monster
        {
            Debug.Log($"npcAi={npcAi}");
            PLAYER.Ai_Move(FollowPlayer);
        }
    }
    protected override void Move(Vector3 _pos)
    {
        //_pos <- ��ǥ������ �Ÿ�
        //value = �Ÿ��� ��ġ��
        Debug.Log($"npcAi={npcAi}");

        float value = PLAYER.TargetDistanseCheck(_pos);

        if (PLAYER.AttackDistanseCheck(value) == true)//Move
        {
            npcAi = NpcAiState.Attack;
        }
        else 
        {
            PLAYER.AI_TargetMove(_pos, value);
        }
    }
    protected override void Attack()
    {
        if (!TagetAive || tagetTrs == null) 
        {
            npcAi = NpcAiState.Search;
            return;
        }
        Debug.Log($"npcAi={npcAi}");
        PLAYER.Ai_Attack(tagetTrs);//���� Ÿ�� ���� ����

        npcAi = NpcAiState.Reset;
    }
    
    
    protected override void Reset()
    {
        float value = PLAYER.TargetDistanseCheck(tagetTrs.position);

        if (PLAYER.AttackDistanseCheck(value) == true)//Move
        {
            npcAi = NpcAiState.Attack;
        }
        else
        {
            npcAi = NpcAiState.Move;
        }
    }

}
