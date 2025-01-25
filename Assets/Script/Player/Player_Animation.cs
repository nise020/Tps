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

    private void upperStateEnum() 
    {
        //close Sword
        bool closeAttack = Input.GetKeyDown(KeyCode.Q);
        if (closeAttack)
        {
            closeCheck = !closeCheck;

            if (closeCheck)
            {
                closeSwordAttack(closeCheck);
            }
        }

        //reload
        bool reload = Input.GetKeyDown(KeyCode.R);
        if (reload || gun.nowbullet <= 0) 
        {
            reloding();
        }

        //ATTACK
        bool attackOn = Input.GetMouseButton(0);
        bool attackOff = Input.GetMouseButtonUp(0);

        if ((attackOn && gun.nowbullet >= 0))
        {
            attack();
        }
        else if (attackOff || (gun.nowbullet <= 0)) 
        {
            string text = ($"{PlayerAnimParameters.Attack}");
            Shared.BattelMgr.MOVECAM.cameraShakeAnim(false);
            playerAnim.SetInteger(text, 0);
        }

        
    }
    private void lowerStateEnum()
    {
        //runOn/Off
        bool runcheck = Input.GetKeyDown(KeyCode.Mouse1);
        if (runcheck) 
        {
            runstate = !runstate;
            clearAnim();
        }

        //shit
        bool shitDown = Input.GetKeyDown(KeyCode.Z);
        if (shitDown)
        {
            shitCheack = !shitCheack;
            shitdownAnim(shitCheack);
        }

        //bool front = Input.GetKey(KeyCode.W);
        //bool right = Input.GetKey(KeyCode.A);
        //bool back = Input.GetKey(KeyCode.S);
        //bool reft = Input.GetKey(KeyCode.D);

        //if (front || right || back || reft)
        //{
        //    move();
        //}
        //else 
        //{
        //    playerAnim.SetLayerWeight(attacklayerIndex, 1.0f);
        //    clearAnim();
        //}
    }
    private void move()
    {
        //bool front = Input.GetKey(KeyCode.W);
        //bool right = Input.GetKey(KeyCode.A);
        //bool back = Input.GetKey(KeyCode.S);
        //bool reft = Input.GetKey(KeyCode.D);

        ////if (front || right || back || reft)
        ////{
        ////    //movePos.x = Input.GetAxisRaw("Horizontal");
        ////    //movePos.z = Input.GetAxisRaw("Vertical");
        ////}

        //if (!front && !right && !back && !reft)
        //{
        //    return;
        //}

        movePos.x = Input.GetAxisRaw("Horizontal");
        movePos.z = Input.GetAxisRaw("Vertical");

        moveAnim(movePos.z);
        sideWalk(movePos.x);
        //Debug.Log($"{movePos.z}");

        if (runstate)
        {
            if (movePos.z > 0)
            {
                transform.position += movePos * (moveSpeed * 2) * Time.deltaTime;
                //2 <- state
            }
            else
            {
                transform.position += movePos * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            transform.position += movePos * moveSpeed * Time.deltaTime;
        }
    }
    private void sideWalk(float _move) 
    {
        string text1 = ($"{PlayerAnimParameters.Right}");
        string text2 = ($"{PlayerAnimParameters.Left}");
        if (_move > 0) 
        {
            playerAnim.SetInteger(text1, (int)_move);
        }
        else if (_move < 0)
        {
            playerAnim.SetInteger(text2, (int)_move);
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
            string text = ($"{PlayerAnimParameters.Run}");
            playerAnim.SetInteger(text, (int)_move);
        }
        else if (_move < 0)
        {
            string text = ($"{PlayerAnimParameters.Back}");
            playerAnim.SetInteger(text, (int)_move);
        }
    }
    private void walkAnim(Animator _anim, float _move)
    {
        if (_move > 0)
        {
            string text = ($"{PlayerAnimParameters.Walk}");
            playerAnim.SetInteger(text, (int)_move);
        }
        else if (_move < 0)
        {
            string text = ($"{PlayerAnimParameters.Back}");
            playerAnim.SetInteger(text, (int)_move);
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
        string text1 = ($"{PlayerAnimParameters.Walk}");
        string text2 = ($"{PlayerAnimParameters.Back}");
        string text3 = ($"{PlayerAnimParameters.Run}");
        string text4 = ($"{PlayerAnimParameters.Right}");
        string text5 = ($"{PlayerAnimParameters.Left}");
        playerAnim.SetInteger(text1, 0);
        playerAnim.SetInteger(text2, 0);
        playerAnim.SetInteger(text3, 0);
        playerAnim.SetInteger(text4, 0);
        playerAnim.SetInteger(text5, 0);
    }
    private void shitdownAnim(bool _check)
    {
        string text = ($"{PlayerAnimParameters.Shit}");
        if (_check)
        {
            playerAnim.SetInteger(text, 1);
        }
        else 
        {
            playerAnim.SetInteger(text, 0);
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
            string text = ($"{PlayerAnimParameters.Shit}");
            playerAnim.SetInteger(text, 0);
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
        string text1 = ($"{PlayerAnimParameters.Close}");
        string text2 = ($"{animInfoName.closeAttack}");//나중에 수정 필요

        if (_check)
        {
            playerAnim.SetLayerWeight(attackLayerIndex, 1.0f);
            playerAnim.SetInteger(text1, 1);
            animCheck(text1, text2);
        }
    }
    public void animCheck(string _parameterText, string _animText) 
    {
        int index = attackLayerIndex;

        AnimatorStateInfo animStateInfo = playerAnim.GetCurrentAnimatorStateInfo(index);
        float time = animStateInfo.normalizedTime;

        Debug.Log($"{time}");
        if (time >= 1.0f && animStateInfo.IsName(_animText))
        {
            string reloading = ($"{animInfoName.reloading}");
            string closeAttack = ($"{animInfoName.closeAttack}");
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
            //Debug.Log($"{time} end");

        }
    }
}
