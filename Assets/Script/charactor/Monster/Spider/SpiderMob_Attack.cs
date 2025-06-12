using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SpiderMob : Monster
{
    protected override void AttackAnimation(Weapon _weapon, GameObject _weaponObj)
    {
        _weapon.gameObject.SetActive(true);
        _weapon.WeaponReset();

        granaidAttack(weaponHandObject.transform.position, targetTrs.position, _weaponObj);
    }
}
