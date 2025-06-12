using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Gunner : Player
{
    public void GunShootEvent() //Animation Event
    {
        Gun gun = MAINWEAPON as Gun;
        gun.StateUpdate(GunState.Off);

        playerAnimator.applyRootMotion = false;

        if (playerStateData.ModeState == PlayerModeState.Npc ||
            playerStateData.ModeState == PlayerModeState.AutoMode)
        {
            forceUpperBody = true;
            StartCoroutine(AdjustUpperBodyToTargetOnce(gun));
            //AdjustUpperBodyToTarget(gun);
            //DirectionAssistance(gun);
        }

        gunShoot(gun);
    }
    private void gunShoot(Gun _gun) //Animation Event
    {

        Transform gunHoleTrs = _gun.GunHoleObj.transform;

        Vector3 aimDirection = gunHoleTrs.forward;

        float recoilAmount = 0.01f;
        Vector3 recoil = new Vector3(
            Random.Range(-recoilAmount, recoilAmount),
            Random.Range(-recoilAmount, recoilAmount),
            0f
        );

        aimDirection += _gun.GunHoleObj.transform.TransformDirection(recoil);
        aimDirection.Normalize();


        //Vector3 AimDirection = MainWeaponObj.transform.forward;

        MAINWEAPON.Attack(aimDirection);

    }
    public void GunAttackAnimationOut() //AnimationEvent
    {
        if (MAINWEAPON.ReturnTypeValue(BulletValueType.NowBullet) <= 0) 
        {
            playerStateData.AttackState = PlayerAttackState.Attack_Off;
            attackAnimation(playerStateData.AttackState, 0);
            playerAnimator.SetLayerWeight(attackLayerIndex, 0.0f);
        }
        if (!PLAYERAI.AtttackCheck()) 
        {
            playerStateData.AttackState = PlayerAttackState.Attack_Off;
            attackAnimation(playerStateData.AttackState, 0);
            playerAnimator.SetLayerWeight(attackLayerIndex, 0.0f);
        }
        //if (playerStateData.ModeState != PlayerModeState.None &&
        //    playerStateData.ModeState != PlayerModeState.Npc)
        //{
        //    playerStateData.AttackState = PlayerAttackState.Attack_Off;
        //    attackAnimation(playerStateData.AttackState, 0);
        //    playerAnimator.SetLayerWeight(attackLayerIndex, 0.0f);
        //}
        //else 
        //{
        //    if (!PLAYERAI.AtttackCheck() || 
        //        MAINWEAPON.ReturnTypeValue(BulletValueType.NowBullet) <= 0) 
        //    {
        //        playerStateData.AttackState = PlayerAttackState.Attack_Off;
        //        attackAnimation(playerStateData.AttackState, 0);
        //        playerAnimator.SetLayerWeight(attackLayerIndex, 0.0f);
        //    }
        //    playerStateData.AttackState = PlayerAttackState.Attack_Off;
        //}
        //if (!PLAYERAI.AtttackCheck() ||
        //        MAINWEAPON.ReturnTypeValue(BulletValueType.NowBullet) <= 0)
        //{
        //    playerStateData.AttackState = PlayerAttackState.Attack_Off;
        //    attackAnimation(playerStateData.AttackState, 0);
        //    playerAnimator.SetLayerWeight(attackLayerIndex, 0.0f);
        //}

        //playerStateData.AttackState = PlayerAttackState.Attack_Off;
        //attackAnimation(playerStateData.AttackState, 0);
        //playerAnimator.SetLayerWeight(attackLayerIndex, 0.0f);
        playerAnimator.applyRootMotion = true;
    }
    protected override void walkAnim(PlayerWalkState _state, Vector3 _pos)
    {
        if (_pos.x == 1)//rigrt
        {
            playerAnimator.SetInteger(PlayerAnimParameters.Right.ToString(), (int)_pos.x);
        }
        else if (_pos.x == -1)//left
        {
            playerAnimator.SetInteger(PlayerAnimParameters.Left.ToString(), (int)_pos.x);
        }
        else if (_pos.z == 1)//front
        {
            if (_state == PlayerWalkState.Walk)
            {
                playerStateData.WalkState = PlayerWalkState.Walk;
                playerAnimator.SetInteger(PlayerAnimParameters.Walk.ToString(), (int)_pos.z);
            }
            else if (_state == PlayerWalkState.Run)
            {
                playerStateData.WalkState = PlayerWalkState.Run;
                playerAnimator.SetInteger(PlayerAnimParameters.Run.ToString(), (int)_pos.z);
            }
        }
        else if (_pos.z == -1)//back
        {
            playerAnimator.SetInteger(PlayerAnimParameters.Back.ToString(), (int)_pos.z);
        }
    }

    protected override void clearWalkAnimation()
    {
        //base.clearWalkAnimation(_type);

        playerAnimator.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
        playerAnimator.SetInteger(PlayerAnimParameters.Back.ToString(), 0);
        playerAnimator.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
        playerAnimator.SetInteger(PlayerAnimParameters.Right.ToString(), 0);
        playerAnimator.SetInteger(PlayerAnimParameters.Left.ToString(), 0);
    }
    public void GunSkillShoot() 
    {
        Vector3 AimDirection = MainWeaponObj.transform.forward;
        MAINWEAPON.Attack(AimDirection);
        SkillAnimation(SkillType.Skill1, false);
        Invoke("skillOut", 1);
    }
    public void skillOut() 
    {
        SkillParentObj1.SetActive(false);
        SkillEffectSystem1.Stop();
    }
}
