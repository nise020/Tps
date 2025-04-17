using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Warrior : Player
{
    [SerializeField] GameObject SkillEffectObj1;
    [SerializeField] GameObject SkillEffectObj2;
    GameObject SkillObj1 = null;
    GameObject SkillObj2 = null;
    protected override void attack(CharctorStateEnum _state, CharactorJobEnum _job)
    {
        if (_state == CharctorStateEnum.Player)
        {
            if (weaponState == WeaponState.Sword_Off) 
            {
                playerAnim.SetInteger(PlayerAnimParameters.GetWeapon.ToString(), 1);
            }
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
            if (firstSkillCheck == SkillRunning.SkillOff)
            {
                if (weaponState == WeaponState.Sword_On) 
                {
                    GetSword();
                }
                skillStrategy.Skill(playerType, 1,out attackValue);
                firstSkillCheck = SkillRunning.SkillOn;

                playerAnim.SetInteger(SkillType.Skill1.ToString(), 1);
                SkillObj1.SetActive(true);
                //SkillObj.transform.SetParent(weapon.gameObject.transform);
                //SkillObj.transform.localPosition = Vector3.zero;

                //playerAnim.SetInteger(PlayerAnimName.AttackSkill.ToString(), 1);
                //Invoke("SkillValueReset", 3);//clear
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
            if (secondSkillCheck == SkillRunning.SkillOff)
            {
                skillStrategy.Skill(playerType, 2, out attackValue);
                secondSkillCheck = SkillRunning.SkillOn;

                playerAnim.SetInteger(SkillType.Skill2.ToString(), 1);

                SkillEffectObj2.transform.SetParent(weapon.gameObject.transform);
                SkillEffectObj2.transform.localPosition = Vector3.zero;

                //playerAnim.SetInteger(PlayerAnimName.BuffSkill.ToString(), 1);
                //Invoke("SkillValueReset", 3);//clear
            }
            else
            {
                return;
            }
        }
        else { return; }
    }
    protected override void cameraModeChange()//수정 필요
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
        if (firstSkillCheck == SkillRunning.SkillOn) 
        {
            playerAnim.SetInteger(SkillType.Skill1.ToString(), 0);
            firstSkillCheck = SkillRunning.SkillOff;
        }
        if (secondSkillCheck == SkillRunning.SkillOn)
        {
            playerAnim.SetInteger(SkillType.Skill2.ToString(), 0);
            secondSkillCheck = SkillRunning.SkillOff;
        }
        attackValue = attackReset;
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
