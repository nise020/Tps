using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AIMonster : AiBase
{
    protected override void Type() 
    {
        switch (MobType)
        {
            case eMobType.Defolt:
                Defolt = GetComponent<DefoltMob>();//class
                break;
            case eMobType.Flying:
                Flying = GetComponent<FlyingMob>();//class
                break;
            case eMobType.Huge:
                Huge = GetComponent<HugeMob>();//class
                break;
        }
    }

    public void pattern() 
    {

    }
    protected override void Create()//생성
    {
        Type();
        if (nextPatternOn == true)
        {
            nextPatternOn = false;
            base.Search();
        }
    }
    protected override void Search()//공격할 대상 찾기
    {
        //타켓 찾기 추가
        if (nextPatternOn == true)
        {
            nextPatternOn = false;
            base.Move();
        }
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
    protected override void Attack()//공격
    {
        switch (MobType)
        {
            case eMobType.Defolt:
                Defolt.attack();
                break;
            case eMobType.Flying:
                Flying.DirectAttack();
                break;
            case eMobType.Huge:
                Huge.jumpAttack();
                break;
        }
        if (nextPatternOn==true) 
        {
            nextPatternOn = false;
            base.Reset();
        }
        
        
    }
    protected override void Reset()//사이클 끝(보통 다시 공격 대상 탐색)
    {
        if (nextPatternOn == true)
        {
            nextPatternOn = false;
            base.Move();
        }
    }
}
