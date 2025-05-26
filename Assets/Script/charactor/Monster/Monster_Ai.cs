using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Monster : Character
{
    Player HItPalyer;
    List<Slot>slots = new List<Slot>();
    int slotCount = 0;
    protected MonsterWalkState walkState = MonsterWalkState.Walk_Off;
    protected MonsterAttackState attackState = MonsterAttackState.Attack_Off;
    public float SphereRadius = 1.0f; // 구 반지름
    protected float stopDistanseValue = 0.2f;
    protected float searchRange = 20.0f;
    protected float attackRange = 5.0f;
    Vector3 targetPos;
    Vector3 movePosition = Vector3.zero;

    protected virtual void FixedUpdate()
    {
        if (AI == null||condition == Condition.Death) { return; }
        AI.State();
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
    
    public void MovePoint(Vector3 _pos)//Search
    {
        //Vector3 vector = Vector3.zero;
        if (walkState != MonsterWalkState.Walk_On)
        {
            slotCount = Random.Range(0, slots.Count);
            if (slots[slotCount] == null) 
            {
                slotCount = 0;
            }
            walkState = MonsterWalkState.Walk_On;
        }

        Vector3 pos = slots[slotCount].gameObject.transform.position;

        Vector3 disTance = (pos - charactorModelTrs.position);
        disTance.y = 0.0f;

        float dist = Vector3.Distance(pos,charactorModelTrs.position);

        Quaternion rotation = Quaternion.LookRotation(disTance.normalized);
        if (dist < stopDistanseValue)
        {
            //Debug.Log($"[모델 위치] {charactorModelTrs.position} / [타겟 위치] {targetPos} / [거리] {disTance}");
            walkState = MonsterWalkState.Walk_Off;
            //slotCount++;
        }
        else 
        {
            //charactorModelTrs.position += disTance.normalized * speedValue * Time.deltaTime;

            //charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation,
            //    rotation, Time.deltaTime * rotationSpeed);

            if (monsterType == MonsterType.Sphere)
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


            //charactorModelTrs.position += movePos;

            //Quaternion rotation = Quaternion.LookRotation(disTance.normalized);

            // charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation,rotation, Time.deltaTime * rotationSpeed);

            //charactorModelTrs.position += Vector3.forward * speedValue * Time.deltaTime;
            //Vector3 movePos = charactorModelTrs.forward * speedValue * Time.deltaTime;

            //charactorModelTrs.Translate(movePos);

            //transform.position += movePos;

            //transform.position += movePos.normalized * speedValue * Time.deltaTime;
            //Debug.Log($"[이동 전] {Pos} -> [이동 후] {charactorModelTrs.localPosition}");


            //charactorModelTrs.position += Vector3.forward * Time.deltaTime * 10f;
        }
        moveAnimation(walkState);

    }
    public bool TargetSearch() //Search bool
    {

        List<Player> playerData = Shared.GameManager.PlayerObj;
        
        for (int i = 0; i < playerData.Count; i++ ) 
        {
            Player player = playerData[i];
            float value = Vector3.Distance(player.gameObject.transform.position,
                charactorModelTrs.position);
            if (value <= searchRange)
            {
                targetPos = player.gameObject.transform.position;
                HItPalyer = player;

                
                return true;
            }
        }
        return false;
    }
    public bool TargetAttackMove(out SearchState _state)//move bool
    {
        float distanse = Vector3.Distance(targetPos, charactorModelTrs.transform.position);

        if (distanse < attackRange)
        {
            _state = SearchState.Stop;//멈추고 공격 준비

            walkState = MonsterWalkState.Walk_Off;
            moveAnimation(walkState);

            return true;
        }
        else 
        {
            if (distanse > 20.0f) 
            {
                _state = SearchState.Move;//다시 이동
                return false; 
            }

            _state = SearchState.TargetOn;//목표물 까지 이동

            Vector3 dist = targetPos - charactorModelTrs.transform.position;
            dist.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(dist.normalized);

            //charactorModelTrs.transform.position += dist.normalized * speedValue * Time.deltaTime;
            

            if (monsterType == MonsterType.Sphere)
            {
                RootTransform.position += dist.normalized * speedValue * Time.deltaTime;
                charactorModelTrs.parent.rotation = Quaternion.Slerp(charactorModelTrs.parent.rotation,
                rotation, Time.deltaTime * rotationSpeed);
            }
            else
            {
                charactorModelTrs.transform.position += dist.normalized * speedValue * Time.deltaTime;
                charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation,
                rotation, Time.deltaTime * rotationSpeed);
            }

        }
        return false;
    }

    public void MonsterAttack(out SearchState _state)
        //돌진 공격을 하는 몬스터는 로직을 다르게 할 필요가 있음
        //Attack
    {
        if (monsterType == MonsterType.Sphere||
            monsterType == MonsterType.Dron)
        {
            attackAnimation(MonsterAttackState.Attack_On);
        }
        else if (monsterType == MonsterType.Spider) 
        {
            Granad granad = weaponObj.GetComponent<Granad>();

            if (granad.skillstate == SkillState.SkillOff)
            {
                attackAnimation(MonsterAttackState.Attack_On);
            }
        }
        _state = SearchState.Wait;
        StartCoroutine(WaitTimer(_state));
    }
    IEnumerator WaitTimer(SearchState _search)
    {
        yield return new WaitForSeconds(3.0f);
        _search = SearchState.None;
        AI.searchingStateUpdate(_search);
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
    protected void SortingObject()
    {

    }
}
