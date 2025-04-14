using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public partial class Player : Charactor
{
    [Header("Animator Info")]
    int attackLayerIndex = 1;
    int BaseLayerIndex = 0;
    [SerializeField] bool shitCheack = false;
    [SerializeField] bool closeCheck = false;
     bool shitOn = false;

    protected SkillRunning Skillcheck = SkillRunning.SkillOff;
    protected WeaponState weaponState = WeaponState.None;
    protected NpcRunState npcRunState = NpcRunState.Run_Off;
    protected ReloadState reloadState = ReloadState.None;
    public void skillAnimation()//AnimationEvent
    {
        Skillcheck = SkillRunning.SkillOff;
    }
    public void ContinuousAttack() 
    {

    }
    public void reloding(CharactorJobEnum _type)
    {
        if (_type != CharactorJobEnum.Gunner) { return; }

        if (reloadOn || GUN.nowbullet <= 0)
        {
            playerAnim.SetLayerWeight(attackLayerIndex, 1.0f);
            playerAnim.SetInteger(PlayerAnimParameters.Reload.ToString(), 1);
        }
    }
    public void ReloadOut()//AnimationEvent
    {
        //AnimationEvent
        reloadState = ReloadState.ReloadOff;
        GUN.nowbullet = GUN.bullet;
        GUN.bulletcount = 0;
        playerAnim.SetLayerWeight(attackLayerIndex, 0.0f);
        //playerAnim.SetLayerWeight(BaseLayerIndex, 1.0f);
        playerAnim.SetInteger(PlayerAnimParameters.Reload.ToString(), 0);
    }
    public void GetSword()//AnimationEvent
    {
        GameObject go = weapon.gameObject;
        go.transform.SetParent(HandObj.gameObject.transform);
        go.transform.localPosition = Vector3.zero;
    }
    public void ClearlSword()//AnimationEvent
    {
        GameObject go = weapon.gameObject;
        go.transform.SetParent(scabbard.gameObject.transform);
        go.transform.localPosition = Vector3.zero;
    }
   
    protected virtual void walkAnim(RunState _runState, Vector3 _pos) 
    {
        if (_runState == RunState.Walk)
        {
            playerWalkState = PlayerWalkState.Walk_On;
            playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 1);
        }
        else if (_runState == RunState.Run)
        {
            playerRunState = PlayerRunState.Run_On;
            playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 1);
        }
    }
    protected void Sidewalk(RunState _runState, Vector3 _pos) 
    {
        if (_pos.x == 1)//rigrt
        {
            playerAnim.SetInteger(PlayerAnimParameters.Right.ToString(), (int)_pos.x);
        }
        else if (_pos.x == -1)//left
        {
            playerAnim.SetInteger(PlayerAnimParameters.Left.ToString(), (int)_pos.x);
        }
        else if (_pos.z == 1)//front
        {
            if (_runState == RunState.Walk)
            {
                playerWalkState = PlayerWalkState.Walk_On;
                playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 1);
            }
            else if (_runState == RunState.Run)
            {
                playerRunState = PlayerRunState.Run_On;
                playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 1);
            }
        }
        else if (_pos.z == -1)//back
        {
            playerAnim.SetInteger(PlayerAnimParameters.Back.ToString(), (int)_pos.z);
        }
    }
    protected void moveAnim(float _move)
    {
        if (_move != 0.0)//Off
        {
            WeaponWalkAnim(_move, weaponState);
        }
        else if (_move != 0.0)
        {
            runAnim(_move);
        }
        else if (_move == 0)
        {
            clearWalkAnim(playerType);
        }
    }
    protected void AttackAnim(float _move)
    {
        if (_move > 0)//Off
        {
            playerAnim.SetInteger(PlayerAnimParameters.Attack.ToString(), (int)_move);
        }
        else if (_move < 0)
        {
            playerAnim.SetInteger(PlayerAnimParameters.Attack.ToString(), (int)_move);
        }
    }
    protected void runAnim(float _move)
    {
        //if(playerType ==CharactorJobEnum.None)
        if (_move > 0)//Off
        {
            playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), (int)_move);
        }
        else if (_move < 0)
        {
            playerAnim.SetInteger(PlayerAnimParameters.Back.ToString(), (int)_move);
        }
    }
    protected void npcRunStateAnim(float dist)
    {
        if (dist > runDistanseValue && npcRunState != NpcRunState.Run_On)//run
        {
            npcRunState = NpcRunState.Run_On;
            playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 1);
            playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
            return;
        }
        else if (dist <= runDistanseValue && dist >= playerStopDistanseValue && npcRunState != NpcRunState.Run_Off)//walk
        {
            npcRunState = NpcRunState.Run_Off;
            playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 1);
            playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
            return;
        }
        else if (dist <= playerStopDistanseValue && npcRunState != NpcRunState.None)//&& PLAYER.playerwalksateinit() == false
        {
            npcRunState = NpcRunState.None;
            playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
        }
        else { return; }
    }

    protected void WeaponWalkAnim(float _move, WeaponState _state)
    {
        if (_state == WeaponState.Sword_On)//Warrior 
        {
            if (_move > 0)
            {
                playerAnim.SetInteger(PlayerAnimParameters.WeaponWalk.ToString(), (int)_move);
            }
            else if (_move < 0)
            {
                playerAnim.SetInteger(PlayerAnimParameters.Back.ToString(), (int)_move);
            }
        }
        else if (_state == WeaponState.Sword_Off)//Warrior
        {
            if (_move > 0)
            {
                playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), (int)_move);
            }
            else if (_move < 0)
            {
                playerAnim.SetInteger(PlayerAnimParameters.Back.ToString(), (int)_move);
            }
        }
        else if (_state == WeaponState.None)//Gunner
        {
            if (_move > 0)
            {
                playerAnim.SetInteger(PlayerAnimParameters.Back.ToString(), (int)_move);
            }
            else if (_move < 0)
            {
                playerAnim.SetInteger(PlayerAnimParameters.Back.ToString(), (int)_move);
            }
        }
    }
    protected void WalkStateChange(RunState _cheack)
    {
        if (_cheack == RunState.Walk)//Walk_Off State
        {
            _cheack = RunState.Run;
            runState = _cheack;
            clearWalkAnim(playerType);
        }
        else if (_cheack == RunState.Run)//Walk_On State
        {
            _cheack = RunState.Walk;
            runState = _cheack;
            clearWalkAnim(playerType);
        }
    }
    public void ClearAllAnimation(CharactorJobEnum type)//PlayerChange
    {
        if (type == CharactorJobEnum.Gunner) 
        {
            playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(),0);
            playerAnim.SetInteger(PlayerAnimParameters.Back.ToString(),0);
            playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(),0);
            playerAnim.SetInteger(PlayerAnimParameters.Right.ToString(),0);
            playerAnim.SetInteger(PlayerAnimParameters.Left.ToString(),0);
        }
        else if (type == CharactorJobEnum.Warrior) 
        {
            
        }
    }
    private void clearWalkAnim(CharactorJobEnum _type) 
    {
        if (playerWalkState == PlayerWalkState.Walk_On)
        {
            playerWalkState = PlayerWalkState.Walk_Off;
        }
        else if (playerRunState == PlayerRunState.Run_On)
        {
            playerRunState = PlayerRunState.Run_Off;
        }
        else { return; }

        if (_type == CharactorJobEnum.Gunner)
        {
            playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
            playerAnim.SetInteger(PlayerAnimParameters.Back.ToString(), 0);
            playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
            playerAnim.SetInteger(PlayerAnimParameters.Right.ToString(), 0);
            playerAnim.SetInteger(PlayerAnimParameters.Left.ToString(), 0);
        }
        else if (_type == CharactorJobEnum.Warrior) 
        {
            playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
        }
    }
    private void shitdownAnim(bool _check)
    {
        if (_check)
        {
            playerShitState = PlayerShitState.ShitDown;
            playerAnim.SetInteger(PlayerAnimName.Shit.ToString(), 1);
        }
        else 
        {
            playerShitState = PlayerShitState.ShitUP;
            playerAnim.SetInteger(PlayerAnimName.Shit.ToString(), 0);
        }
    }
    protected virtual void shitdownCheak()
    {
        shitCheack = !shitCheack;
        shitdownAnim(shitCheack);
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
    public void closeSwordAttack(bool _check)//bug check
    {
        if (!_check) { return; }

        if (_check)
        {
            playerAnim.SetLayerWeight(attackLayerIndex, 1.0f);
            playerAnim.SetInteger(PlayerAnimParameters.Close.ToString(), 1);
            //animCheck(PlayerAnimParameters.Close.ToString(), PlayerAnimName.closeAttack.ToString());
        }
    }
    public void animCheck(string _parameterText, string _animText) 
    {
        int index = attackLayerIndex;

        AnimatorStateInfo animStateInfo = playerAnim.GetCurrentAnimatorStateInfo(index);
        float time = animStateInfo.normalizedTime;

        if (time >= 1.0f && animStateInfo.IsName(_animText))
        {
            string reloading = ($"{PlayerAnimName.reloading}");
            string closeAttack = ($"{PlayerAnimName.closeAttack}");
            if (_animText == reloading)
            {
                //StartCoroutine(reLoadout(index));
            }
            else if (_animText == closeAttack)
            {
                closeCheck = false;
            }
            playerAnim.SetLayerWeight(index, 0.0f);
            playerAnim.SetInteger(_parameterText, 0);
        }
    }
    IEnumerator reLoadout(int _index)
    {
        GUN.nowbullet = GUN.bullet;
        GUN.bulletcount = 0;
        playerAnim.SetLayerWeight(_index, 0.0f);
        GUN.reLoed = false;
        yield return null;
    }
}
