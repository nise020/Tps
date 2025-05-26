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
                    walkStateChange(PlayerStateData.runState);
                    break;
                case KeyCode.R:
                    commonRSkill(PlayerStateData.PlayerType);
                    break;
                case KeyCode.Q:
                    commonskillAttack1(PlayerStateData.PlayerType);//SkillQ
                    break;
                case KeyCode.E:
                    commonskillAttack2(PlayerStateData.PlayerType);//SkillE
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
                    attack(PlayerStateData.PlayerState, PlayerStateData.PlayerType);
                    break;
                case MouseInputType.Release://mouseClickUp
                    //inPutCameraAnimation(false)
                    ;
                    break;
                case MouseInputType.Hold://mouseClickDown
                    attack(PlayerStateData.PlayerState, PlayerStateData.PlayerType);
                    //inPutCameraAnimation(true)
                    ;
                    break;
            }
                
        }

        if (Shared.InputManager.MoveQueData.Count == 0) 
        {
            clearWalkAnimation(PlayerStateData.PlayerType);
        }
        while (Shared.InputManager.MoveQueData.Count > 0)//move
        {
            Vector3 type = Shared.InputManager.MoveQueData.Dequeue();
            move(PlayerStateData.PlayerState,type);
        }

        notWalkTimer += Time.deltaTime;

        if (notWalkTimer > notWalkTime &&
            PlayerStateData.PlayerWalkState == PlayerWalkState.Walk_On)
        {
            PlayerStateData.PlayerWalkState = PlayerWalkState.Walk_Off;
        }
        
    }

}
