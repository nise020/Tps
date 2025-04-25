using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.Asteroids;
using UnityEngine;

public abstract partial class Weapon : Item
{
    protected WeaponEnum WeaponType;
    protected CharactorJobEnum PlayerType;
    Status Weaponstate = new Status();
    //[SerializeField] GameObject SkillEffectObj1;
    //[SerializeField] GameObject SkillEffectObj2;

    public WeaponEnum Weapontype() 
    {
        return WeaponType;
    }
    public virtual void Attack()//sword 
    {

    }
    public virtual void Attack(Vector3 _pos)//Gun
    {

    }
    protected virtual void SkillAttack() 
    {

    }
    public virtual int ReturnTypeValue(BulletValueType _type)
    {
        return 0;
    }
    public virtual void ClearTypeValue(BulletValueType _type)
    {
    }
    protected virtual void ControllWeapon(CharactorJobEnum _type) 
    {

    }
    public virtual void ReloadClearValue()
    {

    }
}
