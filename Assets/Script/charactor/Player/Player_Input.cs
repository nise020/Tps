using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Player : Character
{
    protected void inputrocessing() 
    {
        while (Shared.InputManager.KeyinPutQueData.Count > 0)//key 
        {

            KeyCode type = Shared.InputManager.KeyinPutQueData.Dequeue();

            switch (type)
            {
                case KeyCode.Mouse1:
                    //walkStateChange(playerStateData.runState);
                    AvoidanceCheck();
                    break;
                case KeyCode.R:
                    commonRSkill(playerStateData.PlayerType);
                    break;
                case KeyCode.Q:
                    skillAttack_common1(playerStateData.PlayerType);//SkillQ
                    break;
                case KeyCode.E:
                    skillAttack_common2(playerStateData.PlayerType);//SkillE
                    break;
                case KeyCode.Z:
                    shitdownCheak();//shitdown
                    break;
                case KeyCode.Space:
                    //cameraModeChange();
                    break;
                case KeyCode.LeftControl:
                    blockCheck();
                    break;
            }
        }

        while (Shared.InputManager.MouseInputQueData.Count > 0)//mouseClick == Attack
        {
            MouseInputType type = Shared.InputManager.MouseInputQueData.Dequeue();
            switch (type) 
            {
                case MouseInputType.Click://mouseClick
                    //attack(PlayerStateData.PlayerState, PlayerStateData.PlayerType);
                    break;
                case MouseInputType.Release://mouseClickUp
                    //inPutCameraAnimation(false)
                    ;
                    break;
                case MouseInputType.Hold://mouseClickDown
                    attack(playerStateData.ModeState, playerStateData.PlayerType);
                    //inPutCameraAnimation(true)
                    ;
                    break;
            }
                
        }

        if (Shared.InputManager.MoveQueData.Count != 0)
        {
            //notWalkTimer = 0.0f;

            while (Shared.InputManager.MoveQueData.Count > 0)//move
            {
                Vector3 type = Shared.InputManager.MoveQueData.Dequeue();


                if (playerStateData.AttackState == AttackState.Attack_On)
                {
                    playerStateData.AttackState = AttackState.Attack_Off;

                    canReceiveInput = true;
                }

                if (walkStateChangeTimer >= walkStateChangeTime)
                {
                    playerStateData.WalkState = PlayerWalkState.Run;
                }
                else if(walkStateChangeTimer < walkStateChangeTime)
                {
                    playerStateData.WalkState = PlayerWalkState.Walk;

                    walkStateChangeTimer += Time.deltaTime;
                }

                if (playerStateData.WalkState != PlayerWalkState.Dash)
                {
                    move(playerStateData.ModeState, type);
                }
                else { return; }


            }
        }
        else 
        {
            playerStateData.WalkState = PlayerWalkState.Stop;
            clearWalkAnimation();

            walkStateChangeTimer = 0.0f;
            //notWalkTimer += Time.deltaTime;

            //if (notWalkTimer > notWalkTime &&
            //playerStateData.WalkState != PlayerWalkState.Stop)
            //{
            //    playerStateData.WalkState = PlayerWalkState.Stop;
            //}
        }
        
    }

    
}
