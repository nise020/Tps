using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.Asteroids;
using UnityEngine;

public partial class Weapon : Item
{
    //[SerializeField] GameObject SkillEffectObj1;
    //[SerializeField] GameObject SkillEffectObj2;

    //protected WeaponEnum weaponType;
    //protected PlayerType PlayerType;

    //protected float Range;//����
    //protected float Power;//��
    //protected float Defense;//����
    //protected int RequiredLevel;//���� ����
    //protected int WeaponType;
    //protected float Speed;//����
    protected override void WeaponItemInit(Table_Item.Info _info)
    {


    }
    public WeaponEnum Weapontype() 
    {
        return ItemStateData.WeaponType;
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
    protected virtual void ControllWeapon(PlayerType _type) 
    {

    }
    public virtual void ReloadClearValue()
    {

    }

}
