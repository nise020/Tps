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
        if (_state == CharctorStateEnum.Player) 
        {
            _Ai = npcAi;
            return;
        } 
        switch (npcAi)
        {
            case NpcAiState.Search:
                Search(_player, tagetPos);
                break;
            case NpcAiState.Move://¼±»§ÇÊ½Â
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
    protected override void Search(Player _obj,Vector3 _pos)
    {
        _obj.Move_Npc();
        if (_obj.SearchCheck(out _pos) == true)
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
        if (_obj.TargetMove(_pos) == true)
        {
            npcAi = NpcAiState.Attack;
        }
    }
    protected override void Attack(Player _obj)
    {
        _obj.AutoAttack();
        npcAi = NpcAiState.Reset;
    }
    protected override void Reset()
    {
        tagetPos = new Vector3();
        npcAi = NpcAiState.Search;
    }

}
