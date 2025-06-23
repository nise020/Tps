using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Gunner : Player
{
    
    private void LateUpdate()
    {
        if (!forceUpperBody) return;

        cachedUpperBodyEulerCurrent = Quaternion.Slerp(
            cachedUpperBodyEulerCurrent,
            cachedUpperBodyEulerTarget,
            Time.deltaTime * 15f // 숫자가 클수록 빠르게 따라감
        );

        UpperBody.localRotation = cachedUpperBodyEulerCurrent;
    }
    public override void AnimationOut(string _type) //AnimationEvent
    {
        if (_type == null)
        {
            Debug.LogError($"_type 값의 해당하는 애니메이션이 아닙니다");
            return;
        }

        if (_type == PlayerAnimParameters.Avoidance.ToString())
        {
            playerAnimator.SetInteger(PlayerAnimParameters.Avoidance.ToString(), 0);

            playerStateData.avoidanceState = AvoidanceState.Avoidance_Off;
        }
        else if (_type == PlayerAnimParameters.Dash.ToString())
        {
            playerAnimator.speed = 1f;
            playerStateData.WalkState = PlayerWalkState.Run;
            WalkStateAnimation(playerStateData.WalkState);
        }
        else if (_type == PlayerAnimParameters.Block.ToString())
        {
            battelTriggerCol.enabled = false;

            playerStateData.AttackState = AttackState.Attack_Off;
            attackAnimation(playerStateData.AttackState, 0);
        }
    }
    public override void AnimationStart(string _type) //AnimationEvent
    {

        if (_type == "")
        {
            Debug.LogError($"_type 값의 해당하는 애니메이션이 아닙니다");
            return;
        }
        if (_type == "Attack")
        {
            playerAnimator.SetInteger(PlayerAnimParameters.Attack.ToString(), 0);
        }
        else if (_type == "ComboAttack")
        {
            playerAnimator.SetInteger(PlayerAnimParameters.AttackCombo.ToString(), 0);
        }
        else if (_type == PlayerAnimParameters.Avoidance.ToString())
        {

            AnimatorStateInfo info = playerAnimator.GetCurrentAnimatorStateInfo(0);
            float animLength = info.length / playerAnimator.speed;
            float normalizedTime = info.normalizedTime % 1f;

            float remainingTime = animLength * (1f - normalizedTime);

            StartCoroutine(BackAvoid(gameObject, remainingTime));
        }
        else if (_type == PlayerAnimParameters.Dash.ToString())
        {
        }
    }
    protected override IEnumerator AdjustUpperBodyToTargetLoop(Gun gun)
    {
        while (true)
        {
            if (playerStateData.AttackState != AttackState.Attack_On || targetTrs == null)
            {
                yield return null;
                continue;
            }

            Transform gunHoleTrs = gun.GunHoleObj.transform;

            Vector3 toTarget = (targetTrs.position - UpperBody.position).normalized;

            Vector3 toTargetLocal = gunHoleTrs.InverseTransformDirection(toTarget);

            float pitchAngle = -Mathf.Atan2(toTargetLocal.y, toTargetLocal.z) * Mathf.Rad2Deg;

            float clampedPitch = Mathf.Clamp(pitchAngle, -60f, 60f);

            float pitchOffset = 20f; 

            cachedUpperBodyEulerTarget = Quaternion.Euler(pitchOffset + clampedPitch, 0, 0);

            forceUpperBody = true;

            yield return null; 
        }

    }

    public void GunShootEvent() //Animation Event
    {
        Gun gun = MAINWEAPON as Gun;

        if (gun == null) return;
        gun.StateUpdate(GunState.Off);
        gunShoot(gun);
    }
    private void gunShoot(Gun _gun) //Animation Event
    {
        Transform gunHoleTrs = _gun.GunHoleObj.transform;

        Vector3 aimDirection = gunHoleTrs.forward;

        float recoilAmount = 0.01f;
        Vector3 recoil = new Vector3(
            Random.Range(-recoilAmount, recoilAmount),
            Random.Range(-recoilAmount, recoilAmount),
            0f
        );

        aimDirection += _gun.GunHoleObj.transform.TransformDirection(recoil);
        aimDirection.Normalize();

        MAINWEAPON.Attack(aimDirection);
    }
    public void GunAttackAnimationOut() //AnimationEvent
    {
        if (MAINWEAPON.ReturnTypeValue(BulletValueType.NowBullet) <= 0) 
        {
            playerStateData.AttackState = AttackState.Attack_Off;
            attackAnimation(playerStateData.AttackState, 0);
            playerAnimator.SetLayerWeight(attackLayerIndex, 0.0f);

            StopCoroutine(AdjustUpperBodyToTargetLoop(null)) ;
            forceUpperBody = false;
        }
        if (PLAYERAI.AtttackCheck())//사망확인
        {
            StopCoroutine(UpperBodyColutin);
            UpperBodyColutin = null;

            UpperBody.localRotation = Quaternion.identity;

            playerStateData.AttackState = AttackState.Attack_Off;
            attackAnimation(playerStateData.AttackState, 0);
            playerAnimator.SetLayerWeight(attackLayerIndex, 0.0f);

            StopCoroutine(AdjustUpperBodyToTargetLoop(null));
            forceUpperBody = false;
        }
    }
    protected override void walkAnim(PlayerWalkState _state, Vector3 _pos)
    {
        if (_pos.x == 1)//rigrt
        {
            playerAnimator.SetInteger(PlayerAnimParameters.Right.ToString(), (int)_pos.x);
        }
        else if (_pos.x == -1)//left
        {
            playerAnimator.SetInteger(PlayerAnimParameters.Left.ToString(), (int)_pos.x);
        }
        else if (_pos.z == 1)//front
        {
            if (_state == PlayerWalkState.Walk)
            {
                playerStateData.WalkState = PlayerWalkState.Walk;
                playerAnimator.SetInteger(PlayerAnimParameters.Walk.ToString(), (int)_pos.z);
            }
            else if (_state == PlayerWalkState.Run)
            {
                playerStateData.WalkState = PlayerWalkState.Run;
                playerAnimator.SetInteger(PlayerAnimParameters.Run.ToString(), (int)_pos.z);
            }
        }
        else if (_pos.z == -1)//back
        {
            playerAnimator.SetInteger(PlayerAnimParameters.Back.ToString(), (int)_pos.z);
        }
    }

    protected override void clearWalkAnimation()
    {
        playerAnimator.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
        playerAnimator.SetInteger(PlayerAnimParameters.Back.ToString(), 0);
        playerAnimator.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
        playerAnimator.SetInteger(PlayerAnimParameters.Right.ToString(), 0);
        playerAnimator.SetInteger(PlayerAnimParameters.Left.ToString(), 0);
    }
    public void GunSkillShoot() 
    {
        Vector3 AimDirection = MainWeaponObj.transform.forward;
        MAINWEAPON.Attack(AimDirection);
        SkillAnimation(SkillType.Skill1, false);
        Invoke("skillOut", 1);
    }
    public void skillOut() 
    {
        SkillParentObj1.SetActive(false);
        SkillEffectSystem1.Stop();
    }
}
