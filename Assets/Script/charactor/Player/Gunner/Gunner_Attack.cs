using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public partial class Gunner : Player
{
    [SerializeField] Granad granadObj;
    protected override void attackMovement()
    {
        attackAnimation(PlayerAttackState.Attack_On);
        //gunShoot();
    }
    protected override void attack(PlayerModeState _state, PlayerType _job)
    {
        if (_job == PlayerType.Gunner)
        {
            if (playerStateData.AttackState == PlayerAttackState.Reload_Off && 
                WEAPON.ReturnTypeValue(BulletValueType.NowBullet) <= 0)
            {
                Debug.Log($"bullet = {WEAPON.ReturnTypeValue(BulletValueType.NowBullet)}");
                return;
            }
            playerAnimtor.SetLayerWeight(attackLayerIndex, 1.0f);
            attackAnimation(PlayerAttackState.Attack_On);
        }
    }
    public void GunShootEvent() //Animation Event
    {
        gunShoot();
    }
    private void gunShoot() 
    {
        Vector3 AimDirection = weaponObj.transform.forward;

        WEAPON.Attack(AimDirection);

        //playerAnim.SetLayerWeight(attackLayerIndex, 0.0f);
    }
    //protected override void inPutCameraAnimation(bool _check)
    //{
    //    viewcam.cameraShakeAnim(_check);
    //    //if (_check)
    //    //{
    //    //    viewcam.cameraShakeAnim(_check);
    //    //}
    //    //else 
    //    //{
    //    //    viewcam.cameraShakeAnim(_check)
    //    //}
    //    //if (_type == MouseInputType.Hold) 
    //    //{
    //    //    viewcam.cameraShakeAnim(_check);
    //    //}
    //}
    protected override void commonRSkill(PlayerType _type)//Reload
    {
        if (_type == PlayerType.Gunner ||
            WEAPON.ReturnTypeValue(BulletValueType.NowBullet) <= 0) 
        {
            if (playerStateData.AttackState == PlayerAttackState.Reload_Off)
            {
                playerStateData.AttackState = PlayerAttackState.Reload_On;
                playerAnimtor.SetLayerWeight(attackLayerIndex, 1.0f);
                playerAnimtor.SetInteger(PlayerAnimParameters.Reload.ToString(), 1);
            }
        }


        if (playerStateData.firstSkillCheck == SkillState.SkillOff)
        {
            gunShoot();

            SkillAnimation(SkillType.Skill1, true);

            //firstSkillCheck = SkillRunning.SkillOn;

            //playerAnim.SetInteger(SkillType.Skill1.ToString(), 1);
            int value = (int)atkValue;
            skillStrategy.Skill(playerStateData.PlayerType, 1, out value);

            SkillParentObj1.SetActive(true);

            //Vector3 forward = weaponObj.transform.TransformDirection(Vector3.down);
            //Vector3 up = weaponObj.transform.TransformDirection(Vector3.up);
            //Quaternion rot = Quaternion.LookRotation(forward, up);
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
    protected override void skillAttack_common1(PlayerType _type)
    {
        if (_type == PlayerType.Gunner)
        {
            if (playerStateData.firstSkillCheck == SkillState.SkillOff)
            {
                //Invoke("SkillValueReset", 3);//clear
                int value = (int)atkValue;

                Debug.Log($"attackValue = {value}");

                SkillAnimation(SkillType.Skill1, true);

                //firstSkillCheck = SkillRunning.SkillOn;

                //playerAnim.SetInteger(SkillType.Skill1.ToString(), 1);

                skillStrategy.Skill(playerStateData.PlayerType, 1, out value);

                SkillParentObj1.SetActive(true);

                //Vector3 forward = weaponObj.transform.TransformDirection(Vector3.down);
                //Vector3 up = weaponObj.transform.TransformDirection(Vector3.up);
                //Quaternion rot = Quaternion.LookRotation(forward, up);
                //Quaternion localRot = Quaternion.Inverse(weaponObj.transform.rotation) * rot;

                SkillEffectObj1.transform.localRotation = Quaternion.identity;

                SkillEffectSystem1.Play();


            }
            else
            {
                return;
            }
        }
        else { return; }
    }
    protected override void skillAttack_common2(PlayerType _type)
    {
        if (_type == PlayerType.Gunner)
        {
            if (playerStateData.firstSkillCheck == SkillState.SkillOff)
            {
                int value = (int)atkValue;

                skillStrategy.Skill(playerStateData.PlayerType, 2, out value);
                playerStateData.firstSkillCheck = SkillState.SkillOn;
                playerAnimtor.SetInteger("Skill1", 1);
                playerAnimtor.SetInteger(PlayerAnimName.BuffSkill.ToString(), 1);
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
    protected override void skillValueReset()//Damage Reset
    {
        atkValue = attackReset;
        playerStateData.firstSkillCheck = SkillState.SkillOff;
        playerAnimtor.SetInteger(PlayerAnimName.AttackSkill.ToString(), 0);
        playerAnimtor.SetInteger(PlayerAnimName.BuffSkill.ToString(), 0);
    }
    public void ReloadOut()//AnimationEvent
    {
        //AnimationEvent
        playerStateData.AttackState = PlayerAttackState.Reload_On;
        WEAPON.ReloadClearValue();
        playerAnimtor.SetLayerWeight(attackLayerIndex, 0.0f);
        //playerAnim.SetLayerWeight(BaseLayerIndex, 1.0f);
        playerAnimtor.SetInteger(PlayerAnimParameters.Reload.ToString(), 0);
    }
}
