using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Player : Charactor
{
    Vector3 Vector = new Vector3(0, 1.5f, 0);
    Vector3 movePos = Vector3.zero;
    float moveSpeed = 3.0f;
    Rigidbody rigid;
    Animation playerAnim;

    [Header("����")]
    [SerializeField] GameObject WeaponPrefab;
    [SerializeField] GameObject[] MobPrefab;//����
    //[SerializeField] GameObject GunHoleObj;//�ѱ�
    //[SerializeField] Transform WeapontransPos;//����

    [Header("������")]
    [SerializeField] Transform AimtransPos;//���� ������Ʈ
    [SerializeField] Button ControlBtn;

    [Header("����")]
    float burst_RunTime;
    int attack;
    UnityEngine.Camera Maincam;

    protected int pluse_bullet;
    protected int RelodingBullet;
    
    [Header("���� �ð�,���� �ð�")]
    protected float ChargeingTime;
    protected float ChargeingTimer = 0.0f;
    protected float RerodingTime = 3.0f;
    protected float RerodingTimer = 0.0f;

    Skill_Add SKILLADD = new Skill_Add();
    protected GunTags GunEnumType;//�ٸ������� ���� �ޱ�
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
        Shared.InutTableMgr();
        Table_Charactor.Info info = Shared.TableMgr.Character.Get(1);
        playerAnim = GetComponent<Animation>();
        Maincam = UnityEngine.Camera.main;
    }


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    // Update is called once per frame
    void Update()
    {
        move();
        //moveRot();
    }

    private void move()
    {
        movePos.x = Input.GetAxisRaw("Horizontal");
        movePos.z = Input.GetAxisRaw("Vertical");

        transform.position += movePos * moveSpeed * Time.deltaTime;
    }
    

}
