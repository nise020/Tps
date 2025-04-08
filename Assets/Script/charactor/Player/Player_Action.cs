using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Charactor
{
    Player followPlayerObj;
    
    float rotSpeed = 10.0f;//나중에 조정
    float distancingValue = 3.0f;
    Vector3 targetPos = new Vector3();
    SkillRunning skillCheck = SkillRunning.SkillOff;

    [SerializeField] Vector3 playerStopDistansePos;
    int movetimeCount;
    float timeValue;
    Queue <Time> fsmMoveTime = new Queue<Time>();
    List<GameObject> backPositionObject;//my position Object

    protected override void skillAttack(CharactorJobEnum _type)
    {
        if (Skill1)
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
        else if (Skill2)
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
    protected override void attack(CharctorStateEnum _state)
    {
        if (_state == CharctorStateEnum.Npc)
        {
            //Vector3 pos = new Vector3();
            //pos = Shared.GameManager.FindPlayer(pos);
        }
        else if (_state == CharctorStateEnum.Player) 
        {
            if (playerType == CharactorJobEnum.Gunner)
            {
                GUN = GetComponentInChildren<Gun>();
                if (GUN.reLoed == false && GUN.nowbullet >= 0)
                {
                    Vector3 AimDirection = GUN.gameObject.transform.forward;
                    playerAnim.SetLayerWeight(attackLayerIndex, 1.0f);
                    AttackAnim(1);
                    GUN.Attack(viewcam, AimDirection);
                }
            }
            else if (playerType == CharactorJobEnum.Warrior)
            {
                //closeAttackCheack();
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

    }

}
