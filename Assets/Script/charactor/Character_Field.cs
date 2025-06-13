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

    [Header("CharacterState")]
    protected float atkValue;//공격력
    protected float defVAlue;//방어력
    protected float speedValue;//이동속도
    protected float CritRateValue;
    protected float CritDamageValue;//이동속도
    protected float radius = 20.0f;

    [Header("State")]
    protected CharacterStateData characterData = new CharacterStateData();
    protected ObjectRenderType RenderType = ObjectRenderType.None;
    GroundTouchState GroundTouchState = GroundTouchState.GroundNoneTouch;
    protected InvincibleState characterstate = InvincibleState.invincible_Off;

    [Header("HpBar")]
    protected HpBar HPBAR = new HpBar();
    public Action<float, float> onHpChanged;

    //protected int power;//힘
    [SerializeField] GameObject hpBar;//uiHp

    [Header("Model")]
    protected Transform charactorModelTrs;//Modeling
    protected Transform RootTransform;//RootModel
    [SerializeField] protected GameObject weaponHandObject;//Hand
    [SerializeField] protected GameObject MainWeaponObj;//Weapon
    [SerializeField] protected GameObject SubWeaponObj;//Sub


    [Header("Skill")]
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
    public Action<AiState> StateEvent;

    [Header("Weapon")]
    [SerializeField] protected Weapon MAINWEAPON;
    [SerializeField] protected Weapon SUBWEAPON;

    [Header("Ai")]
    protected Character targetCharacter;
    protected Transform targetTrs;
    protected IEnumerator UpperBodyColutin;

    [Header("Gunner")]
    [SerializeField] protected Transform UpperBody;
    [SerializeField] protected float maxPitch = 30f;      // 상체 최대 회전
    [SerializeField] protected float UpperrotationSpeed = 30f; // 상체 회전 부드러움
    [SerializeField] protected float recoilAmount = 0.01f; // 에임 흔들림 강도s
    [SerializeField] protected float maxAngle = 60f; // 에임 흔들림 강도s
    protected float aimSpeed = 0.2f;

    protected bool forceUpperBody;
    protected Quaternion cachedUpperBodyEuler;
    protected Quaternion initialUpperBodyRot;
    protected Vector3 lastTargetPosition;
}
