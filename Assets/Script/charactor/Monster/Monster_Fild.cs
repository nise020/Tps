using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : Charactor
{
    BattelManager BATTELMANAGER;
    [Header("������ ��ü(����)")]

    public List<Vector3> movePos;


    public GameObject MobGrenade;//��ô�� ������
    public GameObject MobBullet;//�Ϲݰ��� �Ѿ� ������

    protected MonsterType monsterType;

    protected MonsterAiState aIState = MonsterAiState.Create;

    protected Transform creatTabObj;//�Ѿ� ������
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
    [Header("������ ��ü(����)")]
    public GameObject AttackArm;//������ �������� �� ��
    protected bool NumberOn = false;
    public int MyNumber;
    Vector3 targetpos;
    //public float attackDistanse;



    [Header("�⺻ Ÿ�̸�")]
    //ublic int Patternt = 0f;
    //protected float Patterntimer = 0f;
    //protected float Patternltime = 10.0f;


    [Header("�ٸ�,�̵� ����(FlyingMob ����)")]
    [SerializeField] GameObject footObj;
    [SerializeField] protected bool groundCheck = false;
    [SerializeField] protected float leagh;
    public Rigidbody monsterRigid;
    protected Collider monsterColl;
    //public Vector3 startPos;
}
