using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract partial class Charactor : Actor
{
    protected HpBar HPBAR = new HpBar();
    protected float hP;//실제 체력
    protected float cheHP;//보여지는 체력
    protected float maxHP;//최대체력
    //protected int power;//힘
    [SerializeField] protected float CharactorId = 0;
    [SerializeField] GameObject hpBar;//uiHp

    protected Transform charactorModelTrs;//Modeling
    protected Transform RootTrransform;//RootModel

    protected float rotationSpeed = 20.0f;//나중에 조정
    protected float skillCool_1;//1번 스킬쿨타임
    protected float skillCool_2;//2번 스킬쿨타임
    protected float buff;//버프
    protected float burstCool;//버스트 쿨타임
    StatusType statusType = StatusType.None;
    protected Condition condition = Condition.health;//상태패턴


    [SerializeField] protected GameObject HandObj;
    [SerializeField] protected GameObject weaponObj;
    protected Vector3 weaponOriginalPos = Vector3.zero;
    protected ObjectRenderType RenderType = ObjectRenderType.None;

    protected InvincibleState characterstate = InvincibleState.invincible_Off;
    public void HpInIt(HpBar _hpBar)
    {
        HPBAR = _hpBar;
    }

    protected void hbBarCheck(bool _check)
    {
        if (_check)
        {
            HPBAR.gameObject.SetActive(true);
        }
        else
        {
            HPBAR.gameObject.SetActive(false);
        }
    }

    public void StatusUpLoad(float _hp)
    {
        if (condition == Condition.Death) {return;}
        if (condition != Condition.Death) 
        {
            hP = _hp;
            cheHP = hP;
            hbBarCheck(true);

            HPBAR.SetHp(maxHP, cheHP);


            if (hP <= 0)
            {
                Debug.Log("Dead");
                Invoke("death", 1f);
                return;
            }
            invincibleState();
        }

    }

    protected void invincibleState()
    {
        characterstate = InvincibleState.invincible_On;
        Invoke("invincibleOut", 1.0f);
    }
    protected void invincibleOut()
    {
        //if (hP == 0) return;
        characterstate = InvincibleState.invincible_Off;

        if (hP < maxHP / 2)
        {
            condition = Condition.hard;
        }
        else
        {
            condition = Condition.health;
        }
    }

    public void conditionUpdate(Condition _condition) 
    {
        if (condition == Condition.Death) 
        {
            condition = _condition;
        }
    }
    public int StatusTypeLoad(StatusType _type) 
    {
        int value = 0;
        switch (_type)
        {
            case StatusType.HP:
                value = (int)hP;
                break;
            case StatusType.Power:            
                value = (int)atkValue;
                break;
            case StatusType.Speed:
                value = (int)speedValue;
                break;
            case StatusType.Defens:
                value = (int)defVAlue;
                break;
        }
        return value;
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
    protected virtual void checkHp(Collider other) //수정 필요
    {
        Debug.Log("Hit");
        if (cheHP >= hP && hP >= 0)
        {
            cheHP = hP;
            if (hP == 0)
            {
                Invoke("death", 1f);
            }
        }
    }
    protected virtual void death() //사망 상태
    {
        if (objType == ObjectType.Player)
        {
            Shared.BattelManager.PlayerAlive = false;

            hP = maxHP;
            cheHP = maxHP;
        }
        else//+Item,Effect
        {
            hP = maxHP;
            cheHP = maxHP;

            Shared.EffectManager.Play(EffectType.BoomEffect, charactorModelTrs.position);
        }
    }

    protected virtual void FindWeaponObject(LayerName _name)
    {

    }
    protected void FindBodyObjectType(ObjectRenderType _renderType)
    {
        if (_renderType == ObjectRenderType.Skin) 
        {
            SkinnedMeshRenderer skin = GetComponentInChildren<SkinnedMeshRenderer>();
            charactorModelTrs = skin.transform.parent;
            Debug.Log($"{gameObject}\ncharactorModelTrs = {charactorModelTrs}");
        }
        else if (_renderType == ObjectRenderType.Mesh) 
        {
            MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
            charactorModelTrs = mesh.transform.parent;
            Debug.Log($"{gameObject}\ncharactorModelTrs = {charactorModelTrs}");
        }

    }
    public Transform FindTargetBody() 
    {
        return charactorModelTrs;
    }

    
    //protected void FindMeshBodyObject()
    //{
    //    MeshRenderer skin = GetComponentInChildren<MeshRenderer>();
    //    charactorModelTrs = skin.transform.parent;
    //    Debug.Log($"{gameObject}\ncharactorModelTrs = {charactorModelTrs}");
    //}
}
