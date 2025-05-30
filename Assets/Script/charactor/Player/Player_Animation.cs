using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public partial class Player : Character
{
    
    
    public void AnimationOut(string _type) //AnimationEvent
    {
        if (_type == null) 
        {
            Debug.LogError($"_type 값의 해당하는 애니메이션이 아닙니다");
            return;
        }

        if (_type == "Attack") 
        {
            
            playerAnimtor.SetInteger(PlayerAnimParameters.Attack.ToString(), 0);
        }
        else if (_type == "ComboAttack")
        {
            playerAnimtor.SetInteger(PlayerAnimParameters.AttackCombo.ToString(), 0);
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
    protected virtual void inPutCameraAnimation(bool _check) 
    {

    }
    protected virtual void walkAnim(RunCheckState _runState, Vector3 _pos) 
    {
        if (_runState == RunCheckState.Walk)
        {
            if (playerStateData.WalkState == PlayerWalkState.Walk_Off) 
            {
                playerStateData.WalkState = PlayerWalkState.Walk_On;
                playerAnimtor.SetInteger(PlayerAnimParameters.Walk.ToString(), 1);
            }
        }
        else if (_runState == RunCheckState.Run)
        {
            if (playerStateData.RunState == PlayerRunState.Run_On)
            {
                playerStateData.RunState = PlayerRunState.Run_On;
                playerAnimtor.SetInteger(PlayerAnimParameters.Run.ToString(), 1);
            }
        }
    }
    protected void sidewalk(RunCheckState _runState, Vector3 _pos) 
    {
        if (_pos.x == 1)//rigrt
        {
            playerAnimtor.SetInteger(PlayerAnimParameters.Right.ToString(), (int)_pos.x);
        }
        else if (_pos.x == -1)//left
        {
            playerAnimtor.SetInteger(PlayerAnimParameters.Left.ToString(), (int)_pos.x);
        }
        else if (_pos.z == 1)//front
        {
            if (_runState == RunCheckState.Walk)
            {
                playerStateData.WalkState = PlayerWalkState.Walk_On;
                playerAnimtor.SetInteger(PlayerAnimParameters.Walk.ToString(), 1);
            }
            else if (_runState == RunCheckState.Run)
            {
                playerStateData.RunState = PlayerRunState.Run_On;
                playerAnimtor.SetInteger(PlayerAnimParameters.Run.ToString(), 1);
            }
        }
        else if (_pos.z == -1)//back
        {
            playerAnimtor.SetInteger(PlayerAnimParameters.Back.ToString(), (int)_pos.z);
        }
    }
    protected void moveAnim(float _move)
    {
        if (_move != 0.0)//Off
        {
            weaponWalkAnim(_move, playerStateData.WeaponState);
        }
        else if (_move != 0.0)
        {
            runAnimation(_move);
        }
        else if (_move == 0)
        {
            clearWalkAnimation(playerStateData.PlayerType);
        }
    }
    protected void attackAnimation(PlayerAttackState _state)
    {
        if (_state == PlayerAttackState.Attack_On)//Off
        {
            //viewcam.cameraShakeAnim(true);
            playerAnimtor.SetInteger(PlayerAnimParameters.Attack.ToString(), 1);
        }
        else if (_state == PlayerAttackState.Attack_Off)
        {
            //viewcam.cameraShakeAnim(false);
            playerAnimtor.SetInteger(PlayerAnimParameters.Attack.ToString(), 0);
        }
    }
    protected void runAnimation(float _move)
    {
        //if(playerType ==CharactorJobEnum.None)
        if (_move > 0)//Off
        {
            playerAnimtor.SetInteger(PlayerAnimParameters.Run.ToString(), (int)_move);
        }
        else if (_move < 0)
        {
            playerAnimtor.SetInteger(PlayerAnimParameters.Back.ToString(), (int)_move);
        }
    }
    protected void npcRunStateAnimation(float dist)
    {
        if (dist > runDistanseValue &&
            playerStateData.NpcWalkState != NpcWalkState.Run)//run
        {
            playerStateData.NpcWalkState = NpcWalkState.Run;

            playerAnimtor.SetInteger(PlayerAnimParameters.Run.ToString(), 1);
            playerAnimtor.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
            return;
        }
        else if (dist <= runDistanseValue && dist >= playerStopDistanseValue &&
            playerStateData.NpcWalkState != NpcWalkState.Walk)//walk
        {
            playerStateData.NpcWalkState = NpcWalkState.Walk;

            playerAnimtor.SetInteger(PlayerAnimParameters.Walk.ToString(), 1);
            playerAnimtor.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
            return;
        }
        else if (dist < playerStopDistanseValue &&
            playerStateData.NpcWalkState != NpcWalkState.Stop)//&& PLAYER.playerwalksateinit() == false
        {
            playerStateData.NpcWalkState = NpcWalkState.Stop;

            playerAnimtor.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
            playerAnimtor.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
        }
        else { return; }
    }

    protected void weaponWalkAnim(float _move, PlayerWeaponState _state)
    {
        if (_state == PlayerWeaponState.Sword_On)//Warrior 
        {
            if (_move > 0)
            {
                playerAnimtor.SetInteger(PlayerAnimParameters.WeaponWalk.ToString(), (int)_move);
            }
            else if (_move < 0)
            {
                playerAnimtor.SetInteger(PlayerAnimParameters.Back.ToString(), (int)_move);
            }
        }
        else if (_state == PlayerWeaponState.Sword_Off)//Warrior
        {
            if (_move > 0)
            {
                playerAnimtor.SetInteger(PlayerAnimParameters.Walk.ToString(), (int)_move);
            }
            else if (_move < 0)
            {
                playerAnimtor.SetInteger(PlayerAnimParameters.Back.ToString(), (int)_move);
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
    protected void walkStateChange(RunCheckState _cheack)
    {
        if (_cheack == RunCheckState.Walk)//Walk_Off State
        {
            _cheack = RunCheckState.Run;
            playerStateData.runState = _cheack;
            clearWalkAnimation(playerStateData.PlayerType);
        }
        else if (_cheack == RunCheckState.Run)//Walk_On State
        {
            _cheack = RunCheckState.Walk;
            playerStateData.runState = _cheack;
            clearWalkAnimation(playerStateData.PlayerType);
        }
    }
    protected virtual void clearWalkAnimation(PlayerType _type) 
    {
        if (playerStateData.WalkState == PlayerWalkState.Walk_On)
        {
            playerStateData.WalkState = PlayerWalkState.Walk_Off;

            playerAnimtor.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
        }
        if (playerStateData.RunState == PlayerRunState.Run_On)
        {
            playerStateData.RunState = PlayerRunState.Run_Off;

            playerAnimtor.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
        }
        if (playerStateData.NpcWalkState != NpcWalkState.Stop) 
        {
            playerStateData.NpcWalkState = NpcWalkState.Stop;

            playerAnimtor.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
            playerAnimtor.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
        }
        else { return; }
    }
    private void shitdownAnim(bool _check)
    {
        if (_check)
        {
            playerStateData.ShitState = PlayerShitState.ShitDown;
            playerAnimtor.SetInteger(PlayerAnimName.Shit.ToString(), 1);
        }
        else 
        {
            playerStateData.ShitState = PlayerShitState.ShitUP;
            playerAnimtor.SetInteger(PlayerAnimName.Shit.ToString(), 0);
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
            playerAnimtor.SetLayerWeight(attackLayerIndex, 1.0f);
            playerAnimtor.SetInteger(PlayerAnimParameters.Close.ToString(), 1);
            //animCheck(PlayerAnimParameters.Close.ToString(), PlayerAnimName.closeAttack.ToString());
        }
    }
    public void ClearAllAnimation(PlayerType _type)//PlayerChange
    {
        clearWalkAnimation(_type);
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
                playerStateData.firstSkillCheck = SkillState.SkillOn;
                playerAnimtor.SetInteger(SkillType.Skill1.ToString(), 1);
            }
            else
            {
                playerStateData.firstSkillCheck = SkillState.SkillOff;
                playerAnimtor.SetInteger(SkillType.Skill1.ToString(), 0);
            }
        }
        else if (_type == SkillType.Skill2)
        {
            if (_check)
            {
                playerStateData.secondSkillCheck = SkillState.SkillOn;
                playerAnimtor.SetInteger(SkillType.Skill2.ToString(), 1);
            }
            else
            {
                playerStateData.secondSkillCheck = SkillState.SkillOff;
                playerAnimtor.SetInteger(SkillType.Skill2.ToString(), 0);
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
