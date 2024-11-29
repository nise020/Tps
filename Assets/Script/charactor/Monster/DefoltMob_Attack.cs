using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DefoltMob : Monster
{
    public void targetSearch() 
    {

    }
    protected override void targetOn(ref int _value)
    {
        base.targetOn(ref _value);
    }
    public void attack()
    {
        base.MobAttackTimecheck();
        base.nomalAttack();
        base.GrenadeAttack();
    }
}
