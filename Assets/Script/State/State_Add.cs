using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class State : MonoBehaviour
{
    public void PluseAtk(int _value) 
    {
        Attack = Attack + _value;
    }
    public void PluseDef(int _value)
    {
        Defense = Defense + _value;
    }
    public void PluseSpe(int _value)
    {
        Speed = Speed + _value;
    }
    public void PluseMaxHp(int _value)
    {
        MaxHP = MaxHP + _value;
    }

}
