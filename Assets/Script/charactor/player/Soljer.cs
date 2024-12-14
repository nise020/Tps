using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Soljer : Charactor
{
    GameManager gameManager;
    [Header("����")]
    [SerializeField] GameObject WeaponPrefab;
    [SerializeField] GameObject[] MobPrefab;//����
    //[SerializeField] GameObject GunHoleObj;//�ѱ�
    //[SerializeField] Transform WeapontransPos;//����

    [Header("������")]
    [SerializeField] Transform AimtransPos;//���� ������Ʈ
    [SerializeField] Button ControlBtn;

    [Header("����")]
    float hp = 100.0f;
    float damage = 10f;
    float burst_RunTime;
    float burst_CoolTime;

    Camera Maincam;
    //Vector3 beforTrs;//

    [Header("���� ����,�Ѿ�")]
    protected int bullet;
    protected int pluse_bullet;
    protected int RelodingBullet;
    
    [Header("���� �ð�,���� �ð�")]
    protected float ChargeingTime;
    protected float ChargeingTimer = 0.0f;
    protected float RerodingTime = 3.0f;
    protected float RerodingTimer = 0.0f;
    [Header("��ų ����")]

    [Header("����Ʈ(��) ����")]

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

    //���
    //�߻��Լ�
    protected virtual void Reloding()
    {
        if (bullet == RelodingBullet) { return; }
        RerodingTimer += Time.deltaTime;
        Debug.Log("Reroding On");
        if (RerodingTimer >= RerodingTime)
        {
            bullet = RelodingBullet;
            RerodingTimer = 0.0f;
            //BulletCount.text = bullet.ToString();//ã���� ���ٰ� ��

            Debug.Log("Reroding off");
        }
    }
    protected virtual void CameraAim()//���� ���� ȸ��
    {
        Vector2 mouseWorldPos = Maincam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        Vector2 Pos = mouseWorldPos - playerPos;
        float angle = Quaternion.FromToRotation(transform.localScale.x < 0 ? Vector3.left : Vector3.right, Pos).eulerAngles.z;
        //
        //WeapontransPos.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    protected virtual void GunFireCheck() //�ѱ� �ִϸ��̼� on/off
    {
        
    }

    private void AttackModPosition()//�ִϸ��̼� ����+ ������ ��ȯ;
    {
        Vector3 scale = transform.localScale;
        if (Input.GetMouseButton(0) && bullet > 0)//�Ѿ��� �������� ���
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
