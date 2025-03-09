using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Monster : Charactor
{
    BattelManager BATTELMANAGER;
    [Header("������ ��ü(����)")]

    public List<Vector3> movePos;


    public GameObject MobGrenade;//��ô�� ������
    public GameObject MobBullet;//�Ϲݰ��� �Ѿ� ������


    protected AiMonster AI = new AiMonster();
    protected Skill_Monster SKILL = new Skill_Monster();
    //GameObject hpBar;




    public MonsterType eType;

    protected AiState aIState = global::AiState.Create;

    Transform creatTabObj;//�Ѿ� ������

    [Header("������ ����")]
    Dictionary<int, HpBar> hpBarData = new Dictionary<int, HpBar>();

    public int mobKey = 0;
    public GameObject deadEffect;
    //public void keyAdd(Dictionary<int, HpBar> _data,int _value)
    //{
    //    _data.Add(_value, new HpBar());
    //}
    //public void hpAdd() 
    //{
    //    hpBarData.Clear();
    //}
    [Header("������ ��ü(����)")]
    public GameObject AttackArm;//������ �������� �� ��
    protected bool NumberOn = false;
    public int number;
    Vector3 targetpos;
    public GameObject eyeObj;
    public float attackDistanse;



    [Header("�⺻ Ÿ�̸�")]
    //ublic int Patternt = 0f;
    protected float Patterntimer = 0f;
    protected float Patternltime = 10.0f;


    [Header("�ٸ�,�̵� ����(FlyingMob ����)")]
    [SerializeField] GameObject footObj;
    [SerializeField] protected bool groundCheck = false;
    [SerializeField] protected float leagh;
    public Rigidbody mobRigid;
    Color leaghColor;
    BoxCollider boxColl;
    Collider mobColl;
    //public Vector3 startPos;
}
