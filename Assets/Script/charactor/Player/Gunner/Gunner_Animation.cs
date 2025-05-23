using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Gunner : Player
{
    public void GunAttackAnimationOut() 
    {
        attackAnimation(AttackState.AttackOff);
        playerAnim.SetLayerWeight(attackLayerIndex, 0.0f);
    }
    protected override void walkAnim(RunState _runState, Vector3 _pos)
    {
        if (_pos.x == 1)//rigrt
        {
            playerAnim.SetInteger(PlayerAnimParameters.Right.ToString(), (int)_pos.x);
        }
        else if (_pos.x == -1)//left
        {
            playerAnim.SetInteger(PlayerAnimParameters.Left.ToString(), (int)_pos.x);
        }
        else if (_pos.z == 1)//front
        {
            if (_runState == RunState.Walk)
            {
                playerWalkState = PlayerWalkState.Walk_On;
                playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), (int)_pos.z);
            }
            else if (_runState == RunState.Run)
            {
                playerRunState = PlayerRunState.Run_On;
                playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), (int)_pos.z);
            }
        }
        else if (_pos.z == -1)//back
        {
            playerAnim.SetInteger(PlayerAnimParameters.Back.ToString(), (int)_pos.z);
        }
    }

    protected override void clearWalkAnimation(CharactorJobEnum _type)
    {
        base.clearWalkAnimation(_type);

        if (_type == CharactorJobEnum.Gunner)
        {
            playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
            playerAnim.SetInteger(PlayerAnimParameters.Back.ToString(), 0);
            playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
            playerAnim.SetInteger(PlayerAnimParameters.Right.ToString(), 0);
            playerAnim.SetInteger(PlayerAnimParameters.Left.ToString(), 0);
        }
    }
    public void GunSkillShoot() 
    {
        Vector3 AimDirection = weaponObj.transform.forward;
        WEAPON.Attack(AimDirection);
        SkillAnimation(SkillType.Skill1, false);
        Invoke("skillOut", 1);
    }
    public void skillOut() 
    {
        SkillParentObj1.SetActive(false);
        SkillEffectSystem1.Stop();
    }
}
