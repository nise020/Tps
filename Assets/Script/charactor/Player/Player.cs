using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Player : Charactor
{
    protected MoveCamera viewcam;
    Vector3 Vector = new Vector3(0, 1.5f, 0);
    public Vector3 movePos = Vector3.zero;
    protected Rigidbody rigid;
    protected Animator playerAnim;
    protected Gun gun;
    bool runstate = false;

    [SerializeField] GameObject HandObj;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject scabbard;
    protected PlayerEnum playerType;
    //스토레이지

    SkillRunning skillCheck = SkillRunning.SkillOff;


    public void playerTypInite(PlayerEnum _type) 
    {
        _type = playerType;
    }
    PlayerControll playerControll = PlayerControll.Off;
    public void playerOnOff(PlayerControll _type)
    {
        playerControll = _type;
    }
    protected bool mouseClick => Input.GetMouseButton(0);
    protected bool mouseClickUp => Input.GetMouseButtonUp(0);
    protected bool mouseClickDown => Input.GetMouseButtonDown(0);
    protected bool reloadOn => Input.GetKeyDown(KeyCode.R);
    protected bool Skill1 => Input.GetKeyDown(KeyCode.Q);
    protected bool Skill2 => Input.GetKeyDown(KeyCode.E);

    [Header("무기")]
    [SerializeField] GameObject WeaponPrefab;
    [SerializeField] GameObject shortSword;

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

    protected virtual void Start()
    {
        //playerType = PlayerType.Gunner;
        viewcam = Shared.BattelManager.MOVECAM;
        STATE.init(charactor);
        stateInIt();
        rigid = GetComponent<Rigidbody>();
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
    // Update is called once per frame
    //private void Update()
    //{
    //    runcheck();
    //    if ((mouseClick))
    //    {
    //        attack();
    //    }
    //    else if (playerType == PlayerType.Gunner)//떼면 자동으로
    //    {
    //        if (mouseClickUp || gun.nowbullet <= 0)
    //        {
    //            viewcam.cameraShakeAnim(false);
    //            playerAnim.SetInteger("Attack", 0);
    //        }
    //    }
    //    reloding(playerType);//리로드
    //    shitdownCheak();//앉기
    //    ////Time.timeScale = 0;//Faraim Speed up,Down
    //}
    private void FixedUpdate()
    {
        move(playerControll);
    }
    protected override void playerSkillAttack(PlayerEnum _type) 
    {
        if (Skill1)
        {
            if (skillCheck == SkillRunning.SkillOff)
            {
                skillStrategy.Skill(playerType, 1, attackValue);
                skillCheck = SkillRunning.SkillOn;
                //playerAnim.SetInteger("Skill1", 1);
                playerAnim.SetInteger("", 1);
                Invoke("SkillValueReset", 3);//clear
            }
            else 
            {
                return;
            }
        }
        else if (Skill2)
        {
            if (skillCheck == SkillRunning.SkillOff)
            {
                skillStrategy.Skill(playerType, 2, attackValue);
                skillCheck = SkillRunning.SkillOn;
                //playerAnim.SetInteger("Skill1", 1);
                playerAnim.SetInteger("", 1);
                Invoke("SkillValueReset", 3);//clear
            }
            else
            {
                return;
            }
        }
        else { return; }
        
    }
    protected void SkillValueReset() 
    {
        attackValue = attackReset;
        skillCheck = SkillRunning.SkillOff;
    }
    protected override void nomalAttack()
    {
        //base.nomalAttack();
        if (playerType == PlayerEnum.Gunner)
        {
            gun = GetComponentInChildren<Gun>();
            if (gun.reLoed == false && gun.nowbullet >= 0)
            {
                Vector3 AimDirection = gun.gameObject.transform.forward;
                playerAnim.SetLayerWeight(attackLayerIndex, 1.0f);
                AttackAnim(1);
                gun.Attack(AimDirection);
            }
        }
        else if (playerType == PlayerEnum.Warrior) 
        {
            closeAttackCheack();
        }
       
    }
    public void reloding(PlayerEnum _type) 
    {
        if (_type != PlayerEnum.Gunner) { return; }

        if (reloadOn || gun.nowbullet <= 0)
        {
            playerAnim.SetLayerWeight(attackLayerIndex, 1.0f);
            playerAnim.SetInteger("Reload", 1);
        }

        //if (gun.reLoed)
        //{
        //    animCheck("Reload", "reloading");
        //}

    }
 
    IEnumerator reLoadout(int _index) 
    {
        gun.nowbullet = gun.bullet;
        gun.bulletcount = 0;
        playerAnim.SetLayerWeight(_index, 0.0f);
        gun.reLoed = false;
        yield return null;
    }
    //protected override void checkHp() 
    //{
    //    base.checkHp();
    //}
    protected override void dead() 
    {
        Shared.BattelManager.PlayerAlive = false;
        gameObject.SetActive(false);
    }


    public void attackRot() 
    {
        Vector3 pos = Shared.BattelManager.CamAim.transform.forward;
        transform.rotation = Quaternion.Euler(pos); 
    }
}
