using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DefoltMob : Monster
{
    public void targetSearch() 
    {

    }
    protected override void targetOn(ref int _value, List<GameObject> _listObj)
    {
        base.targetOn(ref _value, _listObj);
    }
    public void attack()//���� �ʿ�
    {
        //base.MobAttackTimecheck();
        //base.nomalAttack();
        //base.GrenadeAttack();
    }
}
