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
    protected float hP;//보여지는 체력
    protected float cheHP;//체크할 체력
    protected float maxHP;//최대체력
    [SerializeField] protected float CharactorId = 0;//최대체력
    [SerializeField] GameObject hpBar;//uiHp
    State state = new State();

    protected float skillCool_1;//1번 스킬쿨타임
    protected float skillCool_2;//2번 스킬쿨타임
    protected float buff;//버프
    protected float burstCool;//버스트 쿨타임

    protected void inIt() 
    {
        hP = state.MaxHP;
        cheHP = hP;
        maxHP = hP;
    }

    protected override void OnTriggerEnter(Collider other) 
    {
        //base.OnTriggerEnter(other);

    }

    public void Hit() 
    {

    }

    protected virtual void dead() //사망 상태
    {

    }
    protected virtual void move() 
    {

    }
    protected virtual void attack() 
    {

    }
}
