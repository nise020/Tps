using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    protected Transform charactorModelTrs;//Modeling
    protected float rotationSpeed = 20.0f;//���߿� ����
    protected float skillCool_1;//1�� ��ų��Ÿ��
    protected float skillCool_2;//2�� ��ų��Ÿ��
    protected float buff;//����
    protected float burstCool;//����Ʈ ��Ÿ��
    public Transform BodyObjectLoad() 
    {
        return charactorModelTrs;
    }

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
    protected HpBar HPBAR = new HpBar();
    public void HpInIt(HpBar _hpBar)
    {
        HPBAR = _hpBar;
    }
    public void StatusUpLoad(float _hp) 
    {
        hP = _hp;
        if (hP <= 0) 
        {
            dead();
        }
        cheHP = hP;
        HPBAR.SetHp(maxHP, cheHP);
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
    protected void footRayCheck() //�߷±���
    {
        //init
        //update
        //Renderring
    }
    protected void FindSkinBodyObject()
    {
        SkinnedMeshRenderer skin = GetComponentInChildren<SkinnedMeshRenderer>();
        charactorModelTrs = skin.transform.parent;
        Debug.Log($"{gameObject}\ncharactorModelTrs = {charactorModelTrs}");
    }
    protected void FindMeshBodyObject()
    {
        MeshRenderer skin = GetComponentInChildren<MeshRenderer>();
        charactorModelTrs = skin.transform.parent;
        Debug.Log($"{gameObject}\ncharactorModelTrs = {charactorModelTrs}");
    }
    protected virtual void checkHp(Collider other) //���� �ʿ�
    {
        //if (cheHP == hP) return;
        //Damage(other);
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
    protected virtual void search() {}
    protected virtual void moveAnimation(MonsterWalkState _state) {}
    protected virtual void attackAnimation(MonsterAttackState _state) {}
    protected virtual void move(CharctorStateEnum _value,Vector3 _pos) 
    {

    }
    protected virtual void move(CharctorStateEnum _value, Player _player)
    {

    }
    protected virtual void attack(CharctorStateEnum _state, CharactorJobEnum _job) 
    {

    }
    protected virtual void commonskillAttack1(CharactorJobEnum _type) 
    {

    }
    protected virtual void commonskillAttack2(CharactorJobEnum _type)
    {

    }
    public Status StateLoad()
    {
        Status state = STATUS;
        return state;
    }

}
