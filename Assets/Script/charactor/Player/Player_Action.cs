using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Charactor
{
    protected Vector3 movePos => new Vector3(Input.GetAxisRaw("Horizontal"), 0, 
        Input.GetAxisRaw("Vertical"));
    protected bool Skill1 => Input.GetKeyDown(KeyCode.Q);
    protected bool Skill2 => Input.GetKeyDown(KeyCode.E);
    SkillRunning skillCheck = SkillRunning.SkillOff;
    protected override void skillAttack(PlayerjobEnum _type)
    {
        if (Skill1)
        {
            if (skillCheck == SkillRunning.SkillOff)
            {
                //skillStrategy.Skill(playerType, 1, attackValue);
                skillCheck = SkillRunning.SkillOn;
                //playerAnim.SetInteger("Skill1", 1);
                playerAnim.SetInteger("AttackSkill", 1);
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
                playerAnim.SetInteger("BuffSkill", 1);
                Invoke("SkillValueReset", 3);//clear
            }
            else
            {
                return;
            }
        }
        else { return; }
    }
    protected void SkillValueReset()
    {
        attackValue = attackReset;
        skillCheck = SkillRunning.SkillOff;
    }
    protected override void attack(CharctorStateEnum _state)
    {
        if (_state == CharctorStateEnum.Npc)
        {
        }
        else if (_state == CharctorStateEnum.Player) 
        {
            if (playerType == PlayerjobEnum.Gunner)
            {
                gun = GetComponentInChildren<Gun>();
                if (gun.reLoed == false && gun.nowbullet >= 0)
                {
                    Vector3 AimDirection = gun.gameObject.transform.forward;
                    playerAnim.SetLayerWeight(attackLayerIndex, 1.0f);
                    AttackAnim(1);
                    gun.Attack(viewcam, AimDirection);
                }
            }
            else if (playerType == PlayerjobEnum.Warrior)
            {
                closeAttackCheack();
            }
        }
    }
    protected override void move(CharctorStateEnum _value)
    {
        if (_value == CharctorStateEnum.Npc)
        {
            //return;
        }
        else if (_value == CharctorStateEnum.Player) 
        {
            Vector3 direction = transform.TransformDirection(movePos.normalized);

            if (movePos.magnitude > 0.1f)
            {
                float speed = runValue ? speedValue * 2 : speedValue;
                transform.localPosition += direction * (speed) * Time.deltaTime;
                //rigid.velocity = direction * speed;
            }
            else
            {
                return;
            }
        }
        
        //All
        //moveAnim(movePos.z);
        //Gunner
        //sideWalkAnim(movePos.x, playerType);
    }

    public void reloding(PlayerjobEnum _type)
    {
        if (_type != PlayerjobEnum.Gunner) { return; }

        if (reloadOn || gun.nowbullet <= 0)
        {
            playerAnim.SetLayerWeight(attackLayerIndex, 1.0f);
            playerAnim.SetInteger("Reload", 1);
        }

        //if (gun.reLoed)
        //{
        //    animCheck("Reload", "reloading");
        //}

    }

    IEnumerator reLoadout(int _index)
    {
        gun.nowbullet = gun.bullet;
        gun.bulletcount = 0;
        playerAnim.SetLayerWeight(_index, 0.0f);
        gun.reLoed = false;
        yield return null;
    }

    public void attackRot()
    {
        Vector3 pos = Shared.BattelManager.CamAim.transform.forward;
        transform.rotation = Quaternion.Euler(pos);
    }
}
