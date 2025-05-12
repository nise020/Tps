using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Base 
{
    [Header("Player")]
    //protected float hP;//보여지는 체력
    //protected float cheHP;//체크할 체력

    public int id;// => Attack;
    protected float maxHP;//최대체력
    protected int power;//공격력
    protected int defense;//방어력
    protected float speed;//이동속도
    protected float critRate; //=> Speed;//이동속도
    protected float critDamage; //=> Speed;//이동속도


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
