using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Gunner : Player
{
    
    private void LateUpdate()
    {
        //if (forceUpperBody && cachedUpperBodyEuler != null)
        //{
        //    UpperBody.localRotation = cachedUpperBodyEuler;
        //}

        if (!forceUpperBody) return;

        cachedUpperBodyEulerCurrent = Quaternion.Slerp(
            cachedUpperBodyEulerCurrent,
            cachedUpperBodyEulerTarget,
            Time.deltaTime * 15f // ���ڰ� Ŭ���� ������ ����
        );

        UpperBody.localRotation = cachedUpperBodyEulerCurrent;
    }
    public override void AnimationOut(string _type) //AnimationEvent
    {
        if (_type == null)
        {
            Debug.LogError($"_type ���� �ش��ϴ� �ִϸ��̼��� �ƴմϴ�");
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

            playerStateData.AttackState = PlayerAttackState.Attack_Off;
            attackAnimation(playerStateData.AttackState, 0);
        }
    }
    public override void AnimationStart(string _type) //AnimationEvent
    {

        if (_type == "")
        {
            Debug.LogError($"_type ���� �ش��ϴ� �ִϸ��̼��� �ƴմϴ�");
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
            //AnimationEventTiming(PlayerAnimParameters.Avoidance, 1.5f,0.10f);
        }
        else if (_type == PlayerAnimParameters.Dash.ToString())
        {
            //Debug.Log($"playerStateData.WalkState ={playerStateData.WalkState}");

            //if (dashCheck)
            //{
            //    dashCheck = false;
            //    StartCoroutine(dashMove());
            //}

            //playerStateData.WalkState = PlayerWalkState.Dash;
            //Debug.Log($"playerStateData.WalkState ={playerStateData.WalkState}");
        }
    }
    protected override IEnumerator AdjustUpperBodyToTargetLoop(Gun gun)
    {
        while (true)
        {
            if (playerStateData.AttackState != PlayerAttackState.Attack_On || targetTrs == null)
            {
                yield return null;
                continue;
            }

            Transform gunHoleTrs = gun.GunHoleObj.transform;

            // ��� ���� ���� ���
            Vector3 toTarget = (targetTrs.position - UpperBody.position).normalized;

            // gunHole ���� ���� ����
            Vector3 toTargetLocal = gunHoleTrs.InverseTransformDirection(toTarget);

            // Pitch ��� (Y�� ���Ʒ�)
            float pitchAngle = -Mathf.Atan2(toTargetLocal.y, toTargetLocal.z) * Mathf.Rad2Deg;

            // ���� ����
            float clampedPitch = Mathf.Clamp(pitchAngle, -60f, 60f);

            float pitchOffset = 20f; // Ȥ�� -10f

            // ���� ��ǥ ȸ���� ���� (Yaw�� 0���� ����)
            cachedUpperBodyEulerTarget = Quaternion.Euler(pitchOffset + clampedPitch, 0, 0);

            forceUpperBody = true;

            yield return null; // �� ������ ����
        }

        //while (true)
        //{
        //    if (playerStateData.AttackState != PlayerAttackState.Attack_On || targetTrs == null)
        //    {
        //        yield return null;
        //        continue;
        //    }

        //    Transform refTrs = UpperBody;

        //    Vector3 toTarget = (targetTrs.position - refTrs.position).normalized;
        //    Vector3 toTargetLocal = refTrs.InverseTransformDirection(toTarget);

        //    float pitchAngle = Mathf.Atan2(toTargetLocal.y, toTargetLocal.z) * Mathf.Rad2Deg;
        //    float clampedPitch = Mathf.Clamp(pitchAngle, -30f, 30f);

        //    cachedUpperBodyEuler = Quaternion.Euler(clampedPitch, 0, 0);
        //    forceUpperBody = true;

        //    yield return null;
        //}

        //while (true)
        //{
        //    if (playerStateData.AttackState != PlayerAttackState.Attack_On || targetTrs == null)
        //    {
        //        yield return null;
        //        continue;
        //    }

        //    Transform gunHoleTrs = gun.GunHoleObj.transform;
        //    Vector3 toTarget = (targetTrs.position - gunHoleTrs.position).normalized;
        //    Vector3 toTargetLocal = gunHoleTrs.InverseTransformDirection(toTarget);
        //    //Vector3 toTargetLocal = UpperBody.InverseTransformDirection(toTarget);

        //    float pitchAngle = -Mathf.Atan2(toTargetLocal.y, toTargetLocal.z) * Mathf.Rad2Deg;
        //    float clampedPitch = Mathf.Clamp(pitchAngle, -30f, 30f);

        //    cachedUpperBodyEulerTarget = Quaternion.Euler(clampedPitch, 0, 0);
        //    forceUpperBody = true;

        //    yield return null;
        //}



        //lastTargetPos = Vector3.zero; // �ʱ�ȭ

        //while (playerStateData.AttackState == PlayerAttackState.Attack_On)
        //{
        //    Vector3 currentTargetPos = Vector3.zero;

        //    if (targetTrs == null) 
        //    {
        //        Debug.LogWarning("[�ڷ�ƾ ����] targetTrs�� null�Դϴ�.");
        //        yield break;
        //    }
        //    if (targetTrs != null)
        //    {
        //        currentTargetPos = targetTrs.position;
        //        //Debug.Log($"���� ��ǥ�� ��ġ: {currentTargetPos}, ���� ��ġ: {lastTargetPos}");

        //        if ((currentTargetPos - lastTargetPos).sqrMagnitude > 0.01f)
        //        {
        //            Debug.Log("��ġ�� �����. ��ü ȸ�� ������Ʈ");
        //        }
        //        else
        //        {
        //            Debug.Log("��ġ ��ȭ ����. ȸ�� ����");
        //        }
        //    }

        //    //Vector3 currentTargetPos = targetTrs.position;

        //    // ��ǥ ��ġ�� �ٲ� ��츸 ���
        //    if ((lastTargetPos - currentTargetPos).sqrMagnitude > 0.01f)
        //    {
        //        lastTargetPos = currentTargetPos;

        //        Transform gunHoleTrs = gun.GunHoleObj.transform;
        //        Vector3 toTarget = (currentTargetPos - gunHoleTrs.position).normalized;
        //        Vector3 toTargetLocal = gunHoleTrs.InverseTransformDirection(toTarget);

        //        float pitchAngle = Mathf.Atan2(toTargetLocal.y, toTargetLocal.z) * Mathf.Rad2Deg;
        //        float clampedPitch = Mathf.Clamp(pitchAngle, -30f, 30f);

        //        Quaternion startRot = Quaternion.identity;
        //        //Quaternion targetRot = Quaternion.Euler(clampedPitch, startRot.eulerAngles.y, startRot.eulerAngles.z);
        //        Quaternion targetRot = Quaternion.Euler(clampedPitch, 0, 0);

        //        float elapsed = 0f;
        //        while (elapsed < duration)
        //        {
        //            cachedUpperBodyEuler = Quaternion.Slerp(startRot, targetRot, elapsed / duration);
        //            elapsed += Time.deltaTime;
        //            yield return null;
        //        }

        //        cachedUpperBodyEuler = targetRot;
        //    }

        //    yield return null;
        //}

        //UpperBodyColutin = null;
        //forceUpperBody = false;

        //// ���� ���� �� ȸ���� �ʱ�ȭ
        //targetUpperBodyRot = null;
        //forceUpperBody = false;

        //if (gun == null || targetTrs == null)
        //yield break;

        //Transform gunHoleTrs = gun.GunHoleObj.transform;
        //Vector3 lastTargetPos = targetTrs.position;

        //while (targetTrs != null)
        //{
        //    Vector3 currentTargetPos = targetTrs.position;

        //    // ��ǥ���� �������� ���� ȸ�� ����
        //    if (Vector3.Distance(currentTargetPos, lastTargetPos) > 0.01f)
        //    {
        //        Vector3 toTarget = (currentTargetPos - gunHoleTrs.position).normalized;
        //        Vector3 toTargetLocal = gunHoleTrs.InverseTransformDirection(toTarget);

        //        float pitchAngle = Mathf.Atan2(toTargetLocal.y, toTargetLocal.z) * Mathf.Rad2Deg;
        //        float clampedPitch = Mathf.Clamp(pitchAngle, -30f, 30f);

        //        Quaternion current = UpperBody.localRotation;
        //        Quaternion target = Quaternion.Euler(clampedPitch, current.eulerAngles.y, current.eulerAngles.z);

        //        UpperBody.localRotation = Quaternion.Slerp(current, target, Time.deltaTime * 5f);
        //        cachedUpperBodyEuler = UpperBody.localRotation;

        //        lastTargetPos = currentTargetPos;
        //    }

        //    yield return null;
        //}

        //forceUpperBody = false;
        //UpperBodyColutin = null;

        #region before

        //if (weapon == null || targetTrs == null) yield break;

        //Gun gun = weapon as Gun;
        //Transform gunHoleTrs = gun.GunHoleObj.transform;

        //Vector3 toTarget = (targetTrs.position - gunHoleTrs.position).normalized;
        //Vector3 toTargetLocal = gunHoleTrs.InverseTransformDirection(toTarget);

        //float pitchAngle = Mathf.Atan2(toTargetLocal.y, toTargetLocal.z) * Mathf.Rad2Deg;
        //float clampedPitch = Mathf.Clamp(pitchAngle, -30f, 30f);

        //Quaternion startRot = UpperBody.localRotation;
        //Quaternion targetRot = Quaternion.Euler(clampedPitch, startRot.eulerAngles.y, startRot.eulerAngles.z);

        //float elapsed = 0f;
        //while (elapsed < _duration)
        //{
        //    cachedUpperBodyEuler = Quaternion.Slerp(startRot, targetRot, elapsed / _duration);
        //    elapsed += Time.deltaTime;
        //    yield return null;
        //}

        ////UpperBody.rotation = targetRot;
        //cachedUpperBodyEuler = targetRot;
        //forceUpperBody = true;
        //UpperBodyColutin = null;
        //Debug.Log($"UpperBody rotation after animation: {UpperBody.localRotation.eulerAngles}");
        #endregion

        //yield return null; // 1������ ��� (�ѱ� ���� ����ȭ) 
    }

    public void GunShootEvent() //Animation Event
    {
        Gun gun = MAINWEAPON as Gun;

        if (gun == null) return;
        gun.StateUpdate(GunState.Off);
        gunShoot(gun);

        //playerAnimator.applyRootMotion = false;

        //if (playerStateData.ModeState == PlayerModeState.Npc ||
        //    playerStateData.ModeState == PlayerModeState.AutoMode)
        //{
        //    //OnAnimatorIK(attackLayerIndex);
        //    if (UpperBodyColutin == null)
        //    {
        //        //OnAnimatorIK(attackLayerIndex);
        //        forceUpperBody = true;
        //        UpperBodyColutin = AdjustUpperBodyToTargetOnce(gun, 0.2f);
        //        StartCoroutine(UpperBodyColutin);
        //    }

        //    //AdjustUpperBodyToTarget(gun);
        //    //DirectionAssistance(gun);
        //}

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
            playerStateData.AttackState = PlayerAttackState.Attack_Off;
            attackAnimation(playerStateData.AttackState, 0);
            playerAnimator.SetLayerWeight(attackLayerIndex, 0.0f);

            StopCoroutine(AdjustUpperBodyToTargetLoop(null)) ;
            forceUpperBody = false;
        }
        if (PLAYERAI.AtttackCheck())//���Ȯ��
        {
            StopCoroutine(UpperBodyColutin);
            UpperBodyColutin = null;

            UpperBody.localRotation = Quaternion.identity;

            playerStateData.AttackState = PlayerAttackState.Attack_Off;
            attackAnimation(playerStateData.AttackState, 0);
            playerAnimator.SetLayerWeight(attackLayerIndex, 0.0f);

            StopCoroutine(AdjustUpperBodyToTargetLoop(null));
            forceUpperBody = false;
        }
        //else 
        //{
        //    UpperBodyColutin = null;
        //    forceUpperBody = false;
        //}
        //if (targetTrs == null)
        //{
        //    UpperBody.localRotation = Quaternion.identity;

        //    // �ʿ��� ���� �ʱ�ȭ
        //    playerAnimator.ResetTrigger("Attack");
        //    playerStateData.AttackState = PlayerAttackState.Attack_Off;
        //    cachedUpperBodyEuler = initialUpperBodyRot; // �ʱ� ������ ȸ����
        //}
        //playerAnimator.applyRootMotion = true;

        //if (playerStateData.ModeState != PlayerModeState.None &&
        //    playerStateData.ModeState != PlayerModeState.Npc)
        //{
        //    playerStateData.AttackState = PlayerAttackState.Attack_Off;
        //    attackAnimation(playerStateData.AttackState, 0);
        //    playerAnimator.SetLayerWeight(attackLayerIndex, 0.0f);
        //}
        //else 
        //{
        //    if (!PLAYERAI.AtttackCheck() || 
        //        MAINWEAPON.ReturnTypeValue(BulletValueType.NowBullet) <= 0) 
        //    {
        //        playerStateData.AttackState = PlayerAttackState.Attack_Off;
        //        attackAnimation(playerStateData.AttackState, 0);
        //        playerAnimator.SetLayerWeight(attackLayerIndex, 0.0f);
        //    }
        //    playerStateData.AttackState = PlayerAttackState.Attack_Off;
        //}
        //if (!PLAYERAI.AtttackCheck() ||
        //        MAINWEAPON.ReturnTypeValue(BulletValueType.NowBullet) <= 0)
        //{
        //    playerStateData.AttackState = PlayerAttackState.Attack_Off;
        //    attackAnimation(playerStateData.AttackState, 0);
        //    playerAnimator.SetLayerWeight(attackLayerIndex, 0.0f);
        //}

        //playerStateData.AttackState = PlayerAttackState.Attack_Off;
        //attackAnimation(playerStateData.AttackState, 0);
        //playerAnimator.SetLayerWeight(attackLayerIndex, 0.0f);
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
        //base.clearWalkAnimation(_type);

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
