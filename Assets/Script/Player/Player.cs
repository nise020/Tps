using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public partial class Player : Charactor
{
    Vector3 Vector = new Vector3(0, 1.5f, 0);
    public Vector3 movePos = Vector3.zero;
    float moveSpeed = 3.0f;
    Rigidbody rigid;
    Animator playerAnim;
    Gun gun;
    bool runstate = false;

    //public GameObject playerSpine;

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
    UnityEngine.Camera Maincam;

    protected int pluse_bullet;
    protected int RelodingBullet;
    
    [Header("���� �ð�,���� �ð�")]
    protected float ChargeingTime;
    protected float ChargeingTimer = 0.0f;
    protected float RerodingTime = 0.0f;
    protected float RerodingTimer = 0.0f;

    Skill_Add SKILLADD = new Skill_Add();
    protected GunTags GunEnumType;//�ٸ������� ���� �ޱ�
    string Name;
    
    //AnimatorStateInfo animStateInfo;

    protected void LoadSkill() 
    {
        switch (GunEnumType)
        {
            case GunTags.AR:
                SKILLADD.UseBurstSkill(1, pluse_bullet, attackValue, burst_RunTime,burstCool);
                break;
            case GunTags.MG:
                SKILLADD.UseBurstSkill(2, pluse_bullet, attackValue, burst_RunTime, burstCool);
                break;
            case GunTags.SG:
                SKILLADD.UseBurstSkill(3, pluse_bullet, attackValue, burst_RunTime, burstCool);
                break;
            case GunTags.SMG:
                SKILLADD.UseBurstSkill(4, pluse_bullet, attackValue, burst_RunTime, burstCool);
                break;
            case GunTags.SR:
                SKILLADD.UseBurstSkill(5, pluse_bullet, attackValue, burst_RunTime, burstCool);
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
        Name = info.Img;
    }


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    // Update is called once per frame
    void Update()
    {
        //upperStateEnum();
        //lowerStateEnum();
        // move();

        runcheck();
        move();
        attackRot();
        bool value1 = Input.GetMouseButton(0);
        bool value2 = Input.GetMouseButtonUp(0);
        if ((value1))
        {
            attack();
        }
        else if (value2 || gun.nowbullet <= 0)
        {
            string text = ($"{PlayerAnimParameters.Attack}");
            Shared.BattelMgr.MOVECAM.cameraShakeAnim(false);
            playerAnim.SetInteger(text, 0);
        }
        reloding();
        closeAttackCheack();
        shitdownCheak();
        ////Time.timeScale = 0;//Faraim Speed up,Down
    }

    private void attack()
    {
        Vector3 AimPos = Shared.BattelMgr.camAim.transform.position;
        Vector3 AimDirection = Shared.BattelMgr.camAim.transform.forward;
        string text = ($"{PlayerAnimParameters.Attack}");
        if (Physics.Raycast(AimPos, AimDirection, out RaycastHit hit))
        {
            float value = Vector3.Dot(AimDirection.normalized, gun.gunHoleObj.transform.forward.normalized);
            if (value <= 1.0f && gun.reLoed == false && gun.nowbullet >= 0)
            {
                playerAnim.SetLayerWeight(attackLayerIndex, 1.0f);
                playerAnim.SetInteger(text, 1);
                gun.GunAttack(AimDirection);//����
            }
            else if (gun.reLoed == true || gun.nowbullet <= 0)
            {
                Shared.BattelMgr.MOVECAM.cameraShakeAnim(false);
                playerAnim.SetInteger(text, 0);
            }
            else { return; }
        }
    }
    public void reloding() 
    {
        string text = ($"{PlayerAnimParameters.Reload}");
        string text2 = ($"{playerAnimInfoName.reloading}");
        bool reload = Input.GetKeyDown(KeyCode.R);
        if (reload || gun.nowbullet <= 0)
        {
            gun.reLoed = true;
            playerAnim.SetLayerWeight(attackLayerIndex, 1.0f);
            playerAnim.SetInteger(text, 1);
        }

        if (gun.reLoed)
        {
            animCheck(text, text2);
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

    
    
    public void attackRot() 
    {
        Vector3 pos = Shared.BattelMgr.camAim.transform.forward;
        transform.rotation = Quaternion.Euler(pos); 
    }
}
