using Photon.Pun.Demo.SlotRacer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Charactor : Actor
{
    //델리게이트
    //지연이 걸리는 부분에 사용

    //Action: 패치관련
    //Funtion
    //Task: ADK,동기화,싱크(async)
    //Const


    //대리자
    //system.Func<object,bool> UpteAction = null;
    //UpteAction = 함수연결
    //system.Action<bool> FinishAction = null;
    //FinishAction = 함수연결
    //delegaet void CallBack();
    //CallBack callback = null;
    //callback = 함수연결
    //async -> await // 비동기
    //변수 호출시 연결된 함시 실행



    //캐릭터
    //스텟 사용
    //clone 오브젝트 적극 사용 
    //protected int ID;//자신의 ID
    protected float hP;//실제 체력
    protected float cheHP;//보여지는 체력
    protected float maxHP;//최대체력
    [SerializeField] protected float CharactorId = 0;
    [SerializeField] GameObject hpBar;//uiHp


    protected float skillCool_1;//1번 스킬쿨타임
    protected float skillCool_2;//2번 스킬쿨타임
    protected float buff;//버프
    protected float burstCool;//버스트 쿨타임

    protected virtual void OnTriggerEnter(Collider other)//세분화 필요
    {
        Collider myColl = gameObject.GetComponent<Collider>();
        if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Monster))//몬스터일 경우
        {
            if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Player))
            {
                attack();
            }
            else if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Bullet))//피격
            {
                checkHp();
            }
        }
        else if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Player))//플레이어 일 경우
        {
            if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Monster))//피격
            {
                checkHp();
            }
        }
    }
    protected void stateInIt() 
    {
        hP = STATE.MaxHP;
        cheHP = hP;
        maxHP = hP;
        speedValue = STATE.Movespeed;
        atkValue = STATE.Attack;
        defVAlue = STATE.Defense;
    }


    protected virtual void checkHp() 
    {
        //if (cheHP == hP) return;
        hP = hP - 1;//1은 바꿔야함
        Debug.Log("Hit");
        if (cheHP >= hP && hP >= 0)
        {
            cheHP = hP;
            if (hP==0) 
            {
                Invoke("dead",1f);
            }
        }
    }
    protected virtual void dead() //사망 상태
    {
        if (objType == ObjectType.Player) 
        {
            Shared.BattelManager.PlayerAlive = false;
            hP = maxHP;
            cheHP = maxHP;
            gameObject.SetActive(false);
        }
        else 
        {
            hP = maxHP;
            cheHP = maxHP;
            gameObject.SetActive(false);
        }
    }
    protected virtual void move(PlayerControll _value) 
    {

    }
    protected virtual void attack() 
    {

    }
}
