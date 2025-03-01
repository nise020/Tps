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
    protected float hP;//실제 체력
    protected float cheHP;//보여지는 체력
    protected float maxHP;//최대체력
    [SerializeField] protected float CharactorId = 0;//최대체력
    [SerializeField] GameObject hpBar;//uiHp
    protected State STATE = new State();

    protected float skillCool_1;//1번 스킬쿨타임
    protected float skillCool_2;//2번 스킬쿨타임
    protected float buff;//버프
    protected float burstCool;//버스트 쿨타임
    [SerializeField] CharactorType type = CharactorType.None;


    protected void inIt() 
    {
        hP = STATE.MaxHP;
        cheHP = hP;
        maxHP = hP;
    }

    protected override void OnTriggerEnter(Collider other) 
    {
        base.OnTriggerEnter(other);
    }

    public void Hit() 
    {

    }
    protected virtual void hpCheck() 
    {
        if (cheHP == hP) return;
        if (cheHP >= hP)
        {
            cheHP = hP;
            if (hP==0) 
            {
                dead();
            }
        }
    }
    protected virtual void dead() //사망 상태
    {
        if (type == CharactorType.Player) 
        {
            Shared.BattelMgr.PlayerAlive = false;
            gameObject.SetActive(false);
        }
        else 
        {
            gameObject.SetActive(false);
        }
    }
    protected virtual void move() 
    {

    }
    protected virtual void attack() 
    {

    }
}
