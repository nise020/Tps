using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    protected Transform charactorModelTrs;//Modeling
    protected float rotationSpeed = 20.0f;//나중에 조정
    protected float skillCool_1;//1번 스킬쿨타임
    protected float skillCool_2;//2번 스킬쿨타임
    protected float buff;//버프
    protected float burstCool;//버스트 쿨타임
    public Transform BodyObjectLoad() 
    {
        return charactorModelTrs;
    }

    protected virtual void OnTriggerEnter(Collider other)//세분화 필요
    {
        Collider myColl = gameObject.GetComponent<Collider>();
        if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerName.Monster))//몬스터일 경우
        {
            if (other.gameObject.layer == Delivery.LayerNameEnum(LayerName.Player))
            {
                //Attack();
            }
            else if (other.gameObject.layer == Delivery.LayerNameEnum(LayerName.Bullet))//피격
            {
                checkHp(other);
            }
        }
        else if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerName.Player))//플레이어 일 경우
        {
            if (other.gameObject.layer == Delivery.LayerNameEnum(LayerName.Monster))//피격
            {
                checkHp(other);
            }
        }
    }
    protected HpBar HPBAR = new HpBar();
    public void HpInIt(HpBar _hpBar)
    {
        HPBAR = _hpBar;
    }
    public void StatusUpLoad(float _hp) 
    {
        hP = _hp;
        if (hP <= 0) 
        {
            dead();
        }
        cheHP = hP;
        HPBAR.SetHp(maxHP, cheHP);
    }
    protected virtual void stateInIt() 
    {
        hP = STATUS.ViewHp;
        cheHP = hP;
        maxHP = hP;
        speedValue = STATUS.ViewSpeed;
        atkValue = STATUS.ViewAttack;
        defVAlue = STATUS.ViewDefense;
    }
    protected void footRayCheck() //중력구현
    {
        //init
        //update
        //Renderring
    }
    protected void FindSkinBodyObject()
    {
        SkinnedMeshRenderer skin = GetComponentInChildren<SkinnedMeshRenderer>();
        charactorModelTrs = skin.transform.parent;
        Debug.Log($"{gameObject}\ncharactorModelTrs = {charactorModelTrs}");
    }
    protected void FindMeshBodyObject()
    {
        MeshRenderer skin = GetComponentInChildren<MeshRenderer>();
        charactorModelTrs = skin.transform.parent;
        Debug.Log($"{gameObject}\ncharactorModelTrs = {charactorModelTrs}");
    }
    protected virtual void checkHp(Collider other) //수정 필요
    {
        //if (cheHP == hP) return;
        //Damage(other);
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
    protected virtual void search() {}
    protected virtual void moveAnimation(MonsterWalkState _state) {}
    protected virtual void attackAnimation(MonsterAttackState _state) {}
    protected virtual void move(CharctorStateEnum _value,Vector3 _pos) 
    {

    }
    protected virtual void move(CharctorStateEnum _value, Player _player)
    {

    }
    protected virtual void attack(CharctorStateEnum _state, CharactorJobEnum _job) 
    {

    }
    protected virtual void commonskillAttack1(CharactorJobEnum _type) 
    {

    }
    protected virtual void commonskillAttack2(CharactorJobEnum _type)
    {

    }
    public Status StateLoad()
    {
        Status state = STATUS;
        return state;
    }

}
