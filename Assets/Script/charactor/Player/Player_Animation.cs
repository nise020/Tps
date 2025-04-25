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
    protected int attackLayerIndex = 1;
    protected int BaseLayerIndex = 0;
    [SerializeField] bool shitCheack = false;
    [SerializeField] bool closeCheck = false;
     bool shitOn = false;

    protected SkillRunning skillcheck = SkillRunning.SkillOff;
    protected WeaponState weaponState = WeaponState.None;
    //protected NpcRunState npcRunState = NpcRunState.Run_Off;
    protected ReloadState reloadState = ReloadState.ReloadOff;

    [SerializeField] protected GameObject SkillEffectObj1;
    [SerializeField] protected GameObject SkillEffectObj2;

    protected ParticleSystem SkillEffectSystem1 = null;
    protected ParticleSystem SkillEffectSystem2 = null;

    [SerializeField] protected GameObject SkillParentObj1 = null;
    [SerializeField] protected GameObject SkillParentObj2 = null;

    public void skillAnimation()//AnimationEvent
    {
        skillcheck = SkillRunning.SkillOff;
    }
    public void ContinuousAttack() 
    {

    }
    protected virtual void commonRSkill(CharactorJobEnum _type)
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
    protected virtual void inPutCameraAnimation(bool _check, MouseInputType _type) 
    {

    }
    protected virtual void walkAnim(RunState _runState, Vector3 _pos) 
    {
        if (_runState == RunState.Walk)
        {
            if (playerWalkState == PlayerWalkState.Walk_Off) 
            {
                playerWalkState = PlayerWalkState.Walk_On;
                playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 1);
            }
        }
        else if (_runState == RunState.Run)
        {
            if (playerRunState == PlayerRunState.Run_On)
            {
                playerRunState = PlayerRunState.Run_On;
                playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 1);
            }
        }
    }
    protected void sidewalk(RunState _runState, Vector3 _pos) 
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
            weaponWalkAnim(_move, weaponState);
        }
        else if (_move != 0.0)
        {
            runAnimation(_move);
        }
        else if (_move == 0)
        {
            clearWalkAnimation(playerType);
        }
    }
    protected void attackAnimation(AttackState _state)
    {
        if (_state == AttackState.AttackOn)//Off
        {
            playerAnim.SetInteger(PlayerAnimParameters.Attack.ToString(), 1);
        }
        else if (_state == AttackState.AttackOff)
        {
            playerAnim.SetInteger(PlayerAnimParameters.Attack.ToString(), 0);
        }
    }
    protected void runAnimation(float _move)
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
    protected void npcRunStateAnimation(float dist)
    {
        if (dist > runDistanseValue && 
            npcWalkState != NpcWalkState.Run)//run
        {
            //npcRunState = NpcRunState.Run_On;
            npcWalkState = NpcWalkState.Run;
            playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 1);
            playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
            return;
        }
        else if (dist <= runDistanseValue && dist >= playerStopDistanseValue &&
            npcWalkState != NpcWalkState.Walk)//walk
        {
            //npcRunState = NpcRunState.Run_Off;
            npcWalkState = NpcWalkState.Walk;
            playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 1);
            playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
            return;
        }
        else if (dist < playerStopDistanseValue &&
            npcWalkState != NpcWalkState.Stop)//&& PLAYER.playerwalksateinit() == false
        {
            //npcRunState = NpcRunState.None;
            npcWalkState = NpcWalkState.Stop;
            playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
        }
        else { return; }
    }

    protected void weaponWalkAnim(float _move, WeaponState _state)
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
    protected void walkStateChange(RunState _cheack)
    {
        if (_cheack == RunState.Walk)//Walk_Off State
        {
            _cheack = RunState.Run;
            runState = _cheack;
            clearWalkAnimation(playerType);
        }
        else if (_cheack == RunState.Run)//Walk_On State
        {
            _cheack = RunState.Walk;
            runState = _cheack;
            clearWalkAnimation(playerType);
        }
    }
    protected virtual void clearWalkAnimation(CharactorJobEnum _type) 
    {
        if (playerWalkState == PlayerWalkState.Walk_On)
        {
            playerWalkState = PlayerWalkState.Walk_Off;

            playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
        }
        if (playerRunState == PlayerRunState.Run_On)
        {
            playerRunState = PlayerRunState.Run_Off;

            playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
        }
        if (npcWalkState != NpcWalkState.Stop) 
        {
            npcWalkState = NpcWalkState.Stop;

            playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
            playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 0);
        }
        else { return; }
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
    public void ClearAllAnimation(CharactorJobEnum _type)//PlayerChange
    {
        clearWalkAnimation(_type);
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
    protected void SkillAnimation(SkillType _type, bool _check)
    {
        if (_type == SkillType.Skill1)
        {
            if (_check)
            {
                firstSkillCheck = SkillRunning.SkillOn;
                playerAnim.SetInteger(SkillType.Skill1.ToString(), 1);
            }
            else
            {
                firstSkillCheck = SkillRunning.SkillOff;
                playerAnim.SetInteger(SkillType.Skill1.ToString(), 0);
            }
        }
        else if (_type == SkillType.Skill2)
        {
            if (_check)
            {
                secondSkillCheck = SkillRunning.SkillOn;
                playerAnim.SetInteger(SkillType.Skill2.ToString(), 1);
            }
            else
            {
                secondSkillCheck = SkillRunning.SkillOff;
                playerAnim.SetInteger(SkillType.Skill2.ToString(), 0);
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
