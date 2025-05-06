using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public partial class Warrior : Player
{
    
    protected override void attack(CharctorStateEnum _state, CharactorJobEnum _job)
    {
        if (_state == CharctorStateEnum.Player)
        {
            if (weaponState == WeaponState.Sword_Off)//Open Weapon
            {
                weaponState = WeaponState.Sword_On;
                playerAnim.SetInteger(PlayerAnimParameters.GetWeapon.ToString(), 1);
            }
            else if (weaponState == WeaponState.Sword_On)//attack 
            {
                attackAnimation(AttackState.AttackOn);
            }  
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
            if (firstSkillCheck == SkillState.SkillOff)
            {
                if (weaponState == WeaponState.Sword_Off) 
                {
                    weaponState = WeaponState.Sword_On;
                    playerAnim.SetInteger(PlayerAnimParameters.GetWeapon.ToString(), 1);
                }
                SkillAnimation(SkillType.Skill1, true);
                //firstSkillCheck = SkillRunning.SkillOn;

                //playerAnim.SetInteger(SkillType.Skill1.ToString(), 1);

                int value = (int)atkValue;

                skillStrategy.Skill(playerType, 1,out value);

                SkillParentObj1.SetActive(true);

                //Vector3 forward = weaponObj.transform.TransformDirection(Vector3.down);
                //Vector3 up = weaponObj.transform.TransformDirection(Vector3.up);
                //Quaternion rot = Quaternion.LookRotation (forward, up);
                //Quaternion localRot = Quaternion.Inverse(weaponObj.transform.rotation) * rot;

                SkillEffectObj1.transform.localRotation = Quaternion.identity;

                SkillEffectSystem1.Play();

                //Transform effect = SkillEffectObj1.transform;
                //Transform effectParent = effect.parent; // Skill_1
                //Transform sword = effectParent.parent;  // sword

                //Vector3 desiredForward = sword.TransformDirection(- Vector3.up);     // 검날 방향
                //Vector3 desiredUp = sword.TransformDirection(Vector3.up);      // 위쪽

                //Quaternion worldRotation = Quaternion.LookRotation(desiredForward, desiredUp);
                //Quaternion localRotation = Quaternion.Inverse(effectParent.rotation) * worldRotation;

                //effect.localRotation = localRotation;
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
        if (_type == CharactorJobEnum.Warrior)
        {
            if (secondSkillCheck == SkillState.SkillOff)
            {
                if (weaponState == WeaponState.Sword_Off)
                {
                    weaponState = WeaponState.Sword_On;
                    playerAnim.SetInteger(PlayerAnimParameters.GetWeapon.ToString(), 1);
                }
                SkillAnimation(SkillType.Skill2, true);
                //secondSkillCheck = SkillRunning.SkillOn;
                //playerAnim.SetInteger(SkillType.Skill1.ToString(), 1);

                int value = (int)atkValue;

                skillStrategy.Skill(playerType, 2, out value);

                SkillParentObj2.SetActive(true);

                SkillEffectObj2.transform.rotation = Quaternion.LookRotation(weaponObj.transform.up);

                SkillEffectSystem2.Play();
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

    protected override void skillValueReset()//Damage Reset
    {
        if (firstSkillCheck == SkillState.SkillOn) 
        {
            playerAnim.SetInteger(SkillType.Skill1.ToString(), 0);
            firstSkillCheck = SkillState.SkillOff;
        }
        if (secondSkillCheck == SkillState.SkillOn)
        {
            playerAnim.SetInteger(SkillType.Skill2.ToString(), 0);
            secondSkillCheck = SkillState.SkillOff;
        }
        atkValue = attackReset;
    }

}
