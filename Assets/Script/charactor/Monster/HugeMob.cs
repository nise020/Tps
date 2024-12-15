using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HugeMob : Monster
{
    protected override void Start()
    {
        eType = eMobType.Huge;
        base.Start();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    void Update()
    {
        groundCheck = groundOn_Off(groundCheck);
        //SKILL.NomalAttack(playerObj, number,ref AttackCount,AttackMaxCount,
        //    MobBullet,AttackArm.transform.position,creatTabObj);
        //SKILL.JumpSkill(jumpOn,ref moveTimer, runningTime, targetCheack,number,playerObj,gameObject,jumpHight,mobRigid);
        //base.nomalAttack();
        //float gravity = Mathf.Abs(Physics.gravity.y);
        //moving();
        //StartCoroutine(MobAttackTimecheck());
    }
}
