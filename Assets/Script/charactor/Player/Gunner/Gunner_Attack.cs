using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public partial class Gunner : Player
{
    protected override void AutoAttack(Transform _transform)
    {
        //targetTrs = _transform;
        if (playerStateData.AttackState != PlayerAttackState.Attack_Off) 
        {
            return;
        }

        playerAnimator.SetLayerWeight(attackLayerIndex, 1.0f);

        if (MAINWEAPON.ReturnTypeValue(BulletValueType.NowBullet) <= 0)
        {
            playerStateData.AttackState = PlayerAttackState.Reload;
        }
        else 
        {
            playerStateData.AttackState = PlayerAttackState.Attack_On;
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
            if (playerStateData.AttackState == PlayerAttackState.Attack_Off && 
                MAINWEAPON.ReturnTypeValue(BulletValueType.NowBullet) <= 0)
            {
                Debug.Log($"bullet = {MAINWEAPON.ReturnTypeValue(BulletValueType.NowBullet)}");
                return;
            }
            playerStateData.AttackState = PlayerAttackState.Attack_On;

            playerAnimator.SetLayerWeight(attackLayerIndex, 1.0f);
            attackAnimation(playerStateData.AttackState, 0);
        }
    }
    private void AdjustUpperBodyToTarget(Weapon weapon)
    {
        if (weapon == null) { return; }
        Gun gun = weapon as Gun;

        Transform gunHoleTrs = gun.GunHoleObj.transform;

        Vector3 toTarget = (targetTrs.position - gunHoleTrs.position).normalized;
        Vector3 gunForward = gunHoleTrs.forward;

        float pitchAngle = Vector3.SignedAngle(gunForward, toTarget, gunHoleTrs.right);
        float clampedPitch = Mathf.Clamp(pitchAngle, -maxPitch, maxPitch);

        Vector3 currentEuler = UpperBody.localEulerAngles;
        if (currentEuler.x > 180f) currentEuler.x -= 360f;

        float newX = Mathf.Lerp(currentEuler.x, currentEuler.x + clampedPitch, Time.deltaTime * UpperrotationSpeed);
        UpperBody.localEulerAngles = new Vector3(newX, currentEuler.y, currentEuler.z);
    }

    public IEnumerator AdjustUpperBodyToTargetOnce(Weapon weapon)
    {
        if (weapon == null) { yield break; }
        Gun gun = weapon as Gun;

        Transform gunHoleTrs = gun.GunHoleObj.transform;


        Vector3 toTarget = (targetTrs.position - gunHoleTrs.position).normalized;
        Vector3 gunForward = gunHoleTrs.forward;

        float pitchAngle = Vector3.SignedAngle(gunForward, toTarget, gunHoleTrs.right);
        float clampedPitch = Mathf.Clamp(pitchAngle, -maxPitch, maxPitch);

        Vector3 currentEuler = UpperBody.localEulerAngles;
        if (currentEuler.x > 180f) currentEuler.x -= 360f;

        float targetX = currentEuler.x + clampedPitch;
        float duration = 0.2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float newX = Mathf.Lerp(currentEuler.x, targetX, elapsed / duration);
            cachedUpperBodyEuler = new Vector3(newX, currentEuler.y, currentEuler.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        cachedUpperBodyEuler = new Vector3(targetX, currentEuler.y, currentEuler.z);
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
            if (playerStateData.AttackState != PlayerAttackState.Reload)
            {
                playerStateData.AttackState = PlayerAttackState.Reload;

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
            if (playerStateData.FirstSkillCheck == SkillState.SkillOff)
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
            if (playerStateData.SecondSkillCheck == SkillState.SkillOff)
            {
                int value = (int)atkValue;

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
        atkValue = attackReset;
        playerStateData.FirstSkillCheck = SkillState.SkillOff;
        playerAnimator.SetInteger(PlayerAnimName.AttackSkill.ToString(), 0);
        playerAnimator.SetInteger(PlayerAnimName.BuffSkill.ToString(), 0);
    }
    public void ReloadOut()//AnimationEvent
    {
        //AnimationEvent
        playerStateData.AttackState = PlayerAttackState.Attack_Off;

        playerAnimator.SetLayerWeight(attackLayerIndex, 0.0f);
        attackAnimation(playerStateData.AttackState, 0);

        MAINWEAPON.ReloadClearValue();
    }
}
