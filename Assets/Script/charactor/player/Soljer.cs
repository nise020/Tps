using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Soljer : Charactor
{
    GameManager gameManager;
    [Header("무기")]
    [SerializeField] GameObject WeaponPrefab;
    [SerializeField] GameObject[] MobPrefab;//몬스터
    //[SerializeField] GameObject GunHoleObj;//총구
    //[SerializeField] Transform WeapontransPos;//무기

    [Header("조준점")]
    [SerializeField] Transform AimtransPos;//명중 오브젝트
    [SerializeField] Button ControlBtn;

    [Header("스텟")]
    float hp = 100.0f;
    float damage = 10f;
    float burst_RunTime;
    float burst_CoolTime;

    Camera Maincam;
    //Vector3 beforTrs;//

    [Header("총의 종류,총알")]
    protected int bullet;
    protected int pluse_bullet;
    protected int RelodingBullet;
    
    [Header("장전 시간,충전 시간")]
    protected float ChargeingTime;
    protected float ChargeingTimer = 0.0f;
    protected float RerodingTime = 3.0f;
    protected float RerodingTimer = 0.0f;
    [Header("스킬 관련")]

    [Header("버스트(궁) 관련")]

    Skill_Add skill_Add = new Skill_Add();
    protected GunTags GunEnumType;
    protected void LoadSkill() 
    {
        switch (GunEnumType)
        {
            case GunTags.AR:
                skill_Add.UseBurstSkill(1, pluse_bullet, damage, burst_RunTime,burst_CoolTime);
                break;
            case GunTags.MG:
                skill_Add.UseBurstSkill(2, pluse_bullet, damage, burst_RunTime, burst_CoolTime);
                break;
            case GunTags.SG:
                skill_Add.UseBurstSkill(3, pluse_bullet, damage, burst_RunTime, burst_CoolTime);
                break;
            case GunTags.SMG:
                skill_Add.UseBurstSkill(4, pluse_bullet, damage, burst_RunTime, burst_CoolTime);
                break;
            case GunTags.SR:
                skill_Add.UseBurstSkill(5, pluse_bullet, damage, burst_RunTime, burst_CoolTime);
                break;
        }
    }

    private void Start()
    {
        gameManager = GameManager.Instanse;
        Maincam = Camera.main;
    }

    public void BulletReturn(out int value)
    {
        value = bullet;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    // Update is called once per frame
    void Update()
    {
        AttackModPosition();
        GunFireCheck();
    }

    //상속
    //추상함수
    protected virtual void Reloding()
    {
        if (bullet == RelodingBullet) { return; }
        RerodingTimer += Time.deltaTime;
        Debug.Log("Reroding On");
        if (RerodingTimer >= RerodingTime)
        {
            bullet = RelodingBullet;
            RerodingTimer = 0.0f;
            //BulletCount.text = bullet.ToString();//찾을수 없다고 뜸

            Debug.Log("Reroding off");
        }
    }
    protected virtual void CameraAim()//무기 각도 회전
    {
        Vector2 mouseWorldPos = Maincam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        Vector2 Pos = mouseWorldPos - playerPos;
        float angle = Quaternion.FromToRotation(transform.localScale.x < 0 ? Vector3.left : Vector3.right, Pos).eulerAngles.z;
        //
        //WeapontransPos.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    protected virtual void GunFireCheck() //총구 애니메이션 on/off
    {
        
    }

    private void AttackModPosition()//애니메이션 실행+ 포지션 전환;
    {
        Vector3 scale = transform.localScale;
        if (Input.GetMouseButton(0) && bullet > 0)//총알이 남아있을 경우
        {
            //transform.position = new Vector3((beforTrs.x + 2.0f), beforTrs.y, beforTrs.z);
            //if (AimtransPos.transform.position.x >= transform.position.x)
            //{
            //    scale.x = Mathf.Abs(scale.x);
            //}
            //else
            //{
            //    scale.x = -Mathf.Abs(scale.x);
            //}
            //CameraAim();
            //GunFireCheck();
            ////Chargeing();

        }
        else
        {
            Reloding();
            //transform.position = beforTrs;
            //WeapontransPos.transform.rotation = Quaternion.Euler(0,0,beforAimtransPos.z);
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
        if (bullet <= 0)
        {
            bullet = 0;
        }
    }
}
