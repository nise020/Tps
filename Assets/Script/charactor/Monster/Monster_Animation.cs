using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public partial class Monster : Character
{
    protected Animator monsterAnimator;

    public void AttackAnimationEvent()//AnimationEvent
    {
        //sin 곡선<- gmsemffla
        if (monsterType == MonsterType.Sphere)
        {
            DirectAttack(charactorModelTrs.gameObject, HItPalyer.transform.position);
            attackRangeCheck();
        }
        else if (monsterType == MonsterType.Spider)
        {        
            weaponObj.SetActive(true);
            //granaidAttack(charactorModelTrs.position, HItPalyer.transform.position, weaponObj);
            granaidAttack(weaponHandObject.transform.position, HItPalyer.transform.position, weaponObj);

        }
        else if (monsterType == MonsterType.Dron)
        {
            DirectAttack(charactorModelTrs.gameObject, HItPalyer.transform.position);
            attackRangeCheck();
        }
    }

    public void AttackAnimationOut()//AnimationEvent
    {
        attackState = MonsterAttackState.Attack_Off;
        attackAnimation(MonsterAttackState.Attack_Off);
    }
    public void DeathAnimationOut()//AnimationEvent
    {
        deathAnimation(MonsterDeathsState.Deaths_Off);
    }

    public void DeathEffect()//AnimationEvent
    {
        base.death();

        int key = Random.Range(0, ITEMLists.Count);

        if (DropItemData.TryGetValue(ITEMLists[key], out GameObject obj)) 
        {
            obj.transform.position = charactorModelTrs.position;
            obj.SetActive(true);
            //Debug.Log($"{obj} = {obj.transform.position}\n" +
            //          $"{charactorModelTrs.gameObject} = {charactorModelTrs.position}");
        }
        
        //Item_Object - Weapon1(UnityEngine.GameObject) = (6.95, 2.85, -23.29)
        //Model(UnityEngine.GameObject) = (6.95, 2.85, -23.29)

        //Resurrection
        Shared.MonsterManager.Resurrection(mobKey);

        //Reset
        stateInIt();
        HPBAR.SetHp(maxHP, cheHP);

        charactorModelTrs.position = startPosition;//PositionReset

        charactorModelTrs.gameObject.SetActive(false);//Body off
    }

    protected void deathAnimation(MonsterDeathsState _state) 
    {
        if (_state == MonsterDeathsState.Deaths_On)
        {
            monsterAnimator.SetInteger(MonsterAnimParameters.Death.ToString(), 1);
        }
        else if (_state == MonsterDeathsState.Deaths_Off)
        {
            monsterAnimator.SetInteger(MonsterAnimParameters.Death.ToString(), 0);
        }
    }


    protected override void moveAnimation(MonsterWalkState _state) 
    {
        if (_state == MonsterWalkState.Walk_On)
        {
            //_state = MonsterWalkState.Walk_On;
            monsterAnimator.SetInteger(MonsterAnimParameters.Walk.ToString(), 1);
        }
        else 
        {
            //_state = MonsterWalkState.Walk_Off;
            monsterAnimator.SetInteger(MonsterAnimParameters.Walk.ToString(), 0);
        }
    }


    protected override void attackAnimation(MonsterAttackState _state) 
    {
        if (_state == MonsterAttackState.Attack_On)
        {
            //_state = MonsterAttackState.Attack_On;
            monsterAnimator.SetInteger(MonsterAnimParameters.Attack.ToString(), 1);
        }
        else 
        {
            //_state = MonsterAttackState.Attack_Off;
            monsterAnimator.SetInteger(MonsterAnimParameters.Attack.ToString(), 0);
        }
    }
    protected bool AnimationCheck(MonsterWalkState _state)
    {
        return false;
    }
    protected bool AnimationCheck(MonsterAttackState _state)
    {
        return false;
    }
    //이더 스크롤

}
