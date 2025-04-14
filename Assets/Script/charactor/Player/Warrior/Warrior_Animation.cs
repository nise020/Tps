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
}
