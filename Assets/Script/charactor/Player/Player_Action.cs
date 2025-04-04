using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Charactor
{
    Player followPlayerObj;
    protected Vector3 movePos => new Vector3(Input.GetAxisRaw("Horizontal"), 0, 
        Input.GetAxisRaw("Vertical"));
    protected bool Skill1 => Input.GetKeyDown(KeyCode.Q);
    protected bool Skill2 => Input.GetKeyDown(KeyCode.E);
    SkillRunning skillCheck = SkillRunning.SkillOff;
    protected override void skillAttack(CharactorJobEnum _type)
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
    public void PlayerItit(Player _player)
    {
        followPlayerObj = _player;
    }
    public void Move_Npc() 
    {
        Vector3 pos = new Vector3();
        pos = Shared.GameManager.PlayerPos(pos);
        Vector3 disTance = (gameObject.transform.position - pos) * speedValue * Time.deltaTime;
        gameObject.transform.position += disTance;
        if (Vector3.Distance(gameObject.transform.position, pos) > 0.5f)
        {
            return;
        }
    }
    public bool Move_Attack()//몬스터 한테 따라가기
    {
        return false;
    }
    public void AutoAttack() 
    {

    }
    protected override void move(CharctorStateEnum _value)//Controll
    {
        if (_value == CharctorStateEnum.Npc)
        {
            Move_Npc();
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

    public void reloding(CharactorJobEnum _type)
    {
        if (_type != CharactorJobEnum.Gunner) { return; }

        if (reloadOn || GUN.nowbullet <= 0)
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
        GUN.nowbullet = GUN.bullet;
        GUN.bulletcount = 0;
        playerAnim.SetLayerWeight(_index, 0.0f);
        GUN.reLoed = false;
        yield return null;
    }

    public void attackRot()
    {
        Vector3 pos = Shared.BattelManager.CamAim.transform.forward;
        transform.rotation = Quaternion.Euler(pos);
    }
}
