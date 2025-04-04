using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class AI_Npc : AiBase
{
    Player FollowPlayerObj;
    float speed = 0.0f;
    Vector3 tagetPos = new Vector3(0f,0f,0f);
    public void PlayerItit(Player _player) 
    {
        FollowPlayerObj = _player;
    }
    public override void State(CharctorStateEnum _state, Player _player)
    {
        if (_state == CharctorStateEnum.Player) return;
        switch (npcAi)
        {
            case NpcAiState.Search:
                Search(_player, tagetPos);
                break;
            case NpcAiState.Move://선빵필승
                Move(_player, tagetPos);
                break;
            case NpcAiState.Attack:
                Attack(_player);
                break;
            case NpcAiState.Reset:
                Reset();
                break;
        }
    }
    float viewDistance = 10f;
    float viewAngle = 60f;
    public float sphereRadius = 50.0f;
    protected override void Search(Player _obj,Vector3 _pos)
    {
        Debug.Log($"Search");

        RaycastHit hit;
        if (Physics.SphereCast(_obj.transform.position, sphereRadius, _obj.transform.forward, out hit, viewDistance))
        {
            //방향이 다르기 떄문에 수정 필요
            int layer = hit.collider.gameObject.layer;
            if (layer == Delivery.LayerNameEnum(LayerName.Monster))
            {
                hit.point = tagetPos;
                npcAi = NpcAiState.Move;
            }
        }
        else //Not Find Monster
        {
            return;
            //npcAi = NpcAiState.Move;
        }
        //Debug.DrawRay(_obj.transform.position, _obj.transform.forward,Color.red,0);
        Debug.DrawLine(_obj.transform.position, hit.point, Color.red,0);
        //else { return; }
        //주변에 몬스터가 없으면 플레이어를 따라 다니고
        //주변에 몬스터가 있으면 따라가서 줘 팬다
    }
    protected override void Move(Player _obj, Vector3 _pos)
    {
        if (_obj.Move_Attack() == true)
        {
            npcAi = NpcAiState.Attack;
        }
    }
    protected override void Attack(Player _obj)
    {
        _obj.AutoAttack();
        //애니메이션 이벤트도 추가 필요
        npcAi = NpcAiState.Reset;
    }
    protected override void Reset()
    {
        tagetPos = new Vector3();
        npcAi = NpcAiState.Search;
    }

}
