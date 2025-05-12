using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class State : State_Base
{
    protected override void PluseAtk(int _value)
    {
        power = power + _value;
    }
    protected override void PluseDef(int _value)
    {
        defense = defense + _value;
    }
    protected override void PluseSpe(int _value)
    {
        speed = speed + _value;
    }
    protected override void PluseMaxHp(int _value)
    {
        maxHP = maxHP + _value;
    }
}
