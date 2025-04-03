using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Monster : Charactor
{
    BattelManager BATTELMANAGER;
    [Header("공격할 물체(공통)")]

    public List<Vector3> movePos;


    public GameObject MobGrenade;//투척물 프리팹
    public GameObject MobBullet;//일반공격 총알 프리팹


    protected AiMonster AI = new AiMonster();
    protected Skill_Monster SKILL = new Skill_Monster();
    //GameObject hpBar;




    protected MonsterType monsterType;

    protected MonsterAiState aIState = MonsterAiState.Create;

    protected Transform creatTabObj;//총알 저장탭
    public void creatTab(Transform _tab) 
    {
        creatTabObj = _tab;
    }

    [Header("몬스터의 정보")]
    Dictionary<int, HpBar> hpBarData = new Dictionary<int, HpBar>();

    public int mobKey = 0;
    public void mobIndex(int _key) 
    {
        mobKey = _key;
    } 
    protected GameObject deadEffect;
    public void BomEffect(GameObject _effect) 
    {
        deadEffect = _effect;  
    }
    [Header("공격할 물체(공통)")]
    public GameObject AttackArm;//공격의 시작점이 될 팔
    protected bool NumberOn = false;
    public int number;
    Vector3 targetpos;
    public GameObject eyeObj;
    public float attackDistanse;



    [Header("기본 타이머")]
    //ublic int Patternt = 0f;
    protected float Patterntimer = 0f;
    protected float Patternltime = 10.0f;


    [Header("다리,이동 관련(FlyingMob 제외)")]
    [SerializeField] GameObject footObj;
    [SerializeField] protected bool groundCheck = false;
    [SerializeField] protected float leagh;
    public Rigidbody monsterRigid;
    protected Collider monsterColl;
    //public Vector3 startPos;
}
