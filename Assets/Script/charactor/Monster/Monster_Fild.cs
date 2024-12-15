using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Monster : Charactor
{
    BattelManager BATTELMANAGER;
    [Header("공격할 물체(공통)")]
    public GameObject MobGrenade;//투척물 프리팹
    public GameObject MobBullet;//일반공격 총알 프리팹

    protected AiMonster AI = new AiMonster();
    protected MonsterSkill SKILL = new MonsterSkill();

    protected eMobType eType;//<---

    protected eAI aIState = eAI.Create;
    public List<GameObject> soljerObj;//플레이어 위치 정보
    public List<GameObject> coverObj;//엄폐물
    public Transform creatTabObj;//총알 저장탭

    [Header("몬스터의 정보")]
    public int mobKey = 0;

    [Header("공격할 물체(공통)")]
    public GameObject AttackArm;//공격의 시작점이 될 팔
    protected bool NumberOn = false;
    public int number;
    Vector3 targetpos;


    [Header("기본 타이머")]
    //ublic int Patternt = 0f;
    protected float Patterntimer = 0f;
    protected float Patternltime = 10.0f;

    [Header("몬스터의 스탯")]
    public float HP = 0.0f;
    public float MoveSpeed = 0.0f;


    [Header("다리,이동 관련(FlyingMob 제외)")]
    [SerializeField] GameObject footObj;
    [SerializeField] protected bool groundCheck = false;
    [SerializeField] protected float leagh;
    public Rigidbody mobRigid;
    Color leaghColor;
    BoxCollider boxColl;
    Collider mobColl;
}
