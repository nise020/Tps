using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Photon.Pun.Demo.PunBasics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public partial class Player : Character
{
    IEnumerator AnimationEventTiming(PlayerAnimParameters _type, float _animationSpeed, float _event) 
    {
        float originalTimeToAttach = _event; //Event Time

        float delay = originalTimeToAttach / _animationSpeed;

        yield return new WaitForSeconds(delay);

        switch (_type) 
        {
            case PlayerAnimParameters.Avoidance:
                //BackAvoid(gameObject);
                break;
        }
    }
    public void AnimationStart(string _type) //AnimationEvent
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



            StartCoroutine(BackAvoid(gameObject,remainingTime));
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
    IEnumerator dashMove() 
    {
        if (dashCheck) 
        {
            yield break;
        }

        dashCheck = true;
        float elapsed = 0.0f;

        //float elapsed = 0.0f;
        Vector3 dashDirection = charactorModelTrs.forward.normalized;
        float speed = dashDistanse / dashTime;

        while (elapsed < dashTime)
        {
            transform.position += dashDirection * speed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

        playerStateData.WalkState = PlayerWalkState.Run;
        playerAnimator.speed = 1f;
        //playerAnimator.StartPlayback();
        WalkStateAnimation(playerStateData.WalkState);

        walkStateChangeTimer = walkStateChangeTime;

        dashCheck = false;
    }

    public void AnimationOut(string _type) //AnimationEvent
    {
        if (_type == null) 
        {
            Debug.LogError($"_type 값의 해당하는 애니메이션이 아닙니다");
            return;
        }

        if(_type == PlayerAnimParameters.Avoidance.ToString())
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
            playerStateData.AttackState = PlayerAttackState.Attack_Off;
            attackAnimation(playerStateData.AttackState, 0);
        }
    }

    public void AnimationCheck(string _type)
    {
        if (_type == PlayerAnimParameters.Block.ToString())
        {
            if (playerStateData.AttackState == PlayerAttackState.Attack_Off)
            {

            }
            playerStateData.AttackState = PlayerAttackState.Attack_Off;
            attackAnimation(playerStateData.AttackState, 0);
        }
    }

    public void AttackCameraEvent(int _value) //AnimationEvent
    {
        if (_value == 1) 
        {

            viewcam.CameraShakeAnimation(1);
        }
        else if (_value == 2) 
        {
            viewcam.CameraShakeAnimation(0);
        }
    }
    public void ContinuousAttack() 
    {

    }
    protected virtual void commonRSkill(PlayerType _type)
    {

    }
    protected ParticleSystem CreatSkill(GameObject _skill, GameObject _parent)
    {
        GameObject effectObj = Instantiate(_skill, Vector3.zero,
            Quaternion.identity, _parent.transform);
        effectObj.transform.localPosition = Vector3.zero;
        effectObj.transform.localRotation = Quaternion.identity;
        ParticleSystem particleSystem = effectObj.GetComponent<ParticleSystem>();
        _parent.SetActive(false);

        return particleSystem;

        //SkillParentObj1 = effectObj;
        //SkillParentObj1.transform.position = Vector3.zero;
        //SkillParentObj1.transform.rotation = Quaternion.identity;
        //SkillParentObj1.SetActive(false);
    }
    protected virtual void AvoidanceAnimation(AvoidanceState _avoidanceState) 
    {
        if (_avoidanceState == AvoidanceState.Avoidance_On)
        {
            playerAnimator.SetInteger(PlayerAnimParameters.Avoidance.ToString(), 1);
        }
        else 
        {
            playerAnimator.SetInteger(PlayerAnimParameters.Avoidance.ToString(), 0);
        }
    }
    protected virtual void walkAnim(PlayerWalkState _state, Vector3 _pos) 
    {

    }
    //protected void sidewalk(RunCheckState _runState, Vector3 _pos) 
    //{
    //    if (_pos.x == 1)//rigrt
    //    {
    //        playerAnimator.SetInteger(PlayerAnimParameters.Right.ToString(), (int)_pos.x);
    //    }
    //    else if (_pos.x == -1)//left
    //    {
    //        playerAnimator.SetInteger(PlayerAnimParameters.Left.ToString(), (int)_pos.x);
    //    }
    //    else if (_pos.z == 1)//front
    //    {
    //        if (_runState == RunCheckState.Walk)
    //        {
    //            playerStateData.WalkState = PlayerWalkState.Walk;
    //            playerAnimator.SetInteger(PlayerAnimParameters.Walk.ToString(), 1);
    //        }
    //        else if (_runState == RunCheckState.Run)
    //        {
    //            playerStateData.RunState = PlayerRunState.Run_On;
    //            playerAnimator.SetInteger(PlayerAnimParameters.Run.ToString(), 1);
    //        }
    //    }
    //    else if (_pos.z == -1)//back
    //    {
    //        playerAnimator.SetInteger(PlayerAnimParameters.Back.ToString(), (int)_pos.z);
    //    }
    //}
    //protected void moveAnim(float _move)
    //{
    //    if (_move != 0.0)//Off
    //    {
    //        weaponWalkAnim(_move, playerStateData.WeaponState);
    //    }
    //    else if (_move != 0.0)
    //    {
    //        runAnimation(_move);
    //    }
    //    else if (_move == 0)
    //    {
    //        clearWalkAnimation(playerStateData.PlayerType);
    //    }
    //}
    protected void attackAnimation(PlayerAttackState _state,int _comboValue)
    {
        switch (_state)
        {
            case PlayerAttackState.Attack_On:
                playerAnimator.SetInteger(PlayerAnimParameters.Attack.ToString(), 1);
                playerAnimator.SetInteger(PlayerAnimParameters.AttackCombo.ToString(), 0);
                break;
            case PlayerAttackState.Attack_Off:
                playerAnimator.SetInteger(PlayerAnimParameters.Attack.ToString(), 0);
                playerAnimator.SetInteger(PlayerAnimParameters.Block.ToString(), 0);
                playerAnimator.SetInteger(PlayerAnimParameters.AttackCombo.ToString(),0);
                break;
            case PlayerAttackState.Block:
                playerAnimator.SetInteger(PlayerAnimParameters.Attack.ToString(), 0);
                playerAnimator.SetInteger(PlayerAnimParameters.Block.ToString(), 1);
                break;
            case PlayerAttackState.Attack_Combo:
                playerAnimator.SetInteger(PlayerAnimParameters.Attack.ToString(), 0);
                playerAnimator.SetInteger(PlayerAnimParameters.AttackCombo.ToString(), _comboValue);
                break;
        }



        //if (_state == PlayerAttackState.Attack_On)//Off
        //{
        //    //viewcam.cameraShakeAnim(true);
        //    playerAnimator.SetInteger(PlayerAnimParameters.Attack.ToString(), 1);
        //}
        //else if (_state == PlayerAttackState.Attack_Off)
        //{
        //    //viewcam.cameraShakeAnim(false);
        //    playerAnimator.SetInteger(PlayerAnimParameters.Attack.ToString(), 0);
        //}
    }
    protected void runAnimation(float _move)
    {
        //if(playerType ==CharactorJobEnum.None)
        if (_move > 0)//Off
        {
            playerAnimator.SetInteger(PlayerAnimParameters.Run.ToString(), (int)_move);
        }
        else if (_move < 0)
        {
            playerAnimator.SetInteger(PlayerAnimParameters.Back.ToString(), (int)_move);
        }
    }
    protected void npcRunStateAnimation(float dist)
    {
        if (dist > runDistanseValue &&
            playerStateData.NpcWalkState != NpcWalkState.Run)//run
        {
            playerStateData.NpcWalkState = NpcWalkState.Run;

            playerAnimator.SetInteger(PlayerAnimParameters.Run.ToString(), 1);
            playerAnimator.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
            return;
        }
        else if (dist <= runDistanseValue && dist >= playerStopDistanseValue &&
            playerStateData.NpcWalkState != NpcWalkState.Walk)//walk
        {
            playerStateData.NpcWalkState = NpcWalkState.Walk;

            playerAnimator.SetInteger(PlayerAnimParameters.Walk.ToString(), 1);
            playerAnimator.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
            return;
        }
        else if (dist < playerStopDistanseValue &&
            playerStateData.NpcWalkState != NpcWalkState.Stop)//&& PLAYER.playerwalksateinit() == false
        {
            playerStateData.NpcWalkState = NpcWalkState.Stop;

            playerAnimator.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
            playerAnimator.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
        }
        else { return; }
    }

    protected void weaponWalkAnim(float _move, PlayerWeaponState _state)
    {
        if (_state == PlayerWeaponState.Sword_On)//Warrior 
        {
            if (_move > 0)
            {
                playerAnimator.SetInteger(PlayerAnimParameters.WeaponWalk.ToString(), (int)_move);
            }
            else if (_move < 0)
            {
                playerAnimator.SetInteger(PlayerAnimParameters.Back.ToString(), (int)_move);
            }
        }
        else if (_state == PlayerWeaponState.Sword_Off)//Warrior
        {
            if (_move > 0)
            {
                playerAnimator.SetInteger(PlayerAnimParameters.Walk.ToString(), (int)_move);
            }
            else if (_move < 0)
            {
                playerAnimator.SetInteger(PlayerAnimParameters.Back.ToString(), (int)_move);
            }
        }
        //else if (_state == PlayerWeaponState.)//Gunner
        //{
        //    if (_move > 0)
        //    {
        //        playerAnimtor.SetInteger(PlayerAnimParameters.Back.ToString(), (int)_move);
        //    }
        //    else if (_move < 0)
        //    {
        //        playerAnimtor.SetInteger(PlayerAnimParameters.Back.ToString(), (int)_move);
        //    }
        //}
    }
    //protected void walkStateChange(RunCheckState _cheack)
    //{
    //    if (_cheack == RunCheckState.Walk)//Walk_Off State
    //    {
    //        _cheack = RunCheckState.Run;
    //        playerStateData.runState = _cheack;
    //        clearWalkAnimation(playerStateData.PlayerType);
    //    }
    //    else if (_cheack == RunCheckState.Run)//Walk_On State
    //    {
    //        _cheack = RunCheckState.Walk;
    //        playerStateData.runState = _cheack;
    //        clearWalkAnimation(playerStateData.PlayerType);
    //    }
    //}
    //protected void blockAnimation(PlayerAttackState _state) 
    //{
    //    if (_state == PlayerAttackState.Block_On)//Off
    //    {
    //        //viewcam.cameraShakeAnim(true);
    //        playerAnimator.SetInteger(PlayerAnimParameters.Attack.ToString(), 1);
    //    }
    //    else if (_state == PlayerAttackState.Attack_Off)
    //    {
    //        //viewcam.cameraShakeAnim(false);
    //        playerAnimator.SetInteger(PlayerAnimParameters.Attack.ToString(), 0);
    //    }
    //}
    protected virtual void WalkStateAnimation(PlayerWalkState _state) 
    {
        switch (_state) 
        {
            case PlayerWalkState.Walk:
                playerAnimator.SetInteger(PlayerAnimParameters.Dash.ToString(), 0);
                playerAnimator.SetInteger(PlayerAnimParameters.Walk.ToString(), 1);
                playerAnimator.SetInteger(PlayerAnimParameters.Run.ToString(), 0); ;
                break;
            case PlayerWalkState.Run:
                playerAnimator.SetInteger(PlayerAnimParameters.Dash.ToString(), 0);
                playerAnimator.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
                playerAnimator.SetInteger(PlayerAnimParameters.Run.ToString(), 1); 
                break;
            case PlayerWalkState.Dash:
                playerAnimator.SetInteger(PlayerAnimParameters.Dash.ToString(), 1);
                playerAnimator.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
                playerAnimator.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
                break;

        }
    }
    protected virtual void clearWalkAnimation() 
    {
        playerStateData.WalkState = PlayerWalkState.Stop;
        playerAnimator.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
        playerAnimator.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
        //if (playerStateData.WalkState != PlayerWalkState.Stop)
        //{
        //    playerStateData.WalkState = PlayerWalkState.Stop;
        //    playerAnimator.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
        //    playerAnimator.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
        //    playerAnimator.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
        //}
        //else { return; }
        //if (playerStateData.RunState == PlayerRunState.Run_On)
        //{
        //    playerStateData.RunState = PlayerRunState.Run_Off;

        //    playerAnimator.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
        //}
        //if (playerStateData.NpcWalkState != NpcWalkState.Stop) 
        //{
        //    playerStateData.NpcWalkState = NpcWalkState.Stop;

        //    playerAnimator.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
        //    playerAnimator.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
        //}
    }
    private void shitdownAnim(bool _check)
    {
        if (_check)
        {
            playerStateData.ShitState = PlayerShitState.ShitDown;
            playerAnimator.SetInteger(PlayerAnimName.Shit.ToString(), 1);
        }
        else 
        {
            playerStateData.ShitState = PlayerShitState.ShitUP;
            playerAnimator.SetInteger(PlayerAnimName.Shit.ToString(), 0);
        }
    }
    protected virtual void shitdownCheak()
    {
        shitCheack = !shitCheack;
        shitdownAnim(shitCheack);
    }
    public void closeSwordAttack(bool _check)//bug check
    {
        if (!_check) { return; }

        if (_check)
        {
            playerAnimator.SetLayerWeight(attackLayerIndex, 1.0f);
            playerAnimator.SetInteger(PlayerAnimParameters.Close.ToString(), 1);
            //animCheck(PlayerAnimParameters.Close.ToString(), PlayerAnimName.closeAttack.ToString());
        }
    }
    public void ClearAllAnimation(PlayerType _type)//PlayerChange
    {
        clearWalkAnimation();
    }
    //public void animCheck(string _parameterText, string _animText) 
    //{
    //    int index = attackLayerIndex;

    //    AnimatorStateInfo animStateInfo = playerAnimtor.GetCurrentAnimatorStateInfo(index);
    //    float time = animStateInfo.normalizedTime;

    //    if (time >= 1.0f && animStateInfo.IsName(_animText))
    //    {
    //        string reloading = ($"{PlayerAnimName.reloading}");
    //        string closeAttack = ($"{PlayerAnimName.closeAttack}");
    //        if (_animText == reloading)
    //        {
    //            //StartCoroutine(reLoadout(index));
    //        }
    //        else if (_animText == closeAttack)
    //        {
    //            closeCheck = false;
    //        }
    //        playerAnimtor.SetLayerWeight(index, 0.0f);
    //        playerAnimtor.SetInteger(_parameterText, 0);
    //    }
    //}
    protected void SkillAnimation(SkillType _type, bool _check)
    {
        if (_type == SkillType.Skill1)
        {
            if (_check)
            {
                playerStateData.FirstSkillCheck = SkillState.SkillOn;
                playerAnimator.SetInteger(SkillType.Skill1.ToString(), 1);
            }
            else
            {
                playerStateData.FirstSkillCheck = SkillState.SkillOff;
                playerAnimator.SetInteger(SkillType.Skill1.ToString(), 0);
            }
        }
        else if (_type == SkillType.Skill2)
        {
            if (_check)
            {
                playerStateData.SecondSkillCheck = SkillState.SkillOn;
                playerAnimator.SetInteger(SkillType.Skill2.ToString(), 1);
            }
            else
            {
                playerStateData.SecondSkillCheck = SkillState.SkillOff;
                playerAnimator.SetInteger(SkillType.Skill2.ToString(), 0);
            }
        }
    }
    private void closeAttackCheack()
    {
        bool check = Input.GetKeyDown(KeyCode.Q);
        if (check)
        {
            closeCheck = !closeCheck;
        }

        if (closeCheck)
        {
            closeSwordAttack(closeCheck);
        }
    }
    //IEnumerator reLoadout(int _index)
    //{
    //    GUN.nowbullet = GUN.bullet;
    //    GUN.bulletcount = 0;
    //    playerAnim.SetLayerWeight(_index, 0.0f);
    //    GUN.reLoed = false;
    //    yield return null;
    //}
   
}
