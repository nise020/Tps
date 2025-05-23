using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class AI_Npc : AiBase
{
    Vector3 tagetPos = new Vector3(0f,0f,0f);
    public override void State(CharctorStateEnum _state, Player _player,out NpcAiState _Ai)
    {
        switch (npcAi)
        {
            case NpcAiState.Search:
                Search(_player);
                break;
            case NpcAiState.Move://�����ʽ�
                Move(_player, tagetPos);
                break;
            case NpcAiState.Attack:
                Attack(_player);
                break;
            case NpcAiState.Reset:
                Reset();
                break;
        }
        _Ai = npcAi;
    }
    float viewDistance = 10f;
    float viewAngle = 60f;
    public float sphereRadius = 50.0f;
    
    protected override void Search(Player _obj)
    {
        Shared.GameManager.PlayerData(out Player _player);
        _obj.Ai_Move(_player);

        Debug.Log($"npcAi={npcAi}\n_player = {_player}");
        //Que
        //Time
        //Vector
        if (_obj.SearchCheck(out tagetPos) == true)
        {
            npcAi = NpcAiState.Move;
        }
        else //Not Find Monster
        {
            return;
        }
    }
    protected override void Move(Player _obj, Vector3 _pos)
    {
        Debug.Log($"npcAi={npcAi}");
        float value = _obj.TargetPosition_Move(_pos);
        if (_obj.AttackDistanseCheck(value) == true)//Move
        {
            npcAi = NpcAiState.Attack;
        }
    }
    protected override void Attack(Player _obj)
    {
        Debug.Log($"npcAi={npcAi}");
        _obj.AiAttack();
        npcAi = NpcAiState.Reset;
    }
    protected override void Reset()
    {
        npcAi = NpcAiState.Search;
        tagetPos = new Vector3();
        //npcAi = NpcAiState.Search;
    }

}
