using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BattelManager : MonoBehaviour
{
    public Battel_UI ui;
    //public Camera playerCam;
    public MoveCamera MOVECAM;
    public GameObject camAim;

    public bool GameOver = false;


    //한글 꼭 지우기
    public Player PLAYER;
    public GameObject playerUpper;//상체
    public GameObject playerHand;//오른손
    public Gun GUN;
    public GameObject attackAim;
    [SerializeField] GameObject startPointObj;
    [SerializeField, Tooltip("공격 감지")] public List<bool> AttackSearch;
    [SerializeField] List< Monster> MONSTEROBJ;

    [SerializeField] SpiderMob defoltMob;
    [SerializeField] DronMob flyingMob;
    //[SerializeField] SphereMob sphereMob;

    [SerializeField, Tooltip("엄페물")] List<GameObject> COVER;
    [SerializeField, Tooltip("Stage Level에 따른 Stage 스폰 위치")] List<GameObject> STAGE;

    [Header("Defolt 생성 지점")]
    [SerializeField] List<GameObject> StageSpowneObj;//SpowneObjectList
    [SerializeField] Battel_UI BATTELUI;



    public Transform creatTab;
    public int targetNum;
    //Vector3 targetPos;
    [SerializeField, Tooltip("스폰할 못스터 숫자")] int Maxcount = 1;
    int mincount = 0;
    int monsterCount = 0;
    Dictionary<int, Charactor> CHARACTORDATA = new Dictionary<int, Charactor>();
    public Dictionary<int, GameObject> monsterData = new Dictionary<int, GameObject>();
    //public Dictionary<int, Monster> monsterData = new Dictionary<int, Monster>();
    float spownTimer = 0.0f;
    float spownTime = 1.0f;
    int MobId = 0;
    int stageLevel = 0;

    //작업 해야 할것
    //start 에서 미리 오브젝트 생성 후 비 활성화+key 값을 부여
    //죽으면 비활성화
    //일정 시간 후 부활(위치는 원래 위치)
    //몬스터나 플레이어 생성시 사용할 아이템도 생성

    private void Start()
    {
        //creatObject();
        //spownListArrange();
    }
    private void creatObject() 
    {
        //player
        GameObject player = Instantiate(PLAYER.gameObject, startPointObj.transform.position, Quaternion.identity);


        //Gun
        GameObject gun = Instantiate(GUN.gameObject,transform.position, Quaternion.identity, playerHand.transform);
        Gun guns = gun.GetComponent<Gun>();
        guns.GunBulletType();
        guns.creatbullet();

        //buulet

        //monster
        GameObject monster = Instantiate(GUN.gameObject, transform.position, Quaternion.identity, playerHand.transform);
    }
    private void Update()
    {
        //Timer();
    }
    private void spownListArrange() 
    {
        GameObject stage = STAGE[stageLevel];//스테이지 불러오기

        foreach (Transform spawnPoint in stage.transform)
        {
            LayerMask Layer = spawnPoint.gameObject.layer;
            string layerName = LayerMask.LayerToName(Layer);

            spawnByType(layerName, spawnPoint.position);
        }
    }
    private void spawnByType(string _layername, Vector3 _spawnTrs)
    {
        if (mincount >= Maxcount) { return; }

        mincount += 1;
        GameObject GO = null;
        switch (_layername) 
        {
            case "SpawnDefolt":
                GO = defoltMob.gameObject;
                break;
            case "SpawnFlying":
                GO = flyingMob.gameObject;
                break;
            case "SpawnHuge":
                //GO = hugeMob.gameObject;
                break;
        }

        GameObject go = Instantiate(GO, _spawnTrs, Quaternion.identity, creatTab);

        Monster monster = GO.GetComponent<Monster>();
        monsterData.Add(monsterCount, GO);
        monster.mobKey = monsterCount;
        monsterCount += 1;

    }

    public void Timer() //이걸 인게임 시간으로 사용 예정
    {

        spownTimer += Time.deltaTime;
        if (spownTimer >= spownTime) 
        {
            spownListArrange();
            spownTimer = 0.0f;
        }
        //float ScaleTime = 0.2f;
        //float SlowTime = 3f;
        //float SlowTimeTimeConvertSlow = ScaleTime * SlowTime;
        //Shared.MainCamera.ZoomEndStage(0f, -1.5f, 2f, SlowTime - 1.5f, 1f, Vector3.zero);
    }

    private void Awake()
    {
        if (Shared.BattelMgr == null)
        {
            Shared.BattelMgr = this;
            //SceneMgr 싱글톤
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //chageScene(eScene.Title);
    }

  
}
