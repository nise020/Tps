using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : Charactor
{
    protected Animator mobAnimator;

    public void AttackAnimationEvent()
    {
        //sin 곡선<- gmsemffla
        if (monsterType == MonsterType.Sphere)
        {
            DirectAttack(charactorModelTrs.gameObject, HItPalyer.transform.position);
        }
        else if (monsterType == MonsterType.Spider)
        {        
            weaponObj.SetActive(true);
            granaidAttack(charactorModelTrs.position, HItPalyer.transform.position, weaponObj);

        }
        else if (monsterType == MonsterType.Dron)
        {
            DirectAttack(charactorModelTrs.gameObject, HItPalyer.transform.position);
        }
    }

    public void AttackAnimationOut()//AnimationEvent
    {
        attackState = MonsterAttackState.Attack_Off;
        attackAnimation(MonsterAttackState.Attack_Off);
    }
    public void DeathAnimationOut()//AnimationEvent
    {
        mobAnimator.SetInteger(MonsterAnimParameters.Death.ToString(), 0);
    }
    protected override void moveAnimation(MonsterWalkState _state) 
    {
        if (_state == MonsterWalkState.Walk_On)
        {
            //_state = MonsterWalkState.Walk_On;
            mobAnimator.SetInteger(MonsterAnimParameters.Walk.ToString(), 1);
        }
        else 
        {
            //_state = MonsterWalkState.Walk_Off;
            mobAnimator.SetInteger(MonsterAnimParameters.Walk.ToString(), 0);
        }
    }
    protected override void attackAnimation(MonsterAttackState _state) 
    {
        if (_state == MonsterAttackState.Attack_On)
        {
            //_state = MonsterAttackState.Attack_On;
            mobAnimator.SetInteger(MonsterAnimParameters.Attack.ToString(), 1);
        }
        else 
        {
            //_state = MonsterAttackState.Attack_Off;
            mobAnimator.SetInteger(MonsterAnimParameters.Attack.ToString(), 0);
        }
    }
    protected void deathAnimation(MonsterAnimParameters _state) 
    {
        if (_state == MonsterAnimParameters.Death)
        {
            mobAnimator.SetInteger(MonsterAnimParameters.Death.ToString(), 1);
            condition = Condition.Death;
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
