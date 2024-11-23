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
    [SerializeField] GameObject GunHoleObj;//�ѱ�
    [SerializeField] Transform WeapontransPos;//����

    [Header("������")]
    [SerializeField] Transform AimtransPos;//���� ������Ʈ
    //[SerializeField] Text BulletCount;//���� �����ִ� �Ѿ�
    [SerializeField] Button ControlBtn;

    [Header("����")]
    float HP = 10.0f;


    Camera Maincam;
    Vector3 beforTrs;
    Vector3 beforAimtransPos;
    Transform AfterTrs;
    float moveSpeed = 5f;

    [Header("���� ����,�Ѿ�")]
    protected int bullet;
    protected int RelodingBullet;
    public enum GunTags
    {
        MG,//�ӽŰ�
        SMG,//�Ⱓ����
        SR,//������
    }
    [SerializeField] public GunTags GunType;
    protected float ChargeingTime;
    protected float ChargeingTimer = 0.0f;
    protected float RerodingTime = 3.0f;
    protected float RerodingTimer = 0.0f;

    private void Awake()
    {
        GunHoleObj.gameObject.SetActive(false);
        beforTrs = this.transform.position;
        beforAimtransPos = WeapontransPos.position;
        Maincam = Camera.main;
    }

    public void BulletReturn(out int value)
    {
        value = bullet;
    }
    void Start()
    {
        gameManager = GameManager.Instanse;

    }

    // Update is called once per frame
    void Update()
    {
        //AttackModPosition();
        //GunFireCheck();
    }
    protected virtual void Chargeing()//�ѱ� �׼�,(���ε� �ƴ�!)
    {
        GunHoleObj.SetActive(false);
        ChargeingTimer += Time.deltaTime;
        if (ChargeingTimer > ChargeingTime)//���콺 ������ ���� �ʿ�
        {
            if (GunType == GunTags.SR && Input.GetKeyUp(KeyCode.Mouse0))
            {
                Debug.Log("SR");
                GunHoleObj.SetActive(true);
                ChargeingTimer = 0.0f;
            }
            else
            {
                Debug.Log("Not SR");
                GunHoleObj.SetActive(true);
                ChargeingTimer = 0.0f;
            }
        }
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
        WeapontransPos.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    protected virtual void GunFireCheck() //�ѱ� �ִϸ��̼� on/off
    {
        //if (MobPrefab[0].transform.position == GunHoleObj.transform.position)//MobPrefab[0]<--�ӽ�
        //{
        //    GunHoleObj.SetActive(true);
        //    if (GunHoleObj.activeSelf == true) 
        //    {

        //    }
        //}
    }

    private void AttackModPosition()//�ִϸ��̼� ����+ ������ ��ȯ;
    {
        Vector3 scale = transform.localScale;
        if (Input.GetMouseButton(0) && bullet > 0)//�Ѿ��� �������� ���
        {
            transform.position = new Vector3((beforTrs.x + 2.0f), beforTrs.y, beforTrs.z);
            if (AimtransPos.transform.position.x >= transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x);
            }
            else
            {
                scale.x = -Mathf.Abs(scale.x);
            }
            CameraAim();
            GunFireCheck();
            Chargeing();

        }
        else
        {
            Reloding();
            transform.position = beforTrs;
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
