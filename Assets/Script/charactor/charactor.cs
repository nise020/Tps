using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Charactor : Actor
{
    //ĳ����
    //���� ���
    //clone ������Ʈ ���� ��� 
    //protected int ID;//�ڽ��� ID
    protected float hP;//�������� ü��
    protected float cheHP;//üũ�� ü��
    protected float maxHP;//�ִ�ü��
    [SerializeField] protected float CharactorId = 0;//�ִ�ü��
    [SerializeField] GameObject hpBar;//uiHp
    State state = new State();

    protected float skillCool_1;//1�� ��ų��Ÿ��
    protected float skillCool_2;//2�� ��ų��Ÿ��
    protected float buff;//����
    protected float burstCool;//����Ʈ ��Ÿ��

    protected void inIt() 
    {
        hP = state.MaxHP;
        cheHP = hP;
        maxHP = hP;
    }

    protected override void OnTriggerEnter(Collider other) 
    {
        //base.OnTriggerEnter(other);

    }

    public void Hit() 
    {

    }

    protected virtual void dead() //��� ����
    {

    }
    protected virtual void move() 
    {

    }
    protected virtual void attack() 
    {

    }
}
