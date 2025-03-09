using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Player : Charactor
{
    MoveCamera viewcam;
    Vector3 Vector = new Vector3(0, 1.5f, 0);
    public Vector3 movePos = Vector3.zero;
    Rigidbody rigid;
    Animator playerAnim;
    Gun gun;
    bool runstate = false;


    [SerializeField] PlayerType playerType = PlayerType.None;

    //public GameObject playerSpine;

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
    UnityEngine.Camera Maincam;

    protected int pluse_bullet;
    protected int RelodingBullet;
    
    [Header("장전 시간,충전 시간")]
    protected float ChargeingTime;
    protected float ChargeingTimer = 0.0f;
    protected float RerodingTime = 0.0f;
    protected float RerodingTimer = 0.0f;

    Skill_Add SKILLADD = new Skill_Add();
    protected GunType GunEnumType;//다른곳에서 전달 받기
    string Name;

    //AnimatorStateInfo animStateInfo;
    ObjectType charactor = ObjectType.Player;
    protected void LoadSkill() 
    {
        switch (GunEnumType)
        {
            case GunType.AR:
                SKILLADD.UseBurstSkill(1, pluse_bullet, attackValue, burst_RunTime,burstCool);
                break;
            case GunType.MG:
                SKILLADD.UseBurstSkill(2, pluse_bullet, attackValue, burst_RunTime, burstCool);
                break;
            case GunType.SG:
                SKILLADD.UseBurstSkill(3, pluse_bullet, attackValue, burst_RunTime, burstCool);
                break;
            case GunType.SMG:
                SKILLADD.UseBurstSkill(4, pluse_bullet, attackValue, burst_RunTime, burstCool);
                break;
            case GunType.SR:
                SKILLADD.UseBurstSkill(5, pluse_bullet, attackValue, burst_RunTime, burstCool);
                break;
        }
    }

    protected override void Start()
    {
        viewcam = Shared.BattelManager.MOVECAM;
        STATE.init(charactor);
        inIt();
        rigid = GetComponent<Rigidbody>();
        gun = GetComponentInChildren<Gun>();
        playerAnim = GetComponentInChildren<Animator>();
        Maincam = UnityEngine.Camera.main;
        Shared.InutTableMgr();
        Table_Charactor.Info info = Shared.TableManager.Character.Get(1);
        Name = info.Img;
    }


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    // Update is called once per frame
    void Update()
    {
        runcheck();
        bool value1 = Input.GetMouseButton(0);
        bool value2 = Input.GetMouseButtonUp(0);
        if ((value1))
        {
            hit();
        }
        else if (value2 || gun.nowbullet <= 0)
        {
            viewcam.cameraShakeAnim(false);
            playerAnim.SetInteger("Attack", 0);
        }
        reloding();
        closeAttackCheack();
        shitdownCheak();
        ////Time.timeScale = 0;//Faraim Speed up,Down
    }
    private void FixedUpdate()
    {
        move();
    }
    protected override void hit()
    {
        if (playerType == PlayerType.Gunner) 
        {
            Vector3 AimDirection = gun.gameObject.transform.forward;
            if (gun.reLoed == false && gun.nowbullet >= 0)
            {
                playerAnim.SetLayerWeight(attackLayerIndex, 1.0f);
                playerAnim.SetInteger("Attack", 1);
                gun.GunAttack(AimDirection);
            }
            else if (gun.nowbullet <= 0)
            {
                Shared.BattelManager.MOVECAM.cameraShakeAnim(false);
                playerAnim.SetInteger("Attack", 0);
            }
        }
       
    }
    public void reloding() 
    {
        //string text = ($"{PlayerAnimParameters.Reload}");
        //string text2 = ($"{playerAnimInfoName.reloading}");
        bool reload = Input.GetKeyDown(KeyCode.R);
        if (reload || gun.nowbullet <= 0)
        {
            gun.reLoed = true;
            playerAnim.SetLayerWeight(attackLayerIndex, 1.0f);
            playerAnim.SetInteger("Reload", 1);
        }

        if (gun.reLoed)
        {
            animCheck("Reload", "reloading");
        }

    }
 
    IEnumerator reLoadout(int _index) 
    {
        gun.nowbullet = gun.bullet;
        gun.bulletcount = 0;
        playerAnim.SetLayerWeight(_index, 0.0f);
        gun.reLoed = false;
        yield return null;
    }
    protected override void checkHp() 
    {
        base.checkHp();
    }
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
