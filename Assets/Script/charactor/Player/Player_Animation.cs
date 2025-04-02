using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public partial class Player : Charactor
{
    [Header("Animator Info")]
    int attackLayerIndex = 1;
    [SerializeField] bool shitCheack = false;
    [SerializeField] bool closeCheck = false;
     bool shitOn = false;
    Playerstate upperState = Playerstate.Null;
    Playerstate lowerState = Playerstate.Null;

    protected SkillRunning Skillcheck = SkillRunning.SkillOff;
    protected WeaponState weaponState = WeaponState.None;
    protected RunState runState = RunState.Run_Off;

    public void skillAnimation()//AnimationEvent
    {
        Skillcheck = SkillRunning.SkillOff;
    }
    public void ContinuousAttack() 
    {

    }
    public void ReloadOut()//AnimationEvent
    {
        //AnimationEvent
        playerAnim.SetLayerWeight(attackLayerIndex, 1.0f);
        playerAnim.SetInteger("Reload", 0);
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
    protected override void move(PlayerControll _value)
    {
        if (_value == PlayerControll.Off) 
        {
            return; 
        }
        Vector3 movePos = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = transform.TransformDirection(movePos.normalized);

        if (movePos.magnitude > 0.1f)
        {
            float speed = runValue ? speedValue * 2 : speedValue;
            transform.localPosition += direction * (speed) * Time.deltaTime;
            //rigid.velocity = direction * speed;
        }
        else
        {
            return;
        }
        //All
        //moveAnim(movePos.z);
        //Gunner
        //sideWalkAnim(movePos.x, playerType);
    }
    protected void sideWalkAnim(float _move, PlayerEnum _type) 
    {
        if (_type != PlayerEnum.Gunner) return;
        if (_move > 0) 
        {
            playerAnim.SetInteger("Right", (int)_move);
        }
        else if (_move < 0)
        {
            playerAnim.SetInteger("Left", (int)_move);
        }
    }
    protected void moveAnim(float _move)
    {
        if (runState == RunState.Run_Off && _move != 0.0)//Off
        {
            walkAnim(_move, weaponState);
        }
        else if (runState == RunState.Run_On && _move != 0.0)
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
            playerAnim.SetInteger("Attack", (int)_move);
        }
        else if (_move < 0)
        {
            playerAnim.SetInteger("Attack", (int)_move);
        }
    }
    protected void runAnim(float _move)
    {
        if (_move > 0)//Off
        {
            playerAnim.SetInteger("Run", (int)_move);
        }
        else if (_move < 0)
        {
            playerAnim.SetInteger("Back", (int)_move);
        }
    }
    protected void walkAnim(float _move, WeaponState _state)
    {
        if (_state == WeaponState.Sword_On)//Warrior 
        {
            if (_move > 0)
            {
                playerAnim.SetInteger("WeaponWalk", (int)_move);
            }
            else if (_move < 0)
            {
                playerAnim.SetInteger("Back", (int)_move);
            }
        }
        else if (_state == WeaponState.Sword_Off)//Warrior
        {
            if (_move > 0)
            {
                playerAnim.SetInteger("Walk", (int)_move);
            }
            else if (_move < 0)
            {
                playerAnim.SetInteger("Back", (int)_move);
            }
        }
        else if (_state == WeaponState.None)//Gunner
        {
            if (_move > 0)
            {
                playerAnim.SetInteger("Walk", (int)_move);
            }
            else if (_move < 0)
            {
                playerAnim.SetInteger("Back", (int)_move);
            }
        }
    }
    protected void runcheck(bool _cheack)
    {
        if (_cheack)
        {
            runValue = !runValue;
            runState = RunState.Run_On;
            clearWalkAnim(playerType);
        }
        else { return; }
    }
    public void ClearAllAnimation(PlayerEnum type)//PlayerChange
    {
        if (type == PlayerEnum.Gunner) 
        {
            playerAnim.SetInteger("Walk", 0);
            playerAnim.SetInteger("Back", 0);
            playerAnim.SetInteger("Run", 0);
            playerAnim.SetInteger("Right", 0);
            playerAnim.SetInteger("Left", 0);
        }
        else if (type == PlayerEnum.Warrior) 
        {
            
        }
    }
    private void clearWalkAnim(PlayerEnum _type) 
    {
        if (_type == PlayerEnum.Gunner)
        {
            playerAnim.SetInteger("Walk", 0);
            playerAnim.SetInteger("Back", 0);
            playerAnim.SetInteger("Run", 0);
            playerAnim.SetInteger("Right", 0);
            playerAnim.SetInteger("Left", 0);
        }
        else if (_type == PlayerEnum.Warrior) 
        {

        }
    }
    private void shitdownAnim(bool _check)
    {
        string text = ($"{PlayerAnimParameters.Shit}");
        if (_check)
        {
            playerAnim.SetInteger("Shit", 1);
        }
        else 
        {
            playerAnim.SetInteger("Shit", 0);
        }
    }
    protected virtual void shitdownCheak()
    {
        bool cheak = Input.GetKeyDown(KeyCode.Z);
        if (cheak)
        {
            shitCheack = !shitCheack;
            shitdownAnim(shitCheack);
        }
        if (movePos.z != 0.0 || movePos.x != 0.0) 
        {
            playerAnim.SetInteger("Shit", 0);
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
    public void closeSwordAttack(bool _check)//bug check
    {
        if (!_check) { return; }

        if (_check)
        {
            playerAnim.SetLayerWeight(attackLayerIndex, 1.0f);
            playerAnim.SetInteger("Close", 1);
            animCheck("Close", "closeAttack");
        }
    }
    public void animCheck(string _parameterText, string _animText) 
    {
        int index = attackLayerIndex;

        AnimatorStateInfo animStateInfo = playerAnim.GetCurrentAnimatorStateInfo(index);
        float time = animStateInfo.normalizedTime;

        if (time >= 1.0f && animStateInfo.IsName(_animText))
        {
            string reloading = ($"{playerAnimInfoName.reloading}");
            string closeAttack = ($"{playerAnimInfoName.closeAttack}");
            if (_animText == reloading)
            {
                StartCoroutine(reLoadout(index));
            }
            else if (_animText == closeAttack)
            {
                closeCheck = false;
            }
            playerAnim.SetLayerWeight(index, 0.0f);
            playerAnim.SetInteger(_parameterText, 0);
        }
    }
}
