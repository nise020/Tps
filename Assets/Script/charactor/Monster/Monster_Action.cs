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
    protected float StopDistanseValue = 0.3f;
    public void init()
    {
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
        FindBodyObject();
    }
    public void Attack()
    {
        attackAnimation(attackState);
        attackRangeCheck();
    }
    protected void attackRangeCheck() 
    {

    }
    protected void SortingObject() 
    {

    }
    public void MovePoint()
    {
        Vector3 vector = Vector3.zero;
        if (walkState != MonsterWalkState.Walk_On)
        {
            moveAnimation(walkState);
            slotCount = Random.Range(0, slots.Count);
            vector = slots[slotCount].gameObject.transform.position;
        }

        float distanse = Vector3.Distance(charactorModelTrs.transform.localPosition, vector);
        
        if (distanse > StopDistanseValue)
        {
            vector.Normalize();
            charactorModelTrs.transform.localPosition = vector * speedValue * Time.deltaTime;
            moveAnimation(walkState);
            Quaternion quaternion = Quaternion.LookRotation(vector);
            charactorModelTrs.transform.localRotation = Quaternion.Slerp(charactorModelTrs.transform.localRotation,
                quaternion, Time.deltaTime * rotationSpeed);
        }
        else 
        {

        }

    }
    public void TargetAttackMove()
    {
        
    }
    public bool TargetSearch() 
    {
        List<Player> playerData = Shared.GameManager.PlayerObj;
        //Vector3.Distance(charactorModelTrs.transform.position,);
        if (this)
        {
            return true;
        }
        else 
        {
            return false;
        }
        
        //RaycastHit hit;

        //Vector3 position = gameObject.transform.position;
        //Vector3 direction = gameObject.transform.forward;

        //if (Physics.SphereCast(position, sphereRadius, direction, out hit, viewDistance))
        //{
        //    int layer = hit.collider.gameObject.layer;
        //    if (layer != Delivery.LayerNameEnum(LayerName.Player)) { return; }

        //    if (layer == Delivery.LayerNameEnum(LayerName.Player))
        //    {
        //        //moveAnim = false;
        //        mobAnimator.SetInteger(MonsterAnimParameters.Search.ToString(), 0);
        //        aIState = MonsterAiState.Attack;
        //        targetPos = hit.collider.gameObject.transform.position;//Vector3
        //        //searchAnim = false;//clear
        //    }
        //}
        //MONSTER.NextPoint(ref searchingOnOff);//서치
    }

    
}
