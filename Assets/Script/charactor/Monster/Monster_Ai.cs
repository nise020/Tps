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
    public void AiTagetUpdate(bool _check)
    {
        MONSTERAI.TargetStatUpdate(_check);
    }
    public void AiUpdate(Player _player)
    {
        MONSTERAI.ChangeTarget(_player);
    }


    public void Compomentinit()
    {
        //cam = Camera.main;
        monsterAnimator = GetComponent<Animator>();
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
    public void MovePoint()//Search
    {
        if (stopDilay == false) return;

        if (movePosition == Vector3.zero) 
        {
            movePosition = FindSlot();
        }

        Vector3 disTance = (movePosition - charactorModelTrs.position);
        disTance.y = 0.0f;

        float dist = Vector3.Distance(movePosition, charactorModelTrs.position);

        if (dist <= stopDistanseValue)
        {
            //stopDilay = false;
            movePosition = Vector3.zero;
            if (monsterStateData.WalkState != MonsterWalkState.Walk_Off)
            {
                monsterStateData.WalkState = MonsterWalkState.Walk_Off;
            }
            //slotCount++;
        }
        else
        {
            Quaternion rotation = Quaternion.LookRotation(disTance.normalized);

            if (monsterStateData.MonsterType == MonsterType.Sphere)
            {
                RootTransform.position += disTance.normalized * speedValue * Time.deltaTime;

                charactorModelTrs.parent.rotation = Quaternion.Slerp(charactorModelTrs.parent.rotation,
                rotation, Time.deltaTime * rotationSpeed);
            }
            else
            {
                charactorModelTrs.position += disTance.normalized * speedValue * Time.deltaTime;

                charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation,
                    rotation, Time.deltaTime * rotationSpeed);
            }

            moveAnimation(monsterStateData.WalkState);
        }

    }

    protected void attackRangeCheck()
    {
        attackAnimation(MonsterAttackState.Attack_On);

        float dist = Vector3.Distance(charactorModelTrs.position, HItPalyer.transform.position);
        if (dist < stopDistanseValue)
        {
            Shared.BattelManager.DamageCheck(this, HItPalyer);
        }
    }

    public float TargetDistanseCheck(Vector3 _pos)//수정 필요
    {
        Vector3 direction = _pos - transform.position;
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

    public void Ai_TargetMove(Vector3 _pos, float _distance)
    {
        Vector3 direction = _pos - charactorModelTrs.transform.position;
        float distanse = Vector3.Distance(_pos, charactorModelTrs.transform.position);

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        //charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation, targetRotation, Time.deltaTime * rotationSpeed);


        if (monsterStateData.MonsterType == MonsterType.Sphere)
        {
            RootTransform.position += direction.normalized * speedValue * Time.deltaTime;

            charactorModelTrs.parent.rotation = Quaternion.Slerp(charactorModelTrs.parent.rotation,
            targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            charactorModelTrs.transform.position += direction.normalized * speedValue * Time.deltaTime;

            charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation,
            targetRotation, Time.deltaTime * rotationSpeed);
        }

        moveAnimation(monsterStateData.WalkState);
    }
    public void Ai_Attack(Transform _transform)//거리이내에 있는 적에게 데미지 로직 필요
    {
        //charactorModelTrs.rotation = Quaternion.LookRotation(_transform.position);
        
        stopDilay = true;

        monsterStateData.WalkState = MonsterWalkState.Walk_Off;
        moveAnimation(monsterStateData.WalkState);

        charactorModelTrs.LookAt(_transform);

        MonsterAttack();

        //Debug.Log($"{_transform.position}");
        //charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        //npcRunStateAnimation(0.0f);

        //AutoAttack();
    }

    public virtual void MonsterAttack()
    //돌진 공격을 하는 몬스터는 로직을 다르게 할 필요가 있음
    //Attack
    {
        if (monsterStateData.MonsterType == MonsterType.Sphere ||
           monsterStateData.MonsterType == MonsterType.Dron)
        {
            attackAnimation(MonsterAttackState.Attack_On);
        }
        else if (monsterStateData.MonsterType == MonsterType.Spider)
        {
            Granad granad = weaponObj.GetComponent<Granad>();

            if (granad.skillstate == SkillState.SkillOff)
            {
                attackAnimation(MonsterAttackState.Attack_On);
            }
        }
        //_state = SearchState.Wait;
        //StartCoroutine(WaitTimer(_state));
    }
    //IEnumerator WaitTimer(SearchState _search)
    //{
    //    yield return new WaitForSeconds(3.0f);
    //    _search = SearchState.None;
    //    MONSTERAI.searchingStateUpdate(_search);
    //}

}
