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

    public GameObject playerSpine;

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
    protected GunTags GunEnumType;//다른곳에서 전달 받기

    
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
        
        bool value1 = Input.GetMouseButton(0);
        bool value2 = Input.GetMouseButtonUp(0);
        if ((value1))
        {
            attack();
        }
        else if(value2 || gun.nowbullet <= 0) 
        {
            string text = ($"{PlayerAnimParameters.Attack}");
            Shared.BattelMgr.MOVECAM.cameraShakeAnim(false);
            playerAnim.SetInteger(text, 0);
            //playerAnim.SetLayerWeight(attacklayerIndex, 0.0f);
        }
        reloding();
        closeAttackCheack();
        shitdownCheak();
        //Time.timeScale = 0;//Faraim Speed up,Down
    }

    private void attack()
    {
        //attackRot();
        //reloding();
        Vector3 AimPos = Shared.BattelMgr.camAim.transform.position;
        Vector3 AimDirection = Shared.BattelMgr.camAim.transform.forward;

        if (Physics.Raycast(AimPos, AimDirection, out RaycastHit hit))
        {
            playerSpine.transform.localRotation = Quaternion.Euler(AimPos.normalized);//check
            float value = Vector3.Dot(AimDirection.normalized, playerSpine.transform.forward.normalized);
            if (value <= 1.0f && gun.reLoed == false && gun.nowbullet >= 0)
            {
                string text = ($"{PlayerAnimParameters.Attack}");

                playerAnim.SetLayerWeight(attacklayerIndex, 1.0f);
                playerAnim.SetInteger(text, 1);
                gun.GunAttack(AimDirection);//에러
            }
            else if (gun.reLoed == true || gun.nowbullet <= 0)
            {
                Shared.BattelMgr.MOVECAM.cameraShakeAnim(false);
            }
        }
    }
    public void reloding() 
    {
        bool cheack = Input.GetKeyDown(KeyCode.R);
        if ((cheack || gun.nowbullet <= 0 )&& !gun.reLoed)
        {
            string text = ($"{PlayerAnimParameters.Reload}");
            gun.reLoed = true;
            playerAnim.SetLayerWeight(attacklayerIndex, 1.0f);
            playerAnim.SetInteger(text, 1);    
        }

        if (gun.reLoed)
        {
            int index = attacklayerIndex;
            AnimatorStateInfo animStateInfo = playerAnim.GetCurrentAnimatorStateInfo(index);
            float time = animStateInfo.normalizedTime;
            string text2 = ($"{onesPractice.reloading}");//여기는 제대로 동작함
            Debug.Log($"{time}");
            if (time >= 1.0f && animStateInfo.IsName(text2))
            {
                string text = ($"{PlayerAnimParameters.Reload}");
                StartCoroutine(reLoadout(index));
                playerAnim.SetInteger(text, 0);
            }
            else 
            {
                return; 
            }
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

    
    
    public Quaternion attackRot(Vector3 _pos,Quaternion _rot) 
    {
        _rot = Quaternion.Euler(_pos); 
        return _rot;
    }
}
