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
            //������ �ٸ��� ������ ���� �ʿ�
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
        //�ֺ��� ���Ͱ� ������ �÷��̾ ���� �ٴϰ�
        //�ֺ��� ���Ͱ� ������ ���󰡼� �� �Ҵ�
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
        //�ִϸ��̼� �̺�Ʈ�� �߰� �ʿ�
        npcAi = NpcAiState.Reset;
    }
    protected override void Reset()
    {
        tagetPos = new Vector3();
        npcAi = NpcAiState.Search;
    }

}
