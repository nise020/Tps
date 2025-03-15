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

    [SerializeField] GameObject HandObj;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject scabbard;
    [SerializeField] PlayerType playerType = PlayerType.None;
    public void playerTypInite(PlayerType _type) 
    {
        _type = playerType;
    }
    PlayerControll playerControll = PlayerControll.Off;
    public void playerOnOff(PlayerControll _type)
    {
        playerControll = _type;
    }
    bool mouseClick => Input.GetMouseButton(0);
    bool mouseClickUp => Input.GetMouseButtonUp(0);
    bool mouseClickDown => Input.GetMouseButtonDown(0);
    bool reloadOn => Input.GetKeyDown(KeyCode.R);

    [Header("����")]
    [SerializeField] GameObject WeaponPrefab;
    [SerializeField] GameObject shortSword;

    //[SerializeField] Transform WeapontransPos;//����

    [Header("������")]
    [SerializeField] Transform AimtransPos;//���� ������Ʈ
    [SerializeField] Button ControlBtn;

    [Header("����")]
    float burst_RunTime;
    int attackValue;
    //UnityEngine.Camera Maincam;

    protected int pluse_bullet;
    protected int RelodingBullet;
    
    [Header("���� �ð�,���� �ð�")]
    protected float ChargeingTime;
    protected float ChargeingTimer = 0.0f;
    protected float RerodingTime = 0.0f;
    protected float RerodingTimer = 0.0f;

    Skill_Add SKILLADD = new Skill_Add();
    protected GunType GunEnumType;//�ٸ������� ���� �ޱ�
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

    private void Start()
    {
        viewcam = Shared.BattelManager.MOVECAM;
        STATE.init(charactor);
        stateInIt();
        rigid = GetComponent<Rigidbody>();
        playerAnim = GetComponentInChildren<Animator>();
        Shared.InutTableMgr();
        Table_Charactor.Info info = Shared.TableManager.Character.Get(1);
        Name = info.Img;
        if (playerType == PlayerType.Gunner)
        {
            gun = GetComponentInChildren<Gun>();
        }
    }


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    // Update is called once per frame
    void Update()
    {
        runcheck();
        if ((mouseClick))
        {
            attack();
        }
        else if (playerType == PlayerType.Gunner)//���� �ڵ�����
        {
            if (mouseClickUp || gun.nowbullet <= 0)
            {
                viewcam.cameraShakeAnim(false);
                playerAnim.SetInteger("Attack", 0);
            }
        }
        reloding(playerType);//���ε�
        shitdownCheak();//�ɱ�
        ////Time.timeScale = 0;//Faraim Speed up,Down
    }
    private void FixedUpdate()
    {
        move(playerControll);
    }
    protected override void attack()
    {
        if (playerType == PlayerType.Gunner)
        {
            Vector3 AimDirection = gun.gameObject.transform.forward;
            if (gun.reLoed == false && gun.nowbullet >= 0)
            {
                playerAnim.SetLayerWeight(attackLayerIndex, 1.0f);
                AttackAnim(1);
                gun.GunAttack(AimDirection);
            }
        }
        else if (playerType == PlayerType.Warrior) 
        {
            closeAttackCheack();
        }
       
    }
    public void reloding(PlayerType _type) 
    {
        if (_type != PlayerType.Gunner) { return; }

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
