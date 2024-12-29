using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Charactor : Actor
{
    //캐릭터
    //스텟 사용
    //clone 오브젝트 적극 사용 
    //protected int ID;//자신의 ID
    protected float HP;//보여지는 체력
    protected float cheHP;//체크할 체력
    protected float maxHP;//최대체력



    protected float skillCool_1;//1번 스킬쿨타임
    protected float skillCool_2;//2번 스킬쿨타임
    protected float buff;//버프
    protected float burstCool;//버스트 쿨타임
    public void NowHp()//스타트에서 한번만 실행 
    {
        HP = maxHP;
        cheHP = HP;
    }
    public void CheckHp()//cheHP가 우선적으로 소모
    {
        if (HP == cheHP) { return; }
        //코루틴 으로 수정 예정
        if (HP != cheHP && HP >= 0) 
        {
            HP = cheHP;
        }
    }
    protected void dead() //사망
    {

    }

}
