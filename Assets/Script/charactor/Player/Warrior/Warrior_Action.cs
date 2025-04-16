using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Warrior : Player
{
    [SerializeField] GameObject skillEffect;
    protected override void attack(CharctorStateEnum _state, CharactorJobEnum _job)
    {
        if (_state == CharctorStateEnum.Player)
        {
            playerAnim.SetInteger(PlayerAnimParameters.GetWeapon.ToString(), 1);
            //AttackAnim(1);
        }
    }

    protected override void commonRSkill(CharactorJobEnum _type)
    {
        if (_type == CharactorJobEnum.Warrior) 
        {

        }

    }
    protected override void commonskillAttack1(CharactorJobEnum _type)
    {
        if (_type == CharactorJobEnum.Warrior)
        {
            if (skillCheck == SkillRunning.SkillOff)
            {
                skillStrategy.Skill(playerType, 1,out attackValue);
                skillCheck = SkillRunning.SkillOn;

                playerAnim.SetInteger(SkillType.Skill1.ToString(), 1);
                skillEffect.transform.SetParent(weapon.gameObject.transform);
                skillEffect.transform.localPosition = Vector3.zero;

                playerAnim.SetInteger(PlayerAnimName.AttackSkill.ToString(), 1);
                Invoke("SkillValueReset", 3);//clear
            }
            else
            {
                return;
            }
        }
    }
    protected override void commonskillAttack2(CharactorJobEnum _type)
    {
        if (_type == CharactorJobEnum.Gunner)
        {
            if (skillCheck == SkillRunning.SkillOff)
            {
                skillStrategy.Skill(playerType, 2, out attackValue);
                skillCheck = SkillRunning.SkillOn;

                playerAnim.SetInteger(SkillType.Skill2.ToString(), 1);
                playerAnim.SetInteger(PlayerAnimName.BuffSkill.ToString(), 1);
                Invoke("SkillValueReset", 3);//clear
            }
            else
            {
                return;
            }
        }
        else { return; }
    }
    protected override void cameraModeChange()
    {
        //if (cameraMode == PlayerCameraMode.CameraRotationMode)
        //{
        //    cameraMode = PlayerCameraMode.GunAttackMode;
        //    viewcam.CameraModeInit(cameraMode);
        //}
        //else if (cameraMode == PlayerCameraMode.GunAttackMode)
        //{
        //    cameraMode = PlayerCameraMode.CameraRotationMode;
        //    viewcam.CameraModeInit(cameraMode);
        //}
    }
    protected override void SkillValueReset()//Damage Reset
    {
        attackValue = attackReset;
        skillCheck = SkillRunning.SkillOff;
        playerAnim.SetInteger(PlayerAnimName.AttackSkill.ToString(), 0);
        playerAnim.SetInteger(PlayerAnimName.BuffSkill.ToString(), 0);
    }
    protected override void ReloadOut()//AnimationEvent
    {
        //AnimationEvent
        reloadState = ReloadState.ReloadOff;
        GUN.nowbullet = GUN.bullet;
        GUN.bulletcount = 0;
        playerAnim.SetLayerWeight(attackLayerIndex, 0.0f);
        //playerAnim.SetLayerWeight(BaseLayerIndex, 1.0f);
        playerAnim.SetInteger(PlayerAnimParameters.Reload.ToString(), 0);
    }
}
