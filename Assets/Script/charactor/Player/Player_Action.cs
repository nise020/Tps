using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;

public partial class Player : Charactor
{
    Player followPlayerObj;
    
    float rotSpeed = 20.0f;//나중에 조정
    float distancingValue = 3.0f;
    SkillRunning skillCheck = SkillRunning.SkillOff;
    PlayerCameraMode cameraMode = PlayerCameraMode.CameraRotationMode;
    [SerializeField] Vector3 playerStopDistansePos;
    int movetimeCount;
    float timeValue;
    Queue <Time> fsmMoveTime = new Queue<Time>();
    List<GameObject> backPositionObject;//my position Object

    protected override void skillAttack1(CharactorJobEnum _type)
    {
        if (_type == CharactorJobEnum.Warrior)
        {
            if (skillCheck == SkillRunning.SkillOff)
            {
                //skillStrategy.Skill(playerType, 1, attackValue);
                skillCheck = SkillRunning.SkillOn;
                //playerAnim.SetInteger("Skill1", 1);
                playerAnim.SetInteger(PlayerAnimName.AttackSkill.ToString(), 1);
                Invoke("SkillValueReset", 3);//clear
            }
            else
            {
                return;
            }
        }
        else if (_type == CharactorJobEnum.Gunner)
        {
            if (skillCheck == SkillRunning.SkillOff)
            {
                skillStrategy.Skill(playerType, 2, attackValue);
                skillCheck = SkillRunning.SkillOn;
                //playerAnim.SetInteger("Skill1", 1);
                playerAnim.SetInteger(PlayerAnimName.BuffSkill.ToString(), 1);
                Invoke("SkillValueReset", 3);//clear
            }
            else
            {
                return;
            }
        }
        else { return; }
    }
    protected override void skillAttack2(CharactorJobEnum _type)
    {
        if (_type == CharactorJobEnum.Warrior)
        {
            if (skillCheck == SkillRunning.SkillOff)
            {
                //skillStrategy.Skill(playerType, 1, attackValue);
                skillCheck = SkillRunning.SkillOn;
                //playerAnim.SetInteger("Skill1", 1);
                playerAnim.SetInteger(PlayerAnimName.AttackSkill.ToString(), 1);
                Invoke("SkillValueReset", 3);//clear
            }
            else
            {
                return;
            }
        }
        else if (_type == CharactorJobEnum.Gunner)
        {
            if (skillCheck == SkillRunning.SkillOff)
            {
                skillStrategy.Skill(playerType, 2, attackValue);
                skillCheck = SkillRunning.SkillOn;
                //playerAnim.SetInteger("Skill1", 1);
                playerAnim.SetInteger(PlayerAnimName.BuffSkill.ToString(), 1);
                Invoke("SkillValueReset", 3);//clear
            }
            else
            {
                return;
            }
        }
        else { return; }
    }
    protected void SkillValueReset()//Damage Reset
    {
        attackValue = attackReset;
        skillCheck = SkillRunning.SkillOff;
        playerAnim.SetInteger(PlayerAnimName.AttackSkill.ToString(), 0);
        playerAnim.SetInteger(PlayerAnimName.BuffSkill.ToString(), 0);
    }
    protected void cameraModeChange() 
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
    protected override void attack(CharctorStateEnum _state, CharactorJobEnum _job)
    {
        if (_state == CharctorStateEnum.Player) 
        {
            if (_job == CharactorJobEnum.Gunner)
            {
                if (reloadState == ReloadState.ReloadOn || GUN.nowbullet <= 0) 
                {
                    Debug.Log($"bullet = {GUN.nowbullet}");
                    return ;
                }
                attackState = AttackState.AttackOn;
                Vector3 AimDirection = GUN.gameObject.transform.forward;
                playerAnim.SetLayerWeight(attackLayerIndex, 1.0f);
                AttackAnim(1);
                GUN.Attack(viewcam, AimDirection);
                playerAnim.SetLayerWeight(attackLayerIndex, 0.0f);
            }
            else if (_job == CharactorJobEnum.Warrior)
            {
                AttackAnim(1);
            }
        }
    }
    public bool Move_Attack()//몬스터 한테 따라가기
    {
        return false;
    }
    public void AutoAttack()
    {
        AttackAnim(1);
    }

}
