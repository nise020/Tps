using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : Character
{
    BattelManager BATTELMANAGER;
    [Header("공격할 물체(공통)")]

    public List<Vector3> movePos;


    public GameObject MobGrenade;//투척물 프리팹
    public GameObject MobBullet;//일반공격 총알 프리팹

    //protected MonsterType monsterType;

    //protected AiState aIState = AiState.Create;

    protected Transform creatTabObj;//총알 저장탭
    public void creatTab(Transform _tab) 
    {
        creatTabObj = _tab;
    }

    
    //Dictionary<int, HpBar> hpBarData = new Dictionary<int, HpBar>();

    //protected GameObject deadEffect;
    //public void BomEffect(GameObject _effect) 
    //{
    //    //deadEffect = _effect;  
    //    deadEffect = Instantiate(_effect, charactorModelTrs.position, Quaternion.identity,charactorModelTrs);  
    //}
    [Header("공격할 물체(공통)")]
    public GameObject AttackArm;//공격의 시작점이 될 팔
    protected bool NumberOn = false;
    public int MyNumber;
    Vector3 targetpos;
    //public float attackDistanse;



    [Header("기본 타이머")]
    //ublic int Patternt = 0f;
    //protected float Patterntimer = 0f;
    //protected float Patternltime = 10.0f;


    [Header("다리,이동 관련(FlyingMob 제외)")]
    [SerializeField] GameObject footObj;
    [SerializeField] protected bool groundCheck = false;
    [SerializeField] protected float leagh;
    public Rigidbody monsterRigid;
    protected Collider monsterColl;
    //public Vector3 startPos;

    [Header("Ai")]
    Player HItPalyer;
    List<Slot> slots = new List<Slot>();
    int slotCount = 0;
    //protected MonsterWalkState walkState = MonsterWalkState.Walk_Off;
    //protected MonsterAttackState attackState = MonsterAttackState.Attack_Off;
    public float SphereRadius = 1.0f; // 구 반지름
    protected float stopDistanseValue = 0.2f;
    protected float searchRange = 20.0f;
    protected float attackRange = 5.0f;
    //Vector3 targetPos;
    Vector3 movePosition = Vector3.zero;
    bool stopDilay = false;

    protected MonsterStateData monsterStateData = new MonsterStateData();
}
