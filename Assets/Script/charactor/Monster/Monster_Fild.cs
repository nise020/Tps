using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Monster : Charactor
{
    BattelManager BATTELMANAGER;
    [Header("������ ��ü(����)")]
    public GameObject MobGrenade;//��ô�� ������
    public GameObject MobBullet;//�Ϲݰ��� �Ѿ� ������

    protected AiMonster AI = new AiMonster();
    protected MonsterSkill SKILL = new MonsterSkill();

    protected eMobType eType;//<---

    protected eAI aIState = eAI.Create;
    public List<GameObject> soljerObj;//�÷��̾� ��ġ ����
    public List<GameObject> coverObj;//����
    public Transform creatTabObj;//�Ѿ� ������

    [Header("������ ����")]
    public int mobKey = 0;

    [Header("������ ��ü(����)")]
    public GameObject AttackArm;//������ �������� �� ��
    protected bool NumberOn = false;
    public int number;
    Vector3 targetpos;


    [Header("�⺻ Ÿ�̸�")]
    //ublic int Patternt = 0f;
    protected float Patterntimer = 0f;
    protected float Patternltime = 10.0f;

    [Header("������ ����")]
    public float HP = 0.0f;
    public float MoveSpeed = 0.0f;


    [Header("�ٸ�,�̵� ����(FlyingMob ����)")]
    [SerializeField] GameObject footObj;
    [SerializeField] protected bool groundCheck = false;
    [SerializeField] protected float leagh;
    public Rigidbody mobRigid;
    Color leaghColor;
    BoxCollider boxColl;
    Collider mobColl;
}
