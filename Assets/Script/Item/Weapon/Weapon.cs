using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Weapon : Item
{
    protected WeaponEnum WeaponType = WeaponEnum.None;
    protected CharactorJobEnum PlayerType;
    Status Weaponstate = new Status();
    //[SerializeField] GameObject SkillEffectObj1;
    //[SerializeField] GameObject SkillEffectObj2;
    public void init(WeaponEnum _type) 
    {
        WeaponType = _type;
    }
    public virtual void Attack()//sword 
    {

    }
    public virtual void Attack(PlayerCamera _camera,Vector3 _pos)//Gun
    {

    }
    protected virtual void SkillAttack() 
    {

    }
    protected virtual void ControllWeapon(CharactorJobEnum _type) 
    {

    }   
 
}
