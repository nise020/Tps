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
    [SerializeField] GameObject GunHoleObj;//총구
    [SerializeField] Transform WeapontransPos;//무기

    [Header("조준점")]
    [SerializeField] Transform AimtransPos;//명중 오브젝트
    //[SerializeField] Text BulletCount;//현재 남아있는 총알
    [SerializeField] Button ControlBtn;

    [Header("스텟")]
    float HP = 10.0f;


    Camera Maincam;
    Vector3 beforTrs;
    Vector3 beforAimtransPos;
    Transform AfterTrs;
    float moveSpeed = 5f;

    [Header("총의 종류,총알")]
    protected int bullet;
    protected int RelodingBullet;
    public enum GunTags
    {
        MG,//머신건
        SMG,//기간단총
        SR,//저격총
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
    protected virtual void Chargeing()//총구 액션,(리로드 아님!)
    {
        GunHoleObj.SetActive(false);
        ChargeingTimer += Time.deltaTime;
        if (ChargeingTimer > ChargeingTime)//마우스 땟을때 동작 필요
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
        WeapontransPos.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    protected virtual void GunFireCheck() //총구 애니메이션 on/off
    {
        //if (MobPrefab[0].transform.position == GunHoleObj.transform.position)//MobPrefab[0]<--임시
        //{
        //    GunHoleObj.SetActive(true);
        //    if (GunHoleObj.activeSelf == true) 
        //    {

        //    }
        //}
    }

    private void AttackModPosition()//애니메이션 실행+ 포지션 전환;
    {
        Vector3 scale = transform.localScale;
        if (Input.GetMouseButton(0) && bullet > 0)//총알이 남아있을 경우
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
