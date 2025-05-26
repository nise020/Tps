using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.SceneView;

public partial class Player : Character
{
    protected virtual void skillValueReset()//Damage Reset
    {
        atkValue = attackReset;
        PlayerStateData.firstSkillCheck = SkillState.SkillOff;
        playerAnimtor.SetInteger(PlayerAnimName.AttackSkill.ToString(), 0);
        playerAnimtor.SetInteger(PlayerAnimName.BuffSkill.ToString(), 0);
    }
    protected virtual void cameraModeChange() 
    {
        if (cameraMode == PlayerCameraMode.CameraRotationMode) 
        {
            cameraMode = PlayerCameraMode.GunAttackMode;
            viewcam.CameraModeInit(cameraMode);
        }
        else if (cameraMode == PlayerCameraMode.GunAttackMode)
        {
            cameraMode = PlayerCameraMode.CameraRotationMode;
            viewcam.CameraModeInit(cameraMode);
        }
    }
    
    public bool Move_Attack()//몬스터 한테 따라가기
    {
        return false;
    }

    public void Attack()//거리이내에 있는 적에게 데미지 로직 필요
    {
        attackAnimation(AttackState.AttackOn);
    }
    
}
