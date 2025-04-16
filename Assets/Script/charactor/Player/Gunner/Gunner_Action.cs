using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public partial class Gunner : Player
{
    //[SerializeField] GameObject skillEffect;
    protected override void attack(CharctorStateEnum _state, CharactorJobEnum _job)
    {
        if (_state == CharctorStateEnum.Player)
        {
            if (_job == CharactorJobEnum.Gunner)
            {
                if (reloadState == ReloadState.ReloadOn || GUN.nowbullet <= 0)
                {
                    Debug.Log($"bullet = {GUN.nowbullet}");
                    return;
                }
                attackState = AttackState.AttackOn;
                Vector3 AimDirection = GUN.gameObject.transform.forward;
                playerAnim.SetLayerWeight(attackLayerIndex, 1.0f);
                AttackAnim(1);
                GUN.Attack(viewcam, AimDirection);
                playerAnim.SetLayerWeight(attackLayerIndex, 0.0f);
            }
        }
    }
    protected override void commonRSkill(CharactorJobEnum _type)//Reload
    {
        if (_type == CharactorJobEnum.Gunner || GUN.nowbullet <= 0) 
        {
            if (reloadState == ReloadState.ReloadOff)
            {
                reloadState = ReloadState.ReloadOn;
                playerAnim.SetLayerWeight(attackLayerIndex, 1.0f);
                playerAnim.SetInteger(PlayerAnimParameters.Reload.ToString(), 1);
            }
        } 
    }
    protected override void commonskillAttack1(CharactorJobEnum _type)
    {
        if (_type == CharactorJobEnum.Gunner)
        {
            if (skillCheck == SkillRunning.SkillOff)
            {
                skillStrategy.Skill(playerType, 1, out attackValue);
                skillCheck = SkillRunning.SkillOn;
                playerAnim.SetInteger("Skill1", 1);
                playerAnim.SetInteger(PlayerAnimName.BuffSkill.ToString(), 1);
                Invoke("SkillValueReset", 3);//clear
                Debug.Log($"attackValue = {attackValue}");
            }
            else
            {
                return;
            }
        }
        else { return; }
    }
    protected override void commonskillAttack2(CharactorJobEnum _type)
    {
        if (_type == CharactorJobEnum.Gunner)
        {
            if (skillCheck == SkillRunning.SkillOff)
            {
                skillStrategy.Skill(playerType, 2, out attackValue);
                skillCheck = SkillRunning.SkillOn;
                playerAnim.SetInteger("Skill1", 1);
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
        if (cameraMode == PlayerCameraMode.CameraRotationMode)
        {
            cameraMode = PlayerCameraMode.GunAttackMode;
            viewcam.CameraModeInit(cameraMode);
            viewcam.ShootModePosition();
        }
        else if (cameraMode == PlayerCameraMode.GunAttackMode)
        {
            cameraMode = PlayerCameraMode.CameraRotationMode;
            viewcam.CameraModeInit(cameraMode);
        }
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
