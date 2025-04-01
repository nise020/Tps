using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase 
{
    [Header("Player")]
    protected float hP;//�������� ü��
    protected float cheHP;//üũ�� ü��

    protected float MaxHP;//�ִ�ü��
    protected int Attack;//���ݷ�
    protected int Defense;//����
    protected float Speed;//�̵��ӵ�
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
