using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    [SerializeField] GameObject swordObj;
    public override void Attack() 
    {

    }
    private void Awake()
    {
        ItemStateData.weaponType = WeaponEnum.Sword;
    }
}
