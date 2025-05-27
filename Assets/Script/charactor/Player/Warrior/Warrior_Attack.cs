using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public partial class Warrior : Player
{
    int currentCombo = 0;
    bool isComboPossible = false;
    public void RangeCheak()
    {
        Vector3 weaponPos = new Vector3();

        if (PlayerStateData.firstSkillCheck == SkillState.SkillOn)
        {
            weaponPos = SkillParentObj1.transform.position;
        }
        else if (PlayerStateData.secondSkillCheck == SkillState.SkillOn)
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
        if (PlayerStateData.firstSkillCheck == SkillState.SkillOn)
        {
            SkillAnimation(SkillType.Skill1, false);
            SkillParentObj1.SetActive(false);
            SkillEffectSystem1.Pause();
        }
        else if (PlayerStateData.secondSkillCheck == SkillState.SkillOn)
        {
            SkillAnimation(SkillType.Skill2, false);
            SkillParentObj1.SetActive(false);
            SkillEffectSystem2.Pause();
        }
        else
        {
            attackAnimation(PlayerAttackState.Attack_Off);
        }
        PlayerStateData.WeaponState = PlayerWeaponState.Sword_Off;
        scabbardCount = 0;

    }
    protected override void attackMovement()
    {
        attackAnimation(PlayerAttackState.AttackOn);
    }
    protected override void attack(PlayerModeState _state, PlayerType _job)
    {
        if (_state == PlayerModeState.Player)
        {
            if (PlayerStateData.WeaponState == PlayerWeaponState.Sword_Off)//Open Weapon
            {
                StartCoroutine(getSwordEvent(1.5f,0.10f));//speed,event
            }

            if (isComboPossible && currentCombo < 3)
            {
                currentCombo++;
                isComboPossible = false;
                return;
            }

            if (PlayerStateData.WeaponState == PlayerWeaponState.Sword_On&&
                PlayerStateData.AttackState == PlayerAttackState.Attack_Off)//attack 
            {
                PlayerStateData.AttackState = PlayerAttackState.AttackOn;
                StartCoroutine(PlayAttackCombo());
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
        isComboPossible = true;
        Debug.Log($"currentCombo = {currentCombo}");

        while (currentCombo <= 3)
        {
            //playerAnimtor.speed = 1.5f;
            playerAnimtor.SetInteger(PlayerAnimParameters.AttackCombo.ToString(), currentCombo);

            yield return new WaitForSeconds(0.2f);

            isComboPossible = true;

            float timer = 0f;

            while (timer < 0.5f)
            {
                if (!isComboPossible) break; // 입력 들어오면 빠져나감
                timer += Time.deltaTime;
                yield return null;
            }

            isComboPossible = false;

            if (timer >= 0.5f)
            {
                break;
            }
        }

        playerAnimtor.SetInteger(PlayerAnimParameters.AttackCombo.ToString(), 0);
        playerAnimtor.speed = 1.0f;
        currentCombo = 0;
        PlayerStateData.AttackState = PlayerAttackState.Attack_Off;
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
            if (PlayerStateData.firstSkillCheck == SkillState.SkillOff)
            {
                if (PlayerStateData.WeaponState == PlayerWeaponState.Sword_Off) 
                {
                    PlayerStateData.WeaponState = PlayerWeaponState.Sword_On;
                    playerAnimtor.SetInteger(PlayerAnimParameters.GetWeapon.ToString(), 1);
                }
                SkillAnimation(SkillType.Skill1, true);
                //firstSkillCheck = SkillRunning.SkillOn;

                //playerAnim.SetInteger(SkillType.Skill1.ToString(), 1);

                int value = (int)atkValue;

                skillStrategy.Skill(PlayerStateData.PlayerType, 1,out value);

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
            if (PlayerStateData.secondSkillCheck == SkillState.SkillOff)
            {
                if (PlayerStateData.WeaponState == PlayerWeaponState.Sword_Off)
                {
                    PlayerStateData.WeaponState = PlayerWeaponState.Sword_On;
                    playerAnimtor.SetInteger(PlayerAnimParameters.GetWeapon.ToString(), 1);
                }
                SkillAnimation(SkillType.Skill2, true);
                //secondSkillCheck = SkillRunning.SkillOn;
                //playerAnim.SetInteger(SkillType.Skill1.ToString(), 1);

                int value = (int)atkValue;

                skillStrategy.Skill(PlayerStateData.PlayerType, 2, out value);

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
        if (PlayerStateData.firstSkillCheck == SkillState.SkillOn) 
        {
            playerAnimtor.SetInteger(SkillType.Skill1.ToString(), 0);
            PlayerStateData.firstSkillCheck = SkillState.SkillOff;
        }
        if (PlayerStateData.secondSkillCheck == SkillState.SkillOn)
        {
            playerAnimtor.SetInteger(SkillType.Skill2.ToString(), 0);
            PlayerStateData.secondSkillCheck = SkillState.SkillOff;
        }
        atkValue = attackReset;
    }

}
