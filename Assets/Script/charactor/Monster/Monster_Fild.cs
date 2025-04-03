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




    protected MonsterType monsterType;

    protected MonsterAiState aIState = MonsterAiState.Create;

    protected Transform creatTabObj;//�Ѿ� ������
    public void creatTab(Transform _tab) 
    {
        creatTabObj = _tab;
    }

    [Header("������ ����")]
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
    public Rigidbody monsterRigid;
    protected Collider monsterColl;
    //public Vector3 startPos;
}
