using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract partial class Character : Actor
{
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
        condition = _condition;
    }
    public virtual int StatusTypeLoad(StatusType _type) 
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
                Invoke("death", 0.3f);
                return;
            }
            //invincibleState();
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
        if (!HPBAR.gameObject.activeSelf)
        {
            HPBAR.gameObject.SetActive(true);
        }
        else
        {
            return;
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
