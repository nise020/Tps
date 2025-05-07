using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.SceneView;

public partial class Player : Charactor
{
    Player followPlayerObj;

    float distancingValue = 3.0f;
    protected SkillState firstSkillCheck = SkillState.SkillOff;
    protected SkillState secondSkillCheck = SkillState.SkillOff;
    protected PlayerCameraMode cameraMode = PlayerCameraMode.CameraRotationMode;
    [SerializeField] Vector3 playerStopDistansePos;
    int movetimeCount;
    float timeValue;
    Queue <Time> fsmMoveTime = new Queue<Time>();
    List<GameObject> backPositionObject;//my position Object

    protected virtual void skillValueReset()//Damage Reset
    {
        atkValue = attackReset;
        firstSkillCheck = SkillState.SkillOff;
        playerAnim.SetInteger(PlayerAnimName.AttackSkill.ToString(), 0);
        playerAnim.SetInteger(PlayerAnimName.BuffSkill.ToString(), 0);
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
