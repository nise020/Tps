using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Monster : Charactor
{
    BattelManager BATTELMANAGER;
    [Header("������ ��ü(����)")]

    public List<GameObject> movePosObj;


    public GameObject MobGrenade;//��ô�� ������
    public GameObject MobBullet;//�Ϲݰ��� �Ѿ� ������


    protected AiMonster AI = new AiMonster();
    protected Skill_Monster SKILL = new Skill_Monster();

    public eMobType eType;

    protected eAI aIState = eAI.Create;

    Transform creatTabObj;//�Ѿ� ������

    [Header("������ ����")]
    public int mobKey = 0;

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
    public float moveSpeed = 10.0f;//�ӽ�
    [SerializeField] GameObject footObj;
    [SerializeField] protected bool groundCheck = false;
    [SerializeField] protected float leagh;
    public Rigidbody mobRigid;
    Color leaghColor;
    BoxCollider boxColl;
    Collider mobColl;
    //public Vector3 startPos;
}
