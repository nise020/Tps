using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public partial class Warrior : Player
{
    int currentCombo = 0;
    
    public void RangeCheak()
    {
        Vector3 weaponPos = new Vector3();

        if (playerStateData.firstSkillCheck == SkillState.SkillOn)
        {
            weaponPos = SkillParentObj1.transform.position;
        }
        else if (playerStateData.secondSkillCheck == SkillState.SkillOn)
        {
            weaponPos = SkillParentObj2.transform.position;
        }
        else
        {
            weaponPos = weaponObj.transform.position;
        }

        List<Monster> monsetrPos = Shared.MonsterManager.MonsterList;

        for (int iNum = 0; iNum < monsetrPos.Count; iNum++)
        {
            Transform body = monsetrPos[iNum].BodyObjectLoad();
            float dist = Vector3.Distance(weaponPos, body.position);

            if (dist < 3.5f)
            {
                Shared.BattelManager.DamageCheck(this, monsetrPos[iNum]);
            }
        }
    }

    public void SkillEffectOff(int _value)
    {
        if (playerStateData.firstSkillCheck == SkillState.SkillOn)
        {
            SkillAnimation(SkillType.Skill1, false);
            SkillParentObj1.SetActive(false);
            SkillEffectSystem1.Pause();
        }
        else if (playerStateData.secondSkillCheck == SkillState.SkillOn)
        {
            SkillAnimation(SkillType.Skill2, false);
            SkillParentObj1.SetActive(false);
            SkillEffectSystem2.Pause();
        }
        else
        {
            attackAnimation(PlayerAttackState.Attack_Off);
        }
        playerStateData.WeaponState = PlayerWeaponState.Sword_Off;
        scabbardCount = 0;

    }
    protected override void AutoAttack()
    {
        attackAnimation(PlayerAttackState.Attack_On);
    }
    protected override void attack(PlayerModeState _state, PlayerType _job)
    {
        if (_state == PlayerModeState.Player)
        {
            if (playerStateData.WeaponState == PlayerWeaponState.Sword_Off)//Open Weapon
            {
                StartCoroutine(getSwordEvent(1.5f,0.10f));//speed,event
                return;
            }

            if (playerStateData.WeaponState == PlayerWeaponState.Sword_On &&
                playerStateData.AttackState == PlayerAttackState.Attack_Off)//attack 
            {
                playerStateData.AttackState = PlayerAttackState.Attack_On;

                attackAnimation(PlayerAttackState.Attack_On);
                playerAnimtor.speed = 1.5f;

                canReceiveInput = true;
            }
            else if (canReceiveInput == true && currentCombo < 2)
            {
                currentCombo++;
                canReceiveInput = false;
                scabbardCount = 0;//idel state count Reset
                //playerAnimtor.SetInteger(PlayerAnimParameters.AttackCombo.ToString(), currentCombo);
                //return;
            }

        }
    }
    IEnumerator getSwordEvent(float _animationSpeed,float _event) 
    {

        playerAnimtor.SetInteger(PlayerAnimParameters.GetWeapon.ToString(), 1);

        float originalTimeToAttach = _event; //Event Time

        float delay = originalTimeToAttach / _animationSpeed;

        yield return new WaitForSeconds(delay);

        GetSword();
    }

    IEnumerator PlayAttackCombo()
    {
        currentCombo = 1;
        canReceiveInput = true;
        Debug.Log($"currentCombo = {currentCombo}");

        while (currentCombo <= 3)
        {
            //playerAnimtor.speed = 1.5f;
            playerAnimtor.SetInteger(PlayerAnimParameters.AttackCombo.ToString(), currentCombo);
            Debug.Log($"currentCombo = {currentCombo}");


            float clipLength = GetClipLength($"Attack_Combo_Weapon_{currentCombo}");

            if (clipLength <= 0f)
            {
                Debug.LogWarning("해당 클립을 찾지 못했습니다.");
                break;
            }

            float adjustedTime = clipLength / playerAnimtor.speed;

            
            yield return new WaitForSeconds(adjustedTime * 0.7f);
            canReceiveInput = true;


            float inputWindow = adjustedTime * 0.3f;
            float timer = 0f;

            while (timer < inputWindow)
            {
                if (!canReceiveInput) break; // 입력 들어옴
                timer += Time.deltaTime;
                yield return null;
            }

            canReceiveInput = false;

            if (currentCombo >= 3 || timer >= inputWindow)
            {
                break; // 더 이상 콤보 없음
            }
        }

        playerAnimtor.SetInteger(PlayerAnimParameters.AttackCombo.ToString(), 0);
        playerAnimtor.speed = 1.0f;
        currentCombo = 0;
        playerStateData.AttackState = PlayerAttackState.Attack_Off;
    }
    float GetClipLength(string clipName)
    {
        foreach (AnimationClip clip in playerAnimtor.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
                Debug.Log($"clip.length = {clip.length}");
                return clip.length;
        }
        return 0f;
    }
    protected override void commonRSkill(PlayerType _type)
    {
        if (_type == PlayerType.Warrior) 
        {

        }

    }
    protected override void skillAttack_common1(PlayerType _type)
    {
        if (_type == PlayerType.Warrior)
        {
            if (playerStateData.firstSkillCheck == SkillState.SkillOff)
            {
                if (playerStateData.WeaponState == PlayerWeaponState.Sword_Off) 
                {
                    playerStateData.WeaponState = PlayerWeaponState.Sword_On;
                    playerAnimtor.SetInteger(PlayerAnimParameters.GetWeapon.ToString(), 1);
                }
                SkillAnimation(SkillType.Skill1, true);
                //firstSkillCheck = SkillRunning.SkillOn;

                //playerAnim.SetInteger(SkillType.Skill1.ToString(), 1);

                int value = (int)atkValue;

                skillStrategy.Skill(playerStateData.PlayerType, 1,out value);

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
    protected override void skillAttack_common2(PlayerType _type)
    {
        if (_type == PlayerType.Warrior)
        {
            if (playerStateData.secondSkillCheck == SkillState.SkillOff)
            {
                if (playerStateData.WeaponState == PlayerWeaponState.Sword_Off)
                {
                    playerStateData.WeaponState = PlayerWeaponState.Sword_On;
                    playerAnimtor.SetInteger(PlayerAnimParameters.GetWeapon.ToString(), 1);
                }
                SkillAnimation(SkillType.Skill2, true);
                //secondSkillCheck = SkillRunning.SkillOn;
                //playerAnim.SetInteger(SkillType.Skill1.ToString(), 1);

                int value = (int)atkValue;

                skillStrategy.Skill(playerStateData.PlayerType, 2, out value);

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
        if (playerStateData.firstSkillCheck == SkillState.SkillOn) 
        {
            playerAnimtor.SetInteger(SkillType.Skill1.ToString(), 0);
            playerStateData.firstSkillCheck = SkillState.SkillOff;
        }
        if (playerStateData.secondSkillCheck == SkillState.SkillOn)
        {
            playerAnimtor.SetInteger(SkillType.Skill2.ToString(), 0);
            playerStateData.secondSkillCheck = SkillState.SkillOff;
        }
        atkValue = attackReset;
    }

}
