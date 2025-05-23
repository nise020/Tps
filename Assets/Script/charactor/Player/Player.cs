using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Player : Charactor
{
    protected PlayerCamera viewcam;
    protected AI_Npc PLAYERAI= new AI_Npc();
    protected Weapon WEAPON;
    protected NpcAiState aIState = NpcAiState.Search;
    protected Rigidbody rigid;
    protected Animator playerAnim;
    
    //bool runValue = false;
    //protected BoxCollider cameraViewObj;
    protected GameObject cameraViewObj;
    //[SerializeField] GameObject HandObj;
    //[SerializeField] GameObject weapon;
    //[SerializeField] GameObject scabbard;
    //���䷹����


    [Header("Weapon")]
    [SerializeField] GameObject WeaponPrefab;
    [SerializeField] GameObject shortSword;
    [Header("Key")]
    int PlayerKey;
    

    //[SerializeField] Transform WeapontransPos;//����

    [Header("������")]
    [SerializeField] Transform AimtransPos;//���� ������Ʈ
    [SerializeField] Button ControlBtn;

    [Header("����")]
    float burst_RunTime;
    protected int attackReset;
    //UnityEngine.Camera Maincam;

    protected int pluse_bullet;
    protected int RelodingBullet;
    
    [Header("���� �ð�,���� �ð�")]
    protected float ChargeingTime;
    protected float ChargeingTimer = 0.0f;
    protected float RerodingTime = 0.0f;
    protected float RerodingTimer = 0.0f;

    //Skill_Add SKILLADD = new Skill_Add();
    //protected GunType GunEnumType;//�ٸ������� ���� �ޱ�
    protected string Name;

    //AnimatorStateInfo animStateInfo;
    protected ObjectType charactor = ObjectType.Player;

    protected SkillStrategy skillStrategy = new SkillStrategy();
    //AudioListener audioListener => GetComponentInChildren<AudioListener>();

    protected virtual void Awake()
    {
        objType = ObjectType.Player;
    }
    //protected GameObject WeaponObj;
    protected override void Start()
    {
        
        base.Start();
        //STATE.init(charactor);//State
        //stateInIt();
        //FindSkinBodyObject();
        viewcam = GetComponentInChildren<PlayerCamera>();
        if (charctorState == CharctorStateEnum.Npc)
        {
            viewcam.gameObject.SetActive(false);
        }
        PLAYERAI.init(this);//FSM
        slotinit();

        rigid = GetComponent<Rigidbody>();//Kinematic Controll
        playerAnim = GetComponent<Animator>();

        skillStrategy.PlayerInit(this);
        SkillEffectSystem1 = CreatSkill(SkillEffectObj1, SkillParentObj1);
        SkillEffectSystem2 = CreatSkill(SkillEffectObj2, SkillParentObj2);

    }

    //protected override void stateInIt() 
    //{
    //    //base.stateInIt();
    //    attackReset = (int)atkValue;
    //}

    //protected override void OnTriggerEnter(Collider other)
    //{
    //    base.OnTriggerEnter(other);
    //}

    private void FixedUpdate()
    {
        if (charctorState != CharctorStateEnum.Player)
        {
            PLAYERAI.State(charctorState, this, out aIState);
        }
    }

    public void init(out PlayerCamera _camera) 
    {
        _camera = viewcam; 
    }
    public void TypeInit(CharactorJobEnum _type,int _key) 
    {
        playerType = _type;
        PlayerKey = _key;
    }
    public int keyLode() 
    {
        return PlayerKey;
    }
    public void GetItem(Item _item) 
    {
        Shared.UiManager.UI_INVENTORY.itemLists.Add(_item);//�̰� ������ �����
    }
}
