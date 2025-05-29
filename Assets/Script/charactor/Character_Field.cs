using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character : Actor
{
    [Header("Table/State")]
    protected State STATE = new State();
    protected int hP;//실제 체력
    protected int cheHP;//보여지는 체력
    protected int maxHP;//최대체력

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
    protected int dec;//설명

    protected float atkValue;//공격력
    protected float defVAlue;//방어력
    protected float speedValue;//이동속도
    protected float CritRateValue;
    protected float CritDamageValue;//이동속도

    [Header("State")]
    protected CharacterStateData characterData = new CharacterStateData();
    protected ObjectRenderType RenderType = ObjectRenderType.None;
    GroundTouchState GroundTouchState = GroundTouchState.GroundNoneTouch;
    protected InvincibleState characterstate = InvincibleState.invincible_Off;

    protected HpBar HPBAR = new HpBar();
    public Action<float, float> onHpChanged;

    //protected int power;//힘
    [SerializeField] GameObject hpBar;//uiHp

    protected Transform charactorModelTrs;//Modeling
    protected Transform RootTransform;//RootModel
    [SerializeField] protected GameObject weaponHandObject;//Hand
    [SerializeField] protected GameObject weaponObj;//Weapon

    protected float rotationSpeed = 20.0f;//나중에 조정
    protected float skillCool_1;//1번 스킬쿨타임
    protected float skillCool_2;//2번 스킬쿨타임
    protected float buff;//버프
    protected float burstCool;//버스트 쿨타임
    //StatusType statusType = StatusType.None;
    protected Condition condition = Condition.health;//상태패턴

    protected Vector3 weaponOriginalPos = Vector3.zero;

    protected float gravityValue = -9.81f;
    Vector3 velocity;
    CapsuleCollider CpasuleColl;
    [SerializeField] float groundCheckLenght;
    float groundCheckRadius = 0.3f;

    [Header("Action")]
    public Action<bool> AttackEvent;

}
