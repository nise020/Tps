using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Player : Charactor
{
    protected MoveCamera viewcam;
    protected AI_Npc PLAYERAI= new AI_Npc();
    //protected Slot SlotData= new Slot();
    protected NpcAiState aIState = NpcAiState.Search;
    protected Rigidbody rigid;
    protected Animator playerAnim;
    protected Gun GUN;
    bool runValue = false;
    protected BoxCollider cameraViewObj;
    [SerializeField] GameObject HandObj;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject scabbard;
    //스토레이지


    [Header("Weapon")]
    [SerializeField] GameObject WeaponPrefab;
    [SerializeField] GameObject shortSword;
    [Header("Key")]
    int PlayerKey;
    

    //[SerializeField] Transform WeapontransPos;//무기

    [Header("조준점")]
    [SerializeField] Transform AimtransPos;//명중 오브젝트
    [SerializeField] Button ControlBtn;

    [Header("스텟")]
    float burst_RunTime;
    int attackValue;
    int attackReset;
    //UnityEngine.Camera Maincam;

    protected int pluse_bullet;
    protected int RelodingBullet;
    
    [Header("장전 시간,충전 시간")]
    protected float ChargeingTime;
    protected float ChargeingTimer = 0.0f;
    protected float RerodingTime = 0.0f;
    protected float RerodingTimer = 0.0f;

    //Skill_Add SKILLADD = new Skill_Add();
    //protected GunType GunEnumType;//다른곳에서 전달 받기
    protected string Name;

    //AnimatorStateInfo animStateInfo;
    protected ObjectType charactor = ObjectType.Player;

    protected SkillStrategy skillStrategy = new SkillStrategy();
    AudioListener audioListener => GetComponentInChildren<AudioListener>();
    protected virtual void Start()
    {
        //playerType = PlayerType.Gunner;
        slotinit();//Nps state data
        STATUS.init(charactor);//State
        stateInIt();
        cameraViewObj = GetComponentInChildren<BoxCollider>();
        viewcam = GetComponentInChildren<MoveCamera>();
        viewcam.viewObjInit(cameraViewObj.gameObject);//viewPoint
        if (charctorState == CharctorStateEnum.Npc)
        {
            viewcam.gameObject.SetActive(false);
            //Shared.BattelUI.PlayerCameraCheck(this, charctorState);
        }
        PLAYERAI.init(this);//FSM
        rigid = GetComponent<Rigidbody>();//Kinematic Controll
        playerAnim = GetComponentInChildren<Animator>();
    }
    protected override void stateInIt() 
    {
        base.stateInIt();
        attackReset = attackValue;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    private void FixedUpdate()
    {
        PLAYERAI.State(charctorState,this, out aIState);
    }

    public void init(out MoveCamera _camera) 
    {
        _camera = viewcam; 
    }
    public void TypeInit(CharactorJobEnum _type,int _key) 
    {
        playerType = _type;
        PlayerKey = _key;
    }

}
