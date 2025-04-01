using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase 
{
    [Header("Player")]
    protected float hP;//보여지는 체력
    protected float cheHP;//체크할 체력

    protected float MaxHP;//최대체력
    protected int Attack;//공격력
    protected int Defense;//방어력
    protected float Speed;//이동속도
    protected virtual void PluseAtk(int _value)
    {
        
    }
    protected virtual void PluseDef(int _value)
    {
        
    }
    protected virtual void PluseSpe(int _value)
    {
        
    }
    protected virtual void PluseMaxHp(int _value)
    {
        
    }
}
