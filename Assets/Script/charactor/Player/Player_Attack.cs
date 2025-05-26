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
    
    public bool Move_Attack()//���� ���� ���󰡱�
    {
        return false;
    }

    public void Attack()//�Ÿ��̳��� �ִ� ������ ������ ���� �ʿ�
    {
        attackAnimation(AttackState.AttackOn);
    }
    
}
