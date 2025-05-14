using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public partial class State : State_Base
{
    Actor Actor;
    //player
    //gun
    //moneter

    ObjectType objType;
    MonsterType monster = MonsterType.Defolt;
    GunType gun = global::GunType.None;
    WeaponEnum WeaponType = WeaponEnum.None;

    public void init(Actor _actor, int _stateId) 
    {
        Actor = _actor;
        var info = Shared.TableManager.State.Get(_stateId);
        Debug.Log($"{_actor.name},{this}={info}");
        StateUpdate(info);
    }

    private void StateUpdate(Table_State.Info info)
    {
        id = info.Id;
        maxHP = info.MaxHP;
        power = info.Power;
        defense = info.Defense;
        speed = info.Speed;
        critRate = info.CritRate;
        critDamage = info.CritDamage;
    }

    public float StateValueLoad(StatusType _status) 
    {
        switch (_status)
        {
            case StatusType.HP:
                return maxHP;

            case StatusType.Power:
                return power;

            case StatusType.Defens:
                return defense;

            case StatusType.Speed:
                return speed;

            case StatusType.CritRate:
                return critRate;

            case StatusType.CritDamage:
                return critDamage;

            default:
                return 0.0f;
        }
    }

    public void StatusInit(int value)
    {
        //ViewHp = value;
    }


    [Header("Weapon")]
    public int WeaponAttack;//°ø°Ý·Â
    //[Header("Monster")]
    public void InitStateType(ObjectType _eNum)
    {
        objType = _eNum;
    }
    public void monsterType(MonsterType _eNum)
    {
        monster = _eNum;
    }
    public void gunType(GunType _eNum)
    {
        gun = _eNum;
    }
    public void WeaponState(WeaponEnum _type)
    {
        switch (_type)
        {
            case WeaponEnum.Gun:
                WeaponAttack = 10 ;
                break;
            case WeaponEnum.Sword:
                WeaponAttack = 20;
                break;
        }
    }



    private void playerState() 
    {
        maxHP = 100;
        power = 30;
        defense = 30;
        speed = 5;
    }
    public void MonsterState(GunType _eNum)
    {
        switch (_eNum)
        {
            case GunType.AR:
                power = 30;
                defense = 30;
                break;
            case GunType.SG:
                power = 30;
                defense = 30;
                break;
            case GunType.SR:
                power = 30;
                speed = 10;
                break;
        }
        Debug.Log($"\n" +
            $",monster={_eNum}\n" +
            $",Attack={power}\n" +
            $",Movespeed{speed}");
    }
    public void MonsterState(MonsterType _monster)
    {
        switch (_monster)
        {
            case MonsterType.Spider:
                maxHP = 200;
                power = 30;
                defense = 30;
                speed = 3;
                break;
            case MonsterType.Dron:
                maxHP = 100;
                power = 30;
                defense = 30;
                speed = 5;
                break;
            case MonsterType.Sphere:
                maxHP = 500;
                power = 30;
                defense = 30;
                speed = 5;
                break;
        }
        Debug.Log($"\n" +
            $",monster={_monster}\n" +
            $",MaxHP={maxHP}\n" +
            $",Attack={power}\n" +
            $",Defense={defense}\n" +
            $",Movespeed{speed}");
    }
    public void Hit()
    {

    }

}
