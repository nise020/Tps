using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Monster : Charactor
{
    
    List<Slot>slots = new List<Slot>();
    int slotCount = 0;
    protected MonsterWalkState walkState = MonsterWalkState.Walk_Off;
    protected MonsterAttackState attackState = MonsterAttackState.Attack_Off;
    public float sphereRadius = 1.0f; // 구 반지름
    public LayerMask playerLayer;
    protected float StopDistanseValue = 0.2f;
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
    public void Attack()
    {
        attackAnimation(attackState);
        attackRangeCheck();//Hit
    }
    protected void attackRangeCheck() 
    {
        
    }
    protected void SortingObject() 
    {

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

        Vector3 pos = slots[slotCount].gameObject.transform.localPosition;

        Vector3 movePos = pos - charactorModelTrs.localPosition;
        movePos.y = 0.0f;

        float disTance = Vector3.Distance(charactorModelTrs.localPosition, pos);
        Debug.Log($"슬롯 {slotCount} 위치: {slots[slotCount].transform.localPosition}");
        //moveAnimation(walkState);
        
        if (disTance > StopDistanseValue)
        {
            Vector3 Pos = charactorModelTrs.localPosition;
            charactorModelTrs.position += movePos.normalized * speedValue * Time.deltaTime;
            //transform.position += movePos.normalized * speedValue * Time.deltaTime;
            Debug.Log($"[이동 전] {Pos} -> [이동 후] {charactorModelTrs.localPosition}");

            Quaternion rotation = Quaternion.LookRotation(movePos.normalized);
            charactorModelTrs.rotation = Quaternion.Slerp(charactorModelTrs.rotation,
                rotation, Time.deltaTime * rotationSpeed);

            //charactorModelTrs.position += Vector3.forward * Time.deltaTime * 10f;
        }
        else 
        {
            Debug.Log($"[모델 위치] {charactorModelTrs.localPosition} / [타겟 위치] {targetPos} / [거리] {disTance}");
            walkState = MonsterWalkState.Walk_Off;
            slotCount++;
        }

    }
    public bool TargetSearch() 
    {
        List<Player> playerData = Shared.GameManager.PlayerObj;
        
        for (int i = 0; i < playerData.Count; i++ ) 
        {
            Player player = playerData[i];
            float value = Vector3.Distance(player.gameObject.transform.position,
                charactorModelTrs.position);
            if (value <= StopDistanseValue)
            {
                targetPos = player.gameObject.transform.position;
                return true;
            }
        }
        return false;
    }
    public bool TargetAttackMove()
    {
        float distanse = Vector3.Distance(targetPos, charactorModelTrs.transform.position);

        if (distanse > StopDistanseValue) 
        {
            Vector3 dist = targetPos - charactorModelTrs.transform.position;
            dist.Normalize();
            charactorModelTrs.transform.localPosition += dist * speedValue * Time.deltaTime;
            return false;
        }
            return true;
    }

    
}
