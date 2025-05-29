using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character : Actor
{
    [Header("Table/State")]
    protected State STATE = new State();
    protected int hP;//���� ü��
    protected int cheHP;//�������� ü��
    protected int maxHP;//�ִ�ü��

    [Header("Table/Character")]
    protected int id;
    protected byte type;
    protected int skill1;
    protected int skill2;
    protected int ai;
    protected int state;
    protected string prefabs;
    protected string img;
    protected new int name;
    protected int dec;//����

    protected float atkValue;//���ݷ�
    protected float defVAlue;//����
    protected float speedValue;//�̵��ӵ�
    protected float CritRateValue;
    protected float CritDamageValue;//�̵��ӵ�

    [Header("State")]
    protected CharacterStateData characterData = new CharacterStateData();
    protected ObjectRenderType RenderType = ObjectRenderType.None;
    GroundTouchState GroundTouchState = GroundTouchState.GroundNoneTouch;
    protected InvincibleState characterstate = InvincibleState.invincible_Off;

    protected HpBar HPBAR = new HpBar();
    public Action<float, float> onHpChanged;

    //protected int power;//��
    [SerializeField] GameObject hpBar;//uiHp

    protected Transform charactorModelTrs;//Modeling
    protected Transform RootTransform;//RootModel
    [SerializeField] protected GameObject weaponHandObject;//Hand
    [SerializeField] protected GameObject weaponObj;//Weapon

    protected float rotationSpeed = 20.0f;//���߿� ����
    protected float skillCool_1;//1�� ��ų��Ÿ��
    protected float skillCool_2;//2�� ��ų��Ÿ��
    protected float buff;//����
    protected float burstCool;//����Ʈ ��Ÿ��
    //StatusType statusType = StatusType.None;
    protected Condition condition = Condition.health;//��������

    protected Vector3 weaponOriginalPos = Vector3.zero;

    protected float gravityValue = -9.81f;
    Vector3 velocity;
    CapsuleCollider CpasuleColl;
    [SerializeField] float groundCheckLenght;
    float groundCheckRadius = 0.3f;

    [Header("Action")]
    public Action<bool> AttackEvent;

}
