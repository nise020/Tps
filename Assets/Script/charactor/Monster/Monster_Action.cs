using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public partial class Monster : Charactor
{
    Player HItPalyer;
    List<Slot>slots = new List<Slot>();
    int slotCount = 0;
    protected MonsterWalkState walkState = MonsterWalkState.Walk_Off;
    protected MonsterAttackState attackState = MonsterAttackState.Attack_Off;
    public float SphereRadius = 1.0f; // 구 반지름
    protected float stopDistanseValue = 0.2f;
    protected float searchRange = 10.0f;
    protected float attackRange = 5.0f;
    Vector3 targetPos;
    Vector3 movePosition = Vector3.zero;
    public void init()
    {
        cam = Camera.main;
        mobAnimator = GetComponent<Animator>();
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
    
    public void MovePoint(Vector3 _pos)
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
        
        Debug.Log($"charactorModelTrs: {charactorModelTrs.name}, \n " +
            $"position: {charactorModelTrs.position}");
        if (dist < stopDistanseValue)
        {
            //Debug.Log($"[모델 위치] {charactorModelTrs.position} / [타겟 위치] {targetPos} / [거리] {disTance}");
            walkState = MonsterWalkState.Walk_Off;
            //slotCount++;
        }
        else 
        {
            charactorModelTrs.position = disTance.normalized * speedValue * Time.deltaTime;

            //charactorModelTrs.position += movePos;

            Quaternion rotation = Quaternion.LookRotation(disTance.normalized);
           
            charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation,
                rotation, Time.deltaTime * rotationSpeed);

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
    public bool TargetSearch() 
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
    public bool TargetAttackMove()
    {
        float distanse = Vector3.Distance(targetPos, charactorModelTrs.transform.position);

        if (distanse < attackRange)
        {
            return true;
        }
        else 
        {
            Vector3 dist = targetPos - charactorModelTrs.transform.position;
            charactorModelTrs.transform.position += dist.normalized * speedValue * Time.deltaTime;
            
            Quaternion rotation = Quaternion.LookRotation(dist.normalized);

            charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation,
                rotation, Time.deltaTime * rotationSpeed);
        }
        return false;
    }

    public void MonsterAttack()
    {
        attackRangeCheck();//Hit
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
