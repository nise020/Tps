using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract partial class Charactor : Actor
{
    protected HpBar HPBAR = new HpBar();
    protected float hP;//���� ü��
    protected float cheHP;//�������� ü��
    protected float maxHP;//�ִ�ü��
    //protected int power;//��
    [SerializeField] protected float CharactorId = 0;
    [SerializeField] GameObject hpBar;//uiHp

    protected Transform charactorModelTrs;//Modeling
    protected float rotationSpeed = 20.0f;//���߿� ����
    protected float skillCool_1;//1�� ��ų��Ÿ��
    protected float skillCool_2;//2�� ��ų��Ÿ��
    protected float buff;//����
    protected float burstCool;//����Ʈ ��Ÿ��
    StatusType statusType = StatusType.None;
    public void HpInIt(HpBar _hpBar)
    {
        HPBAR = _hpBar;
    }
    public void StatusUpLoad(float _hp)
    {
        hP = _hp;
        cheHP = hP;

        HPBAR.SetHp(maxHP, cheHP);

        if (hP <= 0)
        {
            Debug.Log("Dead");
            Invoke("dead", 1f);
        }
    }
    public int StatusTypeLoad(StatusType _type) 
    {
        int value = 0;
        switch (_type)
        {
            case StatusType.HP:
                value = (int)hP;
                break;
            case StatusType.Power:
                
                value = (int)atkValue;
                break;
            case StatusType.Speed:
                value = (int)speedValue;
                
                break;
            case StatusType.Defens:
                value = (int)defVAlue;
                
                break;
        }
        return 0;
    }
    protected virtual void stateInIt()
    {
        hP = STATUS.ViewHp;
        cheHP = hP;
        maxHP = hP;
        speedValue = STATUS.ViewSpeed;
        atkValue = STATUS.ViewAttack;
        defVAlue = STATUS.ViewDefense;
    }
    protected virtual void checkHp(Collider other) //���� �ʿ�
    {
        Debug.Log("Hit");
        if (cheHP >= hP && hP >= 0)
        {
            cheHP = hP;
            if (hP == 0)
            {
                Invoke("dead", 1f);
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
        }
        else//+Item,Effect
        {
            hP = maxHP;
            cheHP = maxHP;

            Shared.EffectManager.Play(EffectType.BoomEffect, charactorModelTrs.position);
        }
    }
    
}
