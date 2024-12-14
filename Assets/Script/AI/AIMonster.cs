using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public partial class AiMonster : AiBase
{
    //Monster_Skill SKILL;
    bool attackOn = true;
    int targetNumber;
    //Monster_Skill SKILL = new Monster_Skill();
  
    //private eMobType MOBTYPE;
    //Monster Monster;
    //FSM
    //캐릭터에서 AI를 호출할 필요


    public void Pattern()
    {
        switch (MobType)
        {
            case eMobType.Defolt://일반 평타(횟수제한)
                SKILL.NomalAttack(targetNumber, MONSTER.MobBullet, MONSTER.soljerObj, MONSTER.AttackArm.transform.position,
                    MONSTER.creatTabObj);
                break;
            case eMobType.Flying://자폭 공격
                MONSTER.gameObject.transform.position = SKILL.DirectAttackSkill
                    (targetNumber, MONSTER.soljerObj, MONSTER.gameObject.transform.position);
                break;
            case eMobType.Huge://자폭 공격
                SKILL.JumpSkill(targetNumber, MONSTER.soljerObj, ref attackOn, 
                    MONSTER.gameObject.transform.position, MONSTER.mobRigid);
                break;
        }
    }

    public override void State(ref eAI _aIState)
    {
        switch (_aIState)
        {
            case eAI.Create:
                Create();
                break;
            case eAI.Search:
                Search();
                break;
            case eAI.Move:
                Move();
                break;
            case eAI.Attack:
                Attack();
                break;
            case eAI.Reset:
                Reset();
                break;
        }
        _aIState = aIState;
    }
    protected override void Create()//생성
    {
        //SKILL = new Monster_Skill();
        aIState = eAI.Search;
    }
    protected override void Search()//공격할 대상 찾기
    {

        //필요한 기능
        //1.타이머
        //2.타이머 기능이 끝날때 타겟 좌표 확정(이후 위치 바꿔도 따라가는 기능이 아님)
        if (MONSTER == null)
        {
            Debug.LogError("MONSTER가 null입니다.");
            return;
        }

        if (SKILL == null)
        {
            Debug.LogError("SKILL이 null입니다.");
            return;
        }

        SKILL.targetOn(ref targetNumber,MONSTER.soljerObj);
        aIState = eAI.Attack;
    }
    protected override void Attack()//공격
    {
        Pattern();
        if (nextPatternOn == true)
        {
            nextPatternOn = false;
            aIState = eAI.Reset;
        }
    }
    protected override void Reset()//사이클 끝(보통 다시 공격 대상 탐색)
    {
        attackOn = true;
        targetNumber = 0;
        aIState = eAI.Search;
    }
    protected override void Move()//이동
    {
        switch (MobType)
        {
            case eMobType.Defolt:
                //Defolt.nomalAttack();
                break;
        }
        if (nextPatternOn == true)
        {
            nextPatternOn = false;
            base.Attack();
        }
    }


}
