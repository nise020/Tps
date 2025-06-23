using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.SceneView;

public partial class Player : Character
{
    protected void blockCheck() 
    {
        if (playerStateData.AttackState != AttackState.Block) 
        {
            Block();
        }
    }

    protected void Block()
    {
        if (playerStateData.WeaponState == PlayerWeaponState.Sword_Off)//Open Weapon
        {
            playerAnimator.SetInteger(PlayerAnimParameters.GetWeapon.ToString(), 1);
            GetWeapon();
        }

        playerStateData.AttackState = AttackState.Block;
        attackAnimation(playerStateData.AttackState, 0);

        battelTriggerCol.enabled = true;
    }
    protected virtual void GetWeapon() 
    {

    }
    protected void AvoidanceCheck()
    {
        if (playerStateData.WalkState == PlayerWalkState.Walk ||
            playerStateData.WalkState == PlayerWalkState.Run)
        {
            Dash();
        }
        else 
        {
            Avoidance();
        }
    }
    protected void Dash() 
    {
        Debug.Log("Dashstart");
        playerStateData.WalkState = PlayerWalkState.Dash;
        playerAnimator.speed = 0f;
        WalkStateAnimation(playerStateData.WalkState);

        StartCoroutine(dashMove());
    }
    protected void Avoidance() 
    {
        if (playerStateData.avoidanceState == AvoidanceState.Avoidance_On) { return; }

        List<Monster> monsterList = Shared.MonsterManager.monsterListSearch(gameObject, radius);

        if (monsterList == null || monsterList.Count == 0)
        {
            Debug.LogError($"monster = {monsterList}");
            PerformAvoidance(false);
            return;
        }
        else
        {
            for (int i = 0; i < monsterList.Count; i++)
            {
                if (monsterList[i].AttackStateLoad())
                {
                    if (playerStateData.avoidanceState != AvoidanceState.Avoidance_On)
                    {
                        PerformAvoidance(true);
                        return;
                    }

                }
            }
            PerformAvoidance(false);
        }
    }
    private void PerformAvoidance(bool _check)
    {
        if (playerStateData.avoidanceState != AvoidanceState.Avoidance_On) 
        {
            playerStateData.avoidanceState = AvoidanceState.Avoidance_On;
            AvoidanceAnimation(playerStateData.avoidanceState);
            
        }

        if (_check)
        {
            Shared.BattelManager.JustAvoidance();
        }
    }
    //protected void AvoidanceMove(GameObject _obj)
    //{
    //    StartCoroutine(BackAvoid(_obj));
    //}
    protected IEnumerator BackAvoid(GameObject _obj,float _runTime)
    {
        float elapsed = 0f;
        Vector3 start = _obj.transform.position;
        Vector3 backDir = -charactorModelTrs.forward;
        Vector3 target = start + backDir * backDistance;
        target.y = start.y;

        while (elapsed < _runTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _runTime);

            float heightOffset = 4f * moveHeight * t * (1f - t);

            Vector3 current = Vector3.Lerp(start, target, t);
            current.y += heightOffset;

            _obj.transform.position = current;
            yield return null;
        }

        _obj.transform.position = target;
    }
    protected virtual void skillValueReset()//Damage Reset
    {
        StatusData[StatusType.Power] = attackReset;
        playerStateData.FirstSkillCheck = SkillState.SkillOff;
        playerAnimator.SetInteger(PlayerAnimName.AttackSkill.ToString(), 0);
        playerAnimator.SetInteger(PlayerAnimName.BuffSkill.ToString(), 0);
    }
    protected virtual void cameraModeChange() 
    {
        if (cameraMode == PlayerCameraMode.CameraRotationMode) 
        {
            cameraMode = PlayerCameraMode.GunAttackMode;
            viewcam.CameraModeInit(cameraMode);
        }
        else if (cameraMode == PlayerCameraMode.GunAttackMode)
        {
            cameraMode = PlayerCameraMode.CameraRotationMode;
            viewcam.CameraModeInit(cameraMode);
        }
    }

    protected virtual IEnumerator AdjustUpperBodyToTargetLoop(Gun gun)
    {
        yield return null;
    }
}
