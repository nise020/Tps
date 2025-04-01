using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SkillStrategy : Strategy
{
    Gun GUNS;

    public override void Skill(PlayerEnum _type, int _skillNumber,int _damageValue)
    {
        switch (_type)
        {
            case PlayerEnum.Gunner:
                GunnerSkill(_skillNumber, _damageValue);
                break;
            case PlayerEnum.Warrior:
                WarriorSkill(_skillNumber, _damageValue);
                break;
            case PlayerEnum.None:
                return;

        }

    }
    public void GunnerSkill(int _skill, int _damageValue)
    {
        if (_skill == 1)//attack skill1
        {
            DamegeUp = 10;
            _damageValue = DamegeUp;
            //Gun gun = PLAYER.gameObject.GetComponentInChildren<Gun>();
            //Vector3 AimDirection = GUNS.gameObject.transform.forward;
            //GUNS.Attack(AimDirection);
        }
        else if (_skill == 2)//buff skill2
        {
            DamegeUp = 20;
            _damageValue = DamegeUp;
        }
    }
    public void WarriorSkill(int _skill, int _damageValue)
    {
        if (_skill == 1)//skill1
        {
            DamegeUp = 30;
            _damageValue = DamegeUp;
            //Gun gun = PLAYER.gameObject.GetComponentInChildren<Gun>();
            //Vector3 AimDirection = Weapon.gameObject.transform.forward;
            //Weapon.Attack(AimDirection);
        }
        else if (_skill == 2)//skill2
        {
            DamegeUp = 40;
            _damageValue = DamegeUp;
        }
    }
}
