using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract partial class Charactor : Actor
{
    protected HpBar HPBAR = new HpBar();

    //protected int power;//힘
    [SerializeField] GameObject hpBar;//uiHp

    protected Transform charactorModelTrs;//Modeling
    protected Transform RootTransform;//RootModel
    [SerializeField] protected GameObject weaponHandObject;//Hand
    [SerializeField] protected GameObject weaponObj;//Weapon

    protected float rotationSpeed = 20.0f;//나중에 조정
    protected float skillCool_1;//1번 스킬쿨타임
    protected float skillCool_2;//2번 스킬쿨타임
    protected float buff;//버프
    protected float burstCool;//버스트 쿨타임
    //StatusType statusType = StatusType.None;
    protected Condition condition = Condition.health;//상태패턴


    public Action<float, float> onHpChanged;

    protected Vector3 weaponOriginalPos = Vector3.zero;
    protected ObjectRenderType RenderType = ObjectRenderType.None;

    protected InvincibleState characterstate = InvincibleState.invincible_Off;
    protected virtual void FindWeaponObject(LayerName _name) { }
    protected void invincibleState()//무적
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
            case StatusType.CritDamage:
                value = (int)CritRateValue;
                break;
            case StatusType.CritRate:
                value = (int)CritDamageValue;
                break;
        }
        return value;
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
    public Transform FindTargetBody()
    {
        return charactorModelTrs;
    }
    
    protected void FindBodyObject()
    {
        Transform[] skin = GetComponentsInChildren<Transform>();

        int HandLayer = LayerMask.NameToLayer(LayerName.WeaponHand.ToString());
        int BodyLayer = LayerMask.NameToLayer(LayerName.Body.ToString());

        foreach (Transform skin2 in skin)
        {
            if (skin2.gameObject.layer == HandLayer)
            {
                weaponHandObject = skin2.gameObject;
            }
            else if (skin2.gameObject.layer == BodyLayer)
            {
                charactorModelTrs = skin2.transform;
            }
        }
        
    }
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
        if (condition == Condition.Death) { return; }
        if (condition != Condition.Death)
        {
            hP = (int)_hp;
            cheHP = hP;
            hbBarCheck(true);

            //GameEvents.onHpChanged?.Invoke(this);

            onHpChanged?.Invoke(maxHP, cheHP);

            if (hP <= 0)
            {
                Debug.Log("Dead");
                Invoke("death", 1f);
                return;
            }
            invincibleState();
        }

    }

    protected void FindRenderObjectType(ObjectRenderType _renderType) 
    {
        //if (_renderType == ObjectRenderType.Skin) 
        //{
        //    SkinnedMeshRenderer[] skin = GetComponentsInChildren<SkinnedMeshRenderer>();

        //    int HandLayer = LayerMask.NameToLayer(LayerName.WeaponHand.ToString());
        //    int BodyLayer = LayerMask.NameToLayer(LayerName.Body.ToString());

        //    foreach (SkinnedMeshRenderer skin2 in skin) 
        //    {
        //        if (skin2.gameObject.layer == HandLayer) 
        //        {
        //            weaponHandObject = skin2.gameObject;
        //        }
        //        else if (skin2.gameObject.layer == BodyLayer) 
        //        {
        //            charactorModelTrs = skin2.transform;
        //        }
        //    }
        //    //charactorModelTrs = skin2.transform.parent;
        //    //Debug.Log($"{gameObject}\ncharactorModelTrs = {charactorModelTrs}");
        //}
        //else if (_renderType == ObjectRenderType.Mesh) 
        //{
        //    MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
        //    charactorModelTrs = mesh.transform.parent;
        //    Debug.Log($"{gameObject}\ncharactorModelTrs = {charactorModelTrs}");
        //}
    }
    

    
    //protected void FindMeshBodyObject()
    //{
    //    MeshRenderer skin = GetComponentInChildren<MeshRenderer>();
    //    charactorModelTrs = skin.transform.parent;
    //    Debug.Log($"{gameObject}\ncharactorModelTrs = {charactorModelTrs}");
    //}
}
