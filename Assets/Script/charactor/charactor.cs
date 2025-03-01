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
    protected float hP;//���� ü��
    protected float cheHP;//�������� ü��
    protected float maxHP;//�ִ�ü��
    [SerializeField] protected float CharactorId = 0;//�ִ�ü��
    [SerializeField] GameObject hpBar;//uiHp
    protected State STATE = new State();

    protected float skillCool_1;//1�� ��ų��Ÿ��
    protected float skillCool_2;//2�� ��ų��Ÿ��
    protected float buff;//����
    protected float burstCool;//����Ʈ ��Ÿ��
    [SerializeField] CharactorType type = CharactorType.None;


    protected void inIt() 
    {
        hP = STATE.MaxHP;
        cheHP = hP;
        maxHP = hP;
    }

    protected override void OnTriggerEnter(Collider other) 
    {
        base.OnTriggerEnter(other);
    }

    public void Hit() 
    {

    }
    protected virtual void hpCheck() 
    {
        if (cheHP == hP) return;
        if (cheHP >= hP)
        {
            cheHP = hP;
            if (hP==0) 
            {
                dead();
            }
        }
    }
    protected virtual void dead() //��� ����
    {
        if (type == CharactorType.Player) 
        {
            Shared.BattelMgr.PlayerAlive = false;
            gameObject.SetActive(false);
        }
        else 
        {
            gameObject.SetActive(false);
        }
    }
    protected virtual void move() 
    {

    }
    protected virtual void attack() 
    {

    }
}
