using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Base 
{
    [Header("Player")]
    //protected float hP;//�������� ü��
    //protected float cheHP;//üũ�� ü��

    public int id;// => Attack;
    protected float maxHP;//�ִ�ü��
    protected int power;//���ݷ�
    protected int defense;//����
    protected float speed;//�̵��ӵ�
    protected float critRate; //=> Speed;//�̵��ӵ�
    protected float critDamage; //=> Speed;//�̵��ӵ�


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
