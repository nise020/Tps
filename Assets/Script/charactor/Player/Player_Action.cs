using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;

public partial class Player : Charactor
{
    Player followPlayerObj;

    float distancingValue = 3.0f;
    protected SkillRunning firstSkillCheck = SkillRunning.SkillOff;
    protected SkillRunning secondSkillCheck = SkillRunning.SkillOff;
    protected PlayerCameraMode cameraMode = PlayerCameraMode.CameraRotationMode;
    [SerializeField] Vector3 playerStopDistansePos;
    int movetimeCount;
    float timeValue;
    Queue <Time> fsmMoveTime = new Queue<Time>();
    List<GameObject> backPositionObject;//my position Object

    protected virtual void skillValueReset()//Damage Reset
    {
        attackValue = attackReset;
        firstSkillCheck = SkillRunning.SkillOff;
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
    
    public bool Move_Attack()//몬스터 한테 따라가기
    {
        return false;
    }
    public void AutoAttack()//거리이내에 있는 적에게 데미지 로직 필요
    {
        attackAnimation(AttackState.AttackOn);
    }
    public void Attack()//거리이내에 있는 적에게 데미지 로직 필요
    {
        attackAnimation(AttackState.AttackOn);
    }
    
}
