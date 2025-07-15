using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character : Actor
{
    [Header("Table/State")]
    protected State STATE = new State();
    protected Dictionary<StatusType, float> StatusData { get; set; } = new Dictionary<StatusType, float>();
    protected Dictionary<CharacterTabelType, int> CharacterTabelData { get; set; } = new Dictionary<CharacterTabelType, int>();
    protected Dictionary<CharacterTabelType, string> CharacterTabelTextData { get; set; } = new Dictionary<CharacterTabelType, string>();

    //[Header("Table/Character")]
    public int Id;
    //protected byte type;
    //protected int skill1;
    //protected int skill2;
    //protected int ai;
    //protected int state;
    //protected string prefabs;
    //protected string img;
    //protected new int name;
    //protected int dec;//설명

    [Header("CharacterState")]
    //protected float atkValue;//공격력
    //protected float defVAlue;//방어력
    //protected float speedValue;//이동속도
    //protected float CritRateValue;
    //protected float CritDamageValue;//이동속도
    protected float radius = 20.0f;

    [Header("State")]
    protected CharacterStateData characterData = new CharacterStateData();
    protected ObjectRenderType RenderType = ObjectRenderType.None;
    GroundTouchState GroundTouchState = GroundTouchState.GroundNoneTouch;
    //protected InvincibleState characterstate = InvincibleState.invincible_Off;

    [Header("HpBar")]
    public Action<float, float> onHpChanged;
    protected HpBar HPBAR { get; set; } = null;

    //protected int power;//힘
    [SerializeField] GameObject hpBar;//uiHp

    [Header("Model")]
    protected Transform charactorModelTrs;//Modeling
    protected Transform RootTransform;//RootModel
    [SerializeField] protected GameObject weaponHandObject;//Hand
    [SerializeField] protected GameObject MainWeaponObj;//Weapon
    [SerializeField] protected GameObject SubWeaponObj;//Sub


    [Header("Skill")]
    protected float rotationSpeed = 20.0f;
    protected float skillCool_1;
    protected float skillCool_2;
    protected float buff;
    //protected float burstCool;
    protected Condition condition = Condition.health;//상태패턴
    //StatusType statusType = StatusType.None;

    protected Vector3 weaponOriginalPos = Vector3.zero;

    protected float gravityValue = -9.81f;
    Vector3 velocity;
    //CapsuleCollider CpasuleColl;
    [SerializeField] float groundCheckLenght;
    //float groundCheckRadius = 0.3f;

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
    protected Vector3 startPosition;

    [Header("UpperAvatar")]
    [SerializeField] protected Transform UpperBody;
    [SerializeField] protected float maxPitch = 30f;      
    [SerializeField] protected float UpperrotationSpeed = 30f; 
    protected Quaternion cachedUpperBodyEulerTarget = Quaternion.identity;
    protected Quaternion cachedUpperBodyEulerCurrent = Quaternion.identity;

    //protected float aimSpeed = 0.2f;


    protected bool forceUpperBody;
    protected Quaternion cachedUpperBodyEuler;
    protected Quaternion initialUpperBodyRot;
    protected Vector3 lastTargetPosition;

    [Header("Knockback")]
    protected float knockbackDistance = 1.5f;
    protected float knockbackDuration = 0.2f;
}
