using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Warrior : Player
{
    protected override void walkAnim(RunState _runState, Vector3 _pos)
    {
        if (_runState == RunState.Walk)
        {
            if (playerWalkState == PlayerWalkState.Walk_On) { return; }

            playerWalkState = PlayerWalkState.Walk_On;
            playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 1);
        }
        else if (_runState == RunState.Run)
        {
            if (playerRunState == PlayerRunState.Run_On) { return; }

            playerRunState = PlayerRunState.Run_On;
            playerAnim.SetInteger(PlayerAnimParameters.Run.ToString(), 1);
        }
    }

    protected override void clearWalkAnim(CharactorJobEnum _type)
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

        if (_type == CharactorJobEnum.Warrior)
        {
            playerAnim.SetInteger(PlayerAnimParameters.Walk.ToString(), 0);
        }
    }
}
