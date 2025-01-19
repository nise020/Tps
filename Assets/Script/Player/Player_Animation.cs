using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Charactor
{
    [Header("Animator Info")]
    int baselayerIndex = 0;
    int attacklayerIndex = 1;
    int shortSwordIndex = 2;
    [SerializeField] bool shitCheack = false;
    [SerializeField] bool closeCheck = false;
    private void move()
    {
        movePos.x = Input.GetAxisRaw("Horizontal");
        movePos.z = Input.GetAxisRaw("Vertical");
        moveAnim(movePos.z);
        if (runstate)
        {
            if (movePos.z > 0)
            {
                transform.position += movePos * (moveSpeed * 2) * Time.deltaTime;
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
        playerAnim.SetInteger(text1, 0);
        playerAnim.SetInteger(text2, 0);
        playerAnim.SetInteger(text3, 0);
    }
    private void shitdownAnim(bool _cheak)
    {
        string text = ($"{PlayerAnimParameters.Shit}");
        if (_cheak)
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
            //playerAnim.SetLayerWeight(attacklayerIndex, 1.0f);
        }
    }

}
