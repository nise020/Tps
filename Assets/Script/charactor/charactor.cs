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
    protected float HP;//�������� ü��
    protected float cheHP;//üũ�� ü��
    protected float maxHP;//�ִ�ü��



    protected float skillCool_1;//1�� ��ų��Ÿ��
    protected float skillCool_2;//2�� ��ų��Ÿ��
    protected float buff;//����
    protected float burstCool;//����Ʈ ��Ÿ��
    public void NowHp()//��ŸƮ���� �ѹ��� ���� 
    {
        HP = maxHP;
        cheHP = HP;
    }
    public void CheckHp()//cheHP�� �켱������ �Ҹ�
    {
        if (HP == cheHP) { return; }
        //�ڷ�ƾ ���� ���� ����
        if (HP != cheHP && HP >= 0) 
        {
            HP = cheHP;
        }
    }
    protected void dead() //���
    {

    }

}
