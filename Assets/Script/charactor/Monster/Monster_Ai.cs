using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Monster : Character
{
    
    protected virtual void FixedUpdate()
    {
        if (MONSTERAI == null||condition == Condition.Death) { return; }
        MONSTERAI.State();
    }
    public bool AttackStateLoad()
    {
        if (monsterStateData.AttackState == MonsterAttackState.Attack_On)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    public void AiStateUpdate(AiState _state)
    {
        MONSTERAI.AIStateUpdate(_state);
    }
    public void AiTagetUpdate(bool _check)
    {
        MONSTERAI.TargetStatUpdate(_check);
    }
    public void AiUpdate(Player _player)
    {
        MONSTERAI.ChangeTarget(_player);
    }
    public bool RadiusCheck(float _value) 
    {
        if (_value < radius)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }


    protected void Compomentinit()
    {
        //cam = Camera.main;
        
        Slot[] children = GetComponentsInChildren<Slot>();
        foreach (Slot child in children)
        {
            int layer = child.gameObject.layer;
            if (layer == LayerMask.NameToLayer(LayerName.MonsterMovePosition.ToString()))
            {
                slots.Add(child);
                //Slot slot = child;
                //slot.ObjectState = PositionObjectState.Empty;
            }
        }
    }
    public bool TargetSearch(out Player _player) //Search bool
    {
        _player = Shared.GameManager.playerSearch(gameObject, radius);
        if (_player == null)
        {
            Debug.LogError($"_player = {_player}");
            return false;
        }
        else
        {
            Vector3 targetPos = _player.BodyObjectLoad().position;
            Vector3 myPos = gameObject.transform.position;

            float distance = Vector3.Distance(targetPos, myPos);

            if (radius >= distance)
            {
                return true;
            }
            else
            {
                _player = null;
                return false;
            }
        }
    }
    public Vector3 FindSlot() 
    {
        slotCount = Random.Range(0, slots.Count);
        if (slots[slotCount] == null)
        {
            slotCount = 0;
        }
        monsterStateData.WalkState = MonsterWalkState.Walk_On;

        return slots[slotCount].gameObject.transform.position;
    }
    public virtual void MovePoint()//Search
    {

    }

    public float TargetDistanseCheck(Vector3 _pos)//수정 필요
    {
        Vector3 direction = _pos - charactorModelTrs.position;
        direction.y = 0f;

        float distance = direction.magnitude;

        return distance;
    }

    public bool AttackDistanseCheck(float _value)
    {
        float typeVAlue = 0.0f;
        switch (monsterStateData.MonsterType)
        {
            case MonsterType.Spider:
                typeVAlue = 15.0f;
                break;
            case MonsterType.Dron:
                typeVAlue = 0.3f;
                break;
            case MonsterType.Sphere:
                typeVAlue = 0.3f;
                break;
            case MonsterType.Boss:
                typeVAlue = 5.0f;
                break;
            default:
                Debug.LogError($"monsterStateData.MonsterType = {monsterStateData.MonsterType}");
                break;
        }
        if (_value <= typeVAlue)//값을 상수가 아닌값으로 수정 필요
        {
            // _value = 0;
            return true;
        }
        else
        {
            return false;
        }

    }

    public virtual void Ai_TargetMove(Vector3 _pos, float _distance)
    {

    }

    public virtual void Ai_Attack(Transform _transform)//거리이내에 있는 적에게 데미지 로직 필요
    {

    }

    protected virtual void MonsterAttack()
    {
        monsterStateData.AttackState = MonsterAttackState.Attack_On;
        attackAnimation(monsterStateData.AttackState);
    }

    public override bool CharacterStateCheck()
    {
        if (monsterStateData.AttackState == MonsterAttackState.Attack_Off)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool DamageEventCheck()
    {
        if (monsterStateData.DamageEvent == DamageEvent.Event_On)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public override void DamageEventUpdate(DamageEvent _event)
    {
        monsterStateData.DamageEvent = _event;
    }
}
