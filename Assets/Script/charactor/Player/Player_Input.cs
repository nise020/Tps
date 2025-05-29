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
                    walkStateChange(playerStateData.runState);
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

        if (Shared.InputManager.MoveQueData.Count == 0) 
        {
            clearWalkAnimation(playerStateData.PlayerType);
        }
        while (Shared.InputManager.MoveQueData.Count > 0)//move
        {
            if (playerStateData.AttackState == PlayerAttackState.Attack_On) 
            {
                playerStateData.AttackState = PlayerAttackState.Attack_Off;
                canReceiveInput = true;
            }
            Vector3 type = Shared.InputManager.MoveQueData.Dequeue();
            move(playerStateData.ModeState,type);
        }

        notWalkTimer += Time.deltaTime;

        if (notWalkTimer > notWalkTime &&
            playerStateData.WalkState == PlayerWalkState.Walk_On)
        {
            playerStateData.WalkState = PlayerWalkState.Walk_Off;
        }
        
    }

}
