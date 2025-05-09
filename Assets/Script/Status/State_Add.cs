using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class State : State_Base
{
    protected override void PluseAtk(int _value)
    {
        Attack = Attack + _value;
    }
    protected override void PluseDef(int _value)
    {
        Defense = Defense + _value;
    }
    protected override void PluseSpe(int _value)
    {
        Speed = Speed + _value;
    }
    protected override void PluseMaxHp(int _value)
    {
        MaxHP = MaxHP + _value;
    }
}
