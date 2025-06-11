using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.Asteroids;
using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract partial class Weapon : Item
{
    public override ObjectType Type => ObjectType.Weapon;
    public abstract WeaponType WeaponType { get; }

    //[SerializeField] GameObject SkillEffectObj1;
    //[SerializeField] GameObject SkillEffectObj2;

    //protected WeaponEnum weaponType;
    //protected PlayerType PlayerType;

    //protected float Range;//범위
    //protected float Power;//힘
    //protected float Defense;//방어력
    //protected int RequiredLevel;//착용 조건
    //protected int WeaponType;
    //protected float Speed;//범위
    protected Character character;
    protected List<GameObject> targetToAttackList = new List<GameObject>();
    public void CharcterInit(Character _charactor)
    {
        if (character == null)
        {
            character = _charactor;

            if (_charactor is Player)
            {
                targetToAttackList = Shared.BattelManager.LoadToCharcterList(ObjectType.Monster);
            }
            else if (_charactor is Monster)
            {
                targetToAttackList = Shared.BattelManager.LoadToCharcterList(ObjectType.Player);
            }
        }
        else { return; }
    }

    protected override void WeaponItemInit(Table_Item.Info _info)
    {


    }
    public WeaponclassType Weapontype() 
    {
        return ItemStateData.WeaponType;
    }
    public virtual void WeaponReset() 
    {

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
