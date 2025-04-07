using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Charactor
{
    Player followPlayerObj;
    protected Vector3 inPutPos => new Vector3(Input.GetAxisRaw("Horizontal"), 0, 
        Input.GetAxisRaw("Vertical"));
    float rotSpeed = 10.0f;//나중에 조정
    float distancingValue = 3.0f;
    Vector3 targetPos = new Vector3();
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
        playerAnim.SetInteger(PlayerAnimName.Attack.ToString(), 1);
    }
    public void Move_Npc() 
    {
        targetPos = Shared.GameManager.PlayerPos(targetPos);

        Vector3 stopPoint = new Vector3(targetPos.x, 0.0f, targetPos.z - 1.0f);
        Vector3 disTance = (stopPoint - gameObject.transform.position);

        gameObject.transform.position += disTance.normalized * speedValue * Time.deltaTime;

        if (Vector3.Distance(gameObject.transform.position, stopPoint) < 1.0f)
        {
            return;
        }
    }
    public bool TargetMove(Vector3 _pos) 
    {
        if (Vector3.Distance(transform.position,_pos) < 0.1) 
        {
            return true;
        }

        Quaternion targetRotation = Quaternion.LookRotation(_pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed);
        _pos.y = 0.0f;
        transform.position += _pos.normalized * speedValue * Time.deltaTime;
        return false;
    }
    protected override void move(CharctorStateEnum _value)//Controll
    {
        if (_value == CharctorStateEnum.Npc)
        {
            return;
        }
        else if (_value == CharctorStateEnum.Player) 
        {
            //Vector3 direction = transform.TransformDirection(inPutPos.normalized);

            if (inPutPos.magnitude > 0.1f)
            {
                //float speed = runValue ? speedValue * 2 : speedValue;
                //transform.localPosition += direction * (speed) * Time.deltaTime;
                //rigid.velocity = direction * speed;

                if (playerType == CharactorJobEnum.Warrior|| viewcam.GunModeCheck() == false)//nomal
                {
                    Vector3 moveDir = inPutPos; // World

                    Quaternion targetRotation = Quaternion.LookRotation(moveDir.normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed); // 회전속도: 10 정도 추천
                    moveDir.z = moveDir.z - distancingValue;
                    transform.position += moveDir * speedValue * Time.deltaTime;
                }
                else if(playerType == CharactorJobEnum.Gunner) 
                {
                    Transform cam = transform.GetComponentInChildren<Camera>().transform;

                    Vector3 camForward = cam.forward;
                    Vector3 camRight = cam.right;
                    camForward.y = 0;
                    camRight.y = 0;

                    Vector3 moveDir = camForward.normalized * inPutPos.z + camRight.normalized * inPutPos.x;

                    Quaternion targetRotation = Quaternion.LookRotation(moveDir.normalized);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed); // 회전속도 예: 10f
                    moveDir.z = moveDir.z - distancingValue;
                    transform.position += moveDir * speedValue * Time.deltaTime;
                }


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
