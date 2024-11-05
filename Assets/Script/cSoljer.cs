using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cSoljer : MonoBehaviour
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
    public int bullet;
    public int RerodingBullet;
    public enum GunTags
    {
        MG,//머신건
        SMG,//기간단총
        SR,//저격총
    }
    [SerializeField] public GunTags GunType;
    float ChargeingTime;
    float ChargeingTimer = 0.0f;
    float RerodingTime = 3.0f;
    float RerodingTimer = 0.0f;
    
    private void Awake()
    {
        GunHoleObj.gameObject.SetActive(false);
        beforTrs = this.transform.position;
        beforAimtransPos = WeapontransPos.position;
        Maincam = Camera.main;
    }
    private void GunBulletType()//총의 종류
    {
        if (GunType == GunTags.MG)
        {
            bullet = 300;
            ChargeingTime = 0.1f;
        }
        else if (GunType == GunTags.SMG)
        {
            bullet = 30;
            ChargeingTime = 0.2f;
        }
        else if (GunType == GunTags.SR)
        {
            bullet = 5;
            ChargeingTime = 3.0f;
        }
        RerodingBullet = bullet;
        //BulletCount.text = bullet.ToString();
    }
    public void BulletReturn(out int value) 
    {
         value = bullet;
    }
    void Start()
    {
        gameManager = GameManager.Instanse;
        GunBulletType();
    }

    // Update is called once per frame
    void Update()
    {
        AttackModPosition();
        //GunFireCheck();
    }
    private void Chargeing()//총구 액션,(리로드 아님!)
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
    private void Reloding() 
    {
        if (bullet == RerodingBullet) { return; }
        RerodingTimer += Time.deltaTime;
        Debug.Log("Reroding On");
        if (RerodingTimer >= RerodingTime) 
        {
            bullet = RerodingBullet;
            RerodingTimer = 0.0f;
            //BulletCount.text = bullet.ToString();//찾을수 없다고 뜸

            Debug.Log("Reroding off");
        }
    }
    private void CameraAim()//무기 각도 회전
    {
        Vector2 mouseWorldPos = Maincam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        Vector2 Pos = mouseWorldPos - playerPos;
        float angle = Quaternion.FromToRotation(transform.localScale.x < 0 ? Vector3.left : Vector3.right, Pos).eulerAngles.z;
        //
        WeapontransPos.transform.rotation = Quaternion.Euler(0,0, angle);
    }
    private void GunFireCheck() //총구 애니메이션 on/off
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
