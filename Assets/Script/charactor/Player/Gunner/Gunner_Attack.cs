using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public partial class Gunner : Player
{
    protected override void AutoAttack(Transform _transform)
    {
        //targetTrs = _transform;
        if (playerStateData.AttackState != AttackState.Attack_Off) 
        {
            return;
        }

        playerAnimator.SetLayerWeight(attackLayerIndex, 1.0f);

        if (MAINWEAPON.ReturnTypeValue(BulletValueType.NowBullet) <= 0)
        {
            playerStateData.AttackState = AttackState.Reload;
        }
        else 
        {
            playerStateData.AttackState = AttackState.Attack_On;
        }

        attackAnimation(playerStateData.AttackState, 0);

        //skillAttack_common1(playerStateData.PlayerType);

        //skillAttack_common2(playerStateData.PlayerType);

        //attackAnimation(PlayerAttackState.Attack_On);
        //gunShoot();
    }
    protected override void attack(PlayerModeState _state, PlayerType _job)
    {
        if (_job == PlayerType.Gunner)
        {
            if (playerStateData.AttackState == AttackState.Attack_Off && 
                MAINWEAPON.ReturnTypeValue(BulletValueType.NowBullet) <= 0)
            {
                Debug.Log($"bullet = {MAINWEAPON.ReturnTypeValue(BulletValueType.NowBullet)}");
                return;
            }
            playerStateData.AttackState = AttackState.Attack_On;

            playerAnimator.SetLayerWeight(attackLayerIndex, 1.0f);
            attackAnimation(playerStateData.AttackState, 0);
        }
    }

    private bool isAdjustingUpperBody = false;

    void OnDrawGizmos()
    {
        if (MAINWEAPON is Gun gun && gun.GunHoleObj != null && targetTrs != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(gun.GunHoleObj.transform.position, targetTrs.position);
        }
    }


    private Quaternion CalculateUpperBodyRotationToTarget()
    {
        Transform gunHoleTrs = (MAINWEAPON as Gun).GunHoleObj.transform;
        Vector3 toTarget = (targetTrs.position - gunHoleTrs.position).normalized;

        // 총구 기준 pitch 각도만 계산
        float pitch = Vector3.SignedAngle(gunHoleTrs.forward, toTarget, gunHoleTrs.right);
        float clampedPitch = Mathf.Clamp(pitch, -30f, 30f);

        // 현재 로컬 회전에서 x축만 덮어씀
        Vector3 currentEuler = UpperBody.localRotation.eulerAngles;
        return Quaternion.Euler(clampedPitch, currentEuler.y, currentEuler.z);
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
            MAINWEAPON.ReturnTypeValue(BulletValueType.NowBullet) <= 0) 
        {
            if (playerStateData.AttackState != AttackState.Reload)
            {
                playerStateData.AttackState = AttackState.Reload;

                playerAnimator.SetLayerWeight(attackLayerIndex, 1.0f);
                playerAnimator.SetInteger(playerStateData.AttackState.ToString(), 1);
            }
        }


        if (playerStateData.FirstSkillCheck == SkillState.SkillOff)
        {
            Gun gun = MAINWEAPON as Gun;
            gunShoot(gun);

            SkillAnimation(SkillType.Skill1, true);

            //firstSkillCheck = SkillRunning.SkillOn;

            //playerAnim.SetInteger(SkillType.Skill1.ToString(), 1);
            int value = (int)StatusData[StatusType.Power];
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
            if (playerStateData.FirstSkillCheck == SkillState.SkillOff)
            {
                //Invoke("SkillValueReset", 3);//clear
                int value = (int)StatusData[StatusType.Power];

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
            if (playerStateData.SecondSkillCheck == SkillState.SkillOff)
            {
                int value = (int)StatusData[StatusType.Power];

                skillStrategy.Skill(playerStateData.PlayerType, 2, out value);
                playerStateData.SecondSkillCheck = SkillState.SkillOn;
                playerAnimator.SetInteger("Skill1", 1);
                playerAnimator.SetInteger(PlayerAnimName.BuffSkill.ToString(), 1);
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
        StatusData[StatusType.Power] = attackReset;
        playerStateData.FirstSkillCheck = SkillState.SkillOff;
        playerAnimator.SetInteger(PlayerAnimName.AttackSkill.ToString(), 0);
        playerAnimator.SetInteger(PlayerAnimName.BuffSkill.ToString(), 0);
    }
    public void ReloadOut()//AnimationEvent
    {
        //AnimationEvent
        playerStateData.AttackState = AttackState.Attack_Off;

        playerAnimator.SetLayerWeight(attackLayerIndex, 0.0f);
        attackAnimation(playerStateData.AttackState, 0);

        MAINWEAPON.ReloadClearValue();
    }
}
