using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public override WeaponType WeaponType => WeaponType.Main;

    [SerializeField] GameObject swordObj;
    public override void Attack() 
    {

    }
    private void Awake()
    {
        ItemStateData.WeaponType = WeaponclassType.Sword;

        Power = 100;
        Defense = 0;
        Speed = 0;
        CritRate = 15;
        CritDamage = 20;
    }
}
