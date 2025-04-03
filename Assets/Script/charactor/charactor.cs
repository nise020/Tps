using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Charactor : Actor
{
    //��������Ʈ
    //������ �ɸ��� �κп� ���

    //Action: ��ġ����
    //Funtion
    //Task: ADK,����ȭ,��ũ(async)
    //Const


    //�븮��
    //system.Func<object,bool> UpteAction = null;
    //UpteAction = �Լ�����
    //system.Action<bool> FinishAction = null;
    //FinishAction = �Լ�����
    //delegaet void CallBack();
    //CallBack callback = null;
    //callback = �Լ�����
    //async -> await // �񵿱�
    //���� ȣ��� ����� �Խ� ����



    //ĳ����
    //���� ���
    //clone ������Ʈ ���� ��� 
    //protected int ID;//�ڽ��� ID
    protected float hP;//���� ü��
    protected float cheHP;//�������� ü��
    protected float maxHP;//�ִ�ü��
    [SerializeField] protected float CharactorId = 0;
    [SerializeField] GameObject hpBar;//uiHp


    protected float skillCool_1;//1�� ��ų��Ÿ��
    protected float skillCool_2;//2�� ��ų��Ÿ��
    protected float buff;//����
    protected float burstCool;//����Ʈ ��Ÿ��

    protected virtual void OnTriggerEnter(Collider other)//����ȭ �ʿ�
    {
        Collider myColl = gameObject.GetComponent<Collider>();
        if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerName.Monster))//������ ���
        {
            if (other.gameObject.layer == Delivery.LayerNameEnum(LayerName.Player))
            {
                //Attack();
            }
            else if (other.gameObject.layer == Delivery.LayerNameEnum(LayerName.Bullet))//�ǰ�
            {
                checkHp(other);
            }
        }
        else if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerName.Player))//�÷��̾� �� ���
        {
            if (other.gameObject.layer == Delivery.LayerNameEnum(LayerName.Monster))//�ǰ�
            {
                checkHp(other);
            }
        }
    }
    protected virtual void stateInIt() 
    {
        hP = STATE.ViewHp;
        cheHP = hP;
        maxHP = hP;
        speedValue = STATE.ViewSpeed;
        atkValue = STATE.ViewAttack;
        defVAlue = STATE.ViewDefense;
    }
    protected void footRayCheck() //�߷±���
    {

    }

    protected virtual void checkHp(Collider other) //���� �ʿ�
    {
        //if (cheHP == hP) return;
        Damage(other);
        Debug.Log("Hit");
        if (cheHP >= hP && hP >= 0)
        {
            cheHP = hP;
            if (hP==0) 
            {
                Invoke("dead",1f);
            }
        }
    }
    protected virtual void dead() //��� ����
    {
        if (objType == ObjectType.Player) 
        {
            Shared.BattelManager.PlayerAlive = false;
            hP = maxHP;
            cheHP = maxHP;
            gameObject.SetActive(false);
        }
        else 
        {
            hP = maxHP;
            cheHP = maxHP;
            gameObject.SetActive(false);
        }
    }
    protected virtual void move(CharctorStateEnum _value) 
    {

    }
    protected virtual void attack(CharctorStateEnum _state) 
    {

    }
    protected virtual void skillAttack(PlayerjobEnum _type) 
    {

    }
    protected virtual void Damage(Collider other)
    {
        //other
        hP = hP - 1;//1�� �ٲ����
    }

}
