using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public partial class Gunner : Player
{
    [SerializeField] Granad granadObj;
    protected override void attack(CharctorStateEnum _state, CharactorJobEnum _job)
    {
        if (_job == CharactorJobEnum.Gunner)
        {
            if (reloadState == ReloadState.ReloadOff && 
                WEAPON.ReturnTypeValue(BulletValueType.NowBullet) <= 0)
            {
                Debug.Log($"bullet = {WEAPON.ReturnTypeValue(BulletValueType.NowBullet)}");
                return;
            }
            gunShoot();
        }
    }
    private void gunShoot() 
    {
        playerAnim.SetLayerWeight(attackLayerIndex, 1.0f);

        attackAnimation(AttackState.AttackOn);

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
    protected override void commonRSkill(CharactorJobEnum _type)//Reload
    {
        if (_type == CharactorJobEnum.Gunner ||
            WEAPON.ReturnTypeValue(BulletValueType.NowBullet) <= 0) 
        {
            if (reloadState == ReloadState.ReloadOff)
            {
                reloadState = ReloadState.ReloadOn;
                playerAnim.SetLayerWeight(attackLayerIndex, 1.0f);
                playerAnim.SetInteger(PlayerAnimParameters.Reload.ToString(), 1);
            }
        }


        if (firstSkillCheck == SkillState.SkillOff)
        {
            gunShoot();

            SkillAnimation(SkillType.Skill1, true);

            //firstSkillCheck = SkillRunning.SkillOn;

            //playerAnim.SetInteger(SkillType.Skill1.ToString(), 1);
            int value = (int)atkValue;
            skillStrategy.Skill(playerType, 1, out value);

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
    protected override void commonskillAttack1(CharactorJobEnum _type)
    {
        if (_type == CharactorJobEnum.Gunner)
        {
            if (firstSkillCheck == SkillState.SkillOff)
            {
                //Invoke("SkillValueReset", 3);//clear
                int value = (int)atkValue;

                Debug.Log($"attackValue = {value}");

                SkillAnimation(SkillType.Skill1, true);

                //firstSkillCheck = SkillRunning.SkillOn;

                //playerAnim.SetInteger(SkillType.Skill1.ToString(), 1);

                skillStrategy.Skill(playerType, 1, out value);

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
    protected override void commonskillAttack2(CharactorJobEnum _type)
    {
        if (_type == CharactorJobEnum.Gunner)
        {
            if (firstSkillCheck == SkillState.SkillOff)
            {
                int value = (int)atkValue;

                skillStrategy.Skill(playerType, 2, out value);
                firstSkillCheck = SkillState.SkillOn;
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
    protected override void skillValueReset()//Damage Reset
    {
        atkValue = attackReset;
        firstSkillCheck = SkillState.SkillOff;
        playerAnim.SetInteger(PlayerAnimName.AttackSkill.ToString(), 0);
        playerAnim.SetInteger(PlayerAnimName.BuffSkill.ToString(), 0);
    }
    public void ReloadOut()//AnimationEvent
    {
        //AnimationEvent
        reloadState = ReloadState.ReloadOff;
        WEAPON.ReloadClearValue();
        playerAnim.SetLayerWeight(attackLayerIndex, 0.0f);
        //playerAnim.SetLayerWeight(BaseLayerIndex, 1.0f);
        playerAnim.SetInteger(PlayerAnimParameters.Reload.ToString(), 0);
    }
}
