using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Charactor
{
    [Header("Animator Info")]
    int attackLayerIndex = 1;
    [SerializeField] bool shitCheack = false;
    [SerializeField] bool closeCheck = false;
     bool shitOn = false;
    Playerstate upperState = Playerstate.Null;
    Playerstate lowerState = Playerstate.Null;

    protected override void move()
    {
        Vector3 movePos = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        moveAnim(movePos.z);
        sideWalk(movePos.x);
        Vector3 direction = transform.TransformDirection(movePos.normalized);

        if (movePos.magnitude > 0.1f)
        {
            float speed = runstate ? moveSpeed * 2 : moveSpeed;
            transform.localPosition += direction * (speed) * Time.deltaTime;
            //rigid.velocity = direction * speed;
        }
        else
        {
            return;
            rigid.velocity = Vector3.zero;
        }
    }
    private void sideWalk(float _move) 
    {
        if (_move > 0) 
        {
            playerAnim.SetInteger("Right", (int)_move);
        }
        else if (_move < 0)
        {
            playerAnim.SetInteger("Left", (int)_move);
        }
    }
    private void moveAnim(float _move)
    {
        if (runstate == false && _move != 0.0)//Off
        {
            walkAnim(playerAnim, _move);
        }
        else if (runstate == true && _move != 0.0)
        {
            runAnim(playerAnim, _move);
        }
        else if (_move == 0)
        {
            clearAnim();
        }
    }
    private void runAnim(Animator _anim, float _move)
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
    private void walkAnim(Animator _anim, float _move)
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
    private void runcheck()
    {
        bool cheack = Input.GetKeyDown(KeyCode.Mouse1);
        if (cheack)
        {
            runstate = !runstate;
            clearAnim();
        }
        else { return; }
    }
    private void clearAnim() 
    {
        playerAnim.SetInteger("Walk", 0);
        playerAnim.SetInteger("Back", 0);
        playerAnim.SetInteger("Run", 0);
        playerAnim.SetInteger("Right", 0);
        playerAnim.SetInteger("Left", 0);
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
    private void shitdownCheak()
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
