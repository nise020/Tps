using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Player : Charactor
{
    Vector3 Vector = new Vector3(0, 1.5f, 0);
    public Vector3 movePos = Vector3.zero;
    float moveSpeed = 3.0f;
    Rigidbody rigid;
    Animator playerAnim;
    Gun gun;
    bool runstate = false;

    [SerializeField] GameObject upperbody;

    [Header("무기")]
    [SerializeField] GameObject WeaponPrefab;
    [SerializeField] GameObject[] MobPrefab;//몬스터
    //[SerializeField] GameObject GunHoleObj;//총구
    //[SerializeField] Transform WeapontransPos;//무기

    [Header("조준점")]
    [SerializeField] Transform AimtransPos;//명중 오브젝트
    [SerializeField] Button ControlBtn;

    [Header("스텟")]
    float burst_RunTime;
    int attack;
    UnityEngine.Camera Maincam;

    protected int pluse_bullet;
    protected int RelodingBullet;
    
    [Header("장전 시간,충전 시간")]
    protected float ChargeingTime;
    protected float ChargeingTimer = 0.0f;
    protected float RerodingTime = 3.0f;
    protected float RerodingTimer = 0.0f;

    Skill_Add SKILLADD = new Skill_Add();
    protected GunTags GunEnumType;//다른곳에서 전달 받기
    [Header("Animator Layer")]
    int baselayerIndex = 0;
    int attacklayerIndex = 1;


    protected void LoadSkill() 
    {
        switch (GunEnumType)
        {
            case GunTags.AR:
                SKILLADD.UseBurstSkill(1, pluse_bullet, attack, burst_RunTime,burstCool);
                break;
            case GunTags.MG:
                SKILLADD.UseBurstSkill(2, pluse_bullet, attack, burst_RunTime, burstCool);
                break;
            case GunTags.SG:
                SKILLADD.UseBurstSkill(3, pluse_bullet, attack, burst_RunTime, burstCool);
                break;
            case GunTags.SMG:
                SKILLADD.UseBurstSkill(4, pluse_bullet, attack, burst_RunTime, burstCool);
                break;
            case GunTags.SR:
                SKILLADD.UseBurstSkill(5, pluse_bullet, attack, burst_RunTime, burstCool);
                break;
        }
    }

    private void Start()
    {
        gun = GetComponentInChildren<Gun>();
        playerAnim = GetComponentInChildren<Animator>();
        Maincam = UnityEngine.Camera.main;
        Shared.InutTableMgr();
        Table_Charactor.Info info = Shared.TableMgr.Character.Get(1);
    }


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    // Update is called once per frame
    void Update()
    {
        runcheck();
        move();
        reloding();
    }
    private void move()
    {
        movePos.x = Input.GetAxisRaw("Horizontal");
        movePos.z = Input.GetAxisRaw("Vertical");
        walkAnim(movePos.z);
    }
    private void walkAnim(float _move) 
    {
        if (runstate == false)//Off
        {
            playerAnim.SetInteger("Move", (int)_move);
            transform.position += movePos * moveSpeed * Time.deltaTime;
        }
        else //On
        {
            playerAnim.SetInteger("Run", (int)_move);
            transform.position += movePos * (moveSpeed * 2) * Time.deltaTime;
        }
    }
    private void runcheck() 
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) 
        {
            runstate = !runstate;
            playerAnim.SetInteger("Move", 0);
            playerAnim.SetInteger("Run", 0);
        }
    }
    public void reloding() 
    {
        if (gun.nowbullet == gun.bullet) { return; }
        if (gun.nowbullet == 0 || Input.GetKeyDown(KeyCode.Mouse1))
        {
            gun.reLoed = !gun.reLoed;
        }
        int index = attacklayerIndex;
        AnimatorStateInfo animStateInfo  = playerAnim.GetCurrentAnimatorStateInfo(index);

        float time = playerAnim.GetCurrentAnimatorStateInfo(index).normalizedTime;

        if (time <= 1.0f)
        {
            playerAnim.SetLayerWeight(index, 1.0f);
            playerAnim.Play("AttackAnimation", index);
            playerAnim.SetInteger("Reload", 1);
        }
        else if (time >= 1.0f)
        {
            playerAnim.SetLayerWeight(index, 0.0f);
            gun.reLoed = false;
            StartCoroutine(reLoadout());
            playerAnim.SetInteger("Reload", 0);
        }
    }
    IEnumerator reLoadout() 
    {
        gun.nowbullet = gun.bullet;
        yield return null;
    }
    public void attackRot() 
    {

    }
}
