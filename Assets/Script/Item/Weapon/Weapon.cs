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
    public virtual void Attack()//sword 
    {

    }
    public virtual void Attack(MoveCamera _camera,Vector3 _pos)//Gun
    {

    }
    protected virtual void SkillAttack() 
    {

    }
    protected virtual void ControllWeapon(PlayerEnum _type) 
    {

    }   
 
}
