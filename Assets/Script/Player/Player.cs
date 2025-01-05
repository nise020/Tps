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
    
    }

    private void move()
    {
        movePos.x = Input.GetAxisRaw("Horizontal");
        movePos.z = Input.GetAxisRaw("Vertical");

        transform.position += movePos * moveSpeed * Time.deltaTime;
    }

    //상속
    //추상함수
    //protected virtual void Reloding()
    //{
    //    if (bullet == RelodingBullet) { return; }
    //    RerodingTimer += Time.deltaTime;
    //    Debug.Log("Reroding On");
    //    if (RerodingTimer >= RerodingTime)
    //    {
    //        bullet = RelodingBullet;
    //        RerodingTimer = 0.0f;
    //        //BulletCount.text = bullet.ToString();//찾을수 없다고 뜸

    //        Debug.Log("Reroding off");
    //    }
    //}
    //protected virtual void CameraAim()//무기 각도 회전
    //{
    //    Vector2 mouseWorldPos = Maincam.ScreenToWorldPoint(Input.mousePosition);
    //    Vector2 playerPos = transform.position;
    //    Vector2 Pos = mouseWorldPos - playerPos;
    //    float angle = Quaternion.FromToRotation(transform.localScale.x < 0 ? Vector3.left : Vector3.right, Pos).eulerAngles.z;
    //    //
    //    //WeapontransPos.transform.rotation = Quaternion.Euler(0, 0, angle);
    //}
    //protected virtual void GunFireCheck() //총구 애니메이션 on/off
    //{

    //}

    //private void AttackModPosition()//애니메이션 실행+ 포지션 전환;
    //{
    //    Vector3 scale = transform.localScale;
    //    if (Input.GetMouseButton(0) && bullet > 0)//총알이 남아있을 경우
    //    {
    //        //transform.position = new Vector3((beforTrs.x + 2.0f), beforTrs.y, beforTrs.z);
    //        //if (AimtransPos.transform.position.x >= transform.position.x)
    //        //{
    //        //    scale.x = Mathf.Abs(scale.x);
    //        //}
    //        //else
    //        //{
    //        //    scale.x = -Mathf.Abs(scale.x);
    //        //}
    //        //CameraAim();
    //        //GunFireCheck();
    //        ////Chargeing();

    //    }
    //    else
    //    {
    //        Reloding();
    //        //transform.position = beforTrs;
    //        //WeapontransPos.transform.rotation = Quaternion.Euler(0,0,beforAimtransPos.z);
    //        scale.x = Mathf.Abs(scale.x);
    //    }
    //    transform.localScale = scale;
    //    if (bullet <= 0)
    //    {
    //        bullet = 0;
    //    }
    //}
}
