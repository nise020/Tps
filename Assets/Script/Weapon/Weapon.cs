using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Weapon : Actor
{
    protected WeaponEnum WeaponType = WeaponEnum.None;
    protected PlayerEnum PlayerType;
    State Weaponstate = new State();
    public void init(WeaponEnum _type) 
    {
        WeaponType = _type;
    }
    public virtual void Attack() 
    {

    }
    public virtual void Attack(Vector3 _pos)
    {

    }
    protected virtual void SkillAttack() 
    {

    }
    protected virtual void ControllWeapon(PlayerEnum _type) 
    {

    }   
 
}
