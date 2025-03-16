using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SkillStrategy : Strategy
{
    Player PLAYER;
    Gun weapons;
    PlayerType playerType = PlayerType.None;
    public void PlayerInit(Player player) 
    {
        PLAYER = player;
    }
    public void WeaponInit(Gun weapon)
    {
        weapons = weapon;
    }
    public void type(PlayerType _type) 
    {
        playerType = _type;
    }
    public override void Skill(int _number) 
    {
        if (_number == 1)
        {
            //Gun gun = PLAYER.gameObject.GetComponentInChildren<Gun>();
            Vector3 AimDirection = weapons.gameObject.transform.forward;
            weapons.GunAttack(AimDirection);
        }
        else if (_number == 2) 
        {

        }
    }
}
