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
    [SerializeField] protected float CharactorId = 0;//�ִ�ü��
    [SerializeField] GameObject hpBar;//uiHp
    protected State STATE = new State();

    protected float skillCool_1;//1�� ��ų��Ÿ��
    protected float skillCool_2;//2�� ��ų��Ÿ��
    protected float buff;//����
    protected float burstCool;//����Ʈ ��Ÿ��
    [SerializeField] CharactorType type = CharactorType.None;

    protected virtual void OnTriggerEnter(Collider other)//����ȭ �ʿ�
    {
        Collider myColl = gameObject.GetComponent<Collider>();
        if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Monster))//������ ���
        {
            if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Player))
            {
                attack();
            }
            else if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Bullet))//�ǰ�
            {
                hpCheck();
            }
        }
        else if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Player))//�÷��̾� �� ���
        {
            if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Monster))//�ǰ�
            {
                hpCheck();
            }
        }
    }
    protected void inIt() 
    {
        hP = STATE.MaxHP;
        cheHP = hP;
        maxHP = hP;
    }

    public void Hit() 
    {

    }
    protected virtual void hpCheck() 
    {
        //if (cheHP == hP) return;
        hP = hP - 1;//1�� �ٲ����
        Debug.Log("Hit");
        if (cheHP >= hP)
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
        if (type == CharactorType.Player) 
        {
            Shared.BattelMgr.PlayerAlive = false;
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
    protected virtual void move() 
    {

    }
    protected virtual void attack() 
    {

    }
}
