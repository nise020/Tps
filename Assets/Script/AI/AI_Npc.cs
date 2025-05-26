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
            case NpcAiState.Move://¼±»§ÇÊ½Â
                Move(targetPos);
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
        //Shared.GameManager.PlayerLoad(out Player _player);
        PLAYER.Ai_Move(FollowPlayer);

        //Debug.Log($"npcAi={npcAi}\n_player = {_player}");
        if (PLAYER.SearchCheck(out tagetPos) == true)
        {
            npcAi = NpcAiState.Move;
        }
        else //Not Find Monster
        {
            return;
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
        PLAYER.AiAttack();
        npcAi = NpcAiState.Reset;
    }
    protected override void Reset()
    {
        npcAi = NpcAiState.Search;
        tagetPos = new Vector3();
        //npcAi = NpcAiState.Search;
    }

}
