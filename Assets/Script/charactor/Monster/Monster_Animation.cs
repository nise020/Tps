using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : Charactor
{
    protected Animator mobAnimator;
    protected override void moveAnimation(MonsterWalkState _state) 
    {
        if (_state != MonsterWalkState.Walk_On)
        {
            _state = MonsterWalkState.Walk_On;
            mobAnimator.SetInteger(MonsterAnimParameters.Walk.ToString(), 1);
        }
        else 
        {
            _state = MonsterWalkState.Walk_Off;
            mobAnimator.SetInteger(MonsterAnimParameters.Walk.ToString(), 0);
        }
    }
    protected override void attackAnimation(MonsterAttackState _state) 
    {
        if (_state != MonsterAttackState.Attack_On)
        {
            _state = MonsterAttackState.Attack_On;
            mobAnimator.SetInteger(MonsterAttackState.Attack_On.ToString(), 1);
        }
        else 
        {
            _state = MonsterAttackState.Attack_Off;
            mobAnimator.SetInteger(MonsterAttackState.Attack_On.ToString(), 0);
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
