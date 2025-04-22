using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public partial class Status : StatusBase
{

    //player
    //gun
    //moneter
    public void init(ObjectType _obj) 
    {
        playerState();
        //Debug.Log($"charactor={_obj}\n" +
        //    $",monster={monster}\n" +
        //    $",MaxHP={ViewHp}\n" +
        //    $",Attack={ViewAttack}\n" +
        //    $",Defense={ViewDefense}\n" +
        //    $",Movespeed{ViewSpeed}");
    }
    ObjectType objType;
    MonsterType monster = MonsterType.Defolt;
    GunType gun = global::GunType.None;
    WeaponEnum WeaponType = WeaponEnum.None;
    public float ViewHp => hP;
    public int ViewAttack => Attack;
    public int ViewDefense => Defense;//방어력
    public float ViewSpeed => Speed;//이동속도

    public void StatusInit(int value)
    {
        //ViewHp = value;
    }


    [Header("Weapon")]
    public int WeaponAttack;//공격력
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
        MaxHP = 100;
        Attack = 30;
        Defense = 30;
        Speed = 5;
    }
    public void MonsterState(GunType _eNum)
    {
        switch (_eNum)
        {
            case GunType.AR:
                Attack = 30;
                Defense = 30;
                break;
            case GunType.SG:
                Attack = 30;
                Defense = 30;
                break;
            case GunType.SR:
                Attack = 30;
                Speed = 10;
                break;
        }
        Debug.Log($"\n" +
            $",monster={_eNum}\n" +
            $",Attack={Attack}\n" +
            $",Movespeed{Speed}");
    }
    public void MonsterState(MonsterType _monster)
    {
        switch (_monster)
        {
            case MonsterType.Spider:
                MaxHP = 30;
                Attack = 30;
                Defense = 30;
                Speed = 3;
                break;
            case MonsterType.Dron:
                MaxHP = 20;
                Attack = 30;
                Defense = 30;
                Speed = 5;
                break;
            case MonsterType.Sphere:
                MaxHP = 50;
                Attack = 30;
                Defense = 30;
                Speed = 20;
                break;
        }
        Debug.Log($"\n" +
            $",monster={_monster}\n" +
            $",MaxHP={MaxHP}\n" +
            $",Attack={Attack}\n" +
            $",Defense={Defense}\n" +
            $",Movespeed{Speed}");
    }
    public void Hit()
    {

    }
    public void NowHp()//스타트에서 한번만 실행 
    {
        hP = MaxHP;
        cheHP = hP;
    }
    public void CheckHp()//cheHP가 우선적으로 소모
    {
        if (hP == cheHP) { return; }
        //코루틴 으로 수정 예정
        if (hP != cheHP && hP >= 0)
        {
            hP = cheHP;
        }
    }
}
