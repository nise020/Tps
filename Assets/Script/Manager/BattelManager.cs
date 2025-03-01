using Photon.Realtime;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BattelManager : MonoBehaviour
{
    public BattelUI ui;
    //public Camera playerCam;
    [SerializeField] GameObject playerCam;
    public MoveCamera MOVECAM;
    public GameObject CamAim;

    public bool GameOver = false;

    [Header("Player")]
    //�ѱ� �� �����
    public Player PLAYER;
    public bool PlayerAlive = false;
    public GameObject playerUpper;//��ü
    public GameObject playerHand;//������

    [Header("Gun")]
    public Gun GUN;
    public GameObject attackAim;
    [SerializeField] GameObject startPointObj;
    [SerializeField, Tooltip("���� ����")] public List<bool> AttackSearch;
    [SerializeField] List< Monster> MONSTEROBJ;

    [Header("Monster")]
    [SerializeField] SpiderMob SpiderMob;
    [SerializeField] DronMob dronMob;
    [SerializeField] SphereMob sphereMob;
    [SerializeField] GameObject hpBarCanvers;
    [SerializeField] HpBar hpBarObj;
    [SerializeField] GameObject exflotionEffect;
    public Dictionary<int, GameObject> hpData = new Dictionary<int, GameObject>();

    [Header("Spown")]
    [SerializeField] List<GameObject> STAGE;
    [SerializeField, Tooltip("������ ������ ����")] int Maxcount = 1;
    public Dictionary<int, GameObject> monsterData = new Dictionary<int, GameObject>();

    [Header("Defolt ���� ����")]
    [SerializeField] BattelUI BATTELUI;

    [Header("CreatTab")]
    public Transform creatTab;
    public Transform uiHpTab;
    public int targetNum;

    //Vector3 targetPos;
    int mincount = 0;
    int monsterCount = 0;
    //public Dictionary<int, Monster> monsterData = new Dictionary<int, Monster>();
    float spownTimer = 0.0f;
    float spownTime = 1.0f;
    int MobId = 0;
    int stageLevel = 0;

    //�۾� �ؾ� �Ұ�
    //start ���� �̸� ������Ʈ ���� �� �� Ȱ��ȭ+key ���� �ο�
    //������ ��Ȱ��ȭ
    //���� �ð� �� ��Ȱ(��ġ�� ���� ��ġ)
    //���ͳ� �÷��̾� ������ ����� �����۵� ����
    
    private void Start()
    {
        creatObject();
    }
    private void creatObject() 
    {
        //player
        PLAYER.gameObject.transform.position = startPointObj.gameObject.transform.position;
        PlayerAlive = true;

        playerCam.transform.position = PLAYER.transform.position;
        MOVECAM.PlayerObj = PLAYER.gameObject;
        //Gun

        //monster
        spownListArrange(STAGE[stageLevel]);

        //GameObject monster = Instantiate(GUN.gameObject, transform.position, Quaternion.identity, creatTab);
        
    }
    private void spownListArrange(GameObject _stage) 
    {
        foreach (Transform spawnPoint in _stage.transform)
        {
            LayerMask Layer = spawnPoint.gameObject.layer;
            string layerName = LayerMask.LayerToName(Layer);

            spawnByType(layerName, spawnPoint.position, mincount,Maxcount);
        }
    }
    public bool GetMonsterPosition(int _value, out Vector3 _pos) 
    {
        _pos = Vector3.zero;
        if (monsterData[_value] == null) 
        {
            return false;
        }
        else
        {
            _pos = monsterData[_value].transform.position;
            return true;
        }
    }
    public bool GetPlayerPosition(out Vector3 _pos) 
    {
        _pos = default;
        if (PLAYER == null)
        {
            return false;
        }
        else
        {
            _pos = PLAYER.transform.position;
            return true;
        }
    }

    private void spawnByType(string _layername, Vector3 _spawnTrs,int _min,int _max)
    {
        if (_min >= _max) { return; }

        _min += 1;
        GameObject monsterType = null;
        switch (_layername) //���� �ʿ�
        {
            case "SpawnSpider":
                monsterType = SpiderMob.gameObject;
                break;
            case "SpawnDron":
                monsterType = dronMob.gameObject;
                break;
            case "SpawnSphere":
                monsterType = sphereMob.gameObject;
                break;
        }

        GameObject go = Instantiate(monsterType, _spawnTrs, Quaternion.identity, creatTab);

        Monster monster = go.GetComponent<Monster>();
        monsterData.Add(monsterCount, go);
        monster.mobKey = monsterCount;
        monster.deadEffect = exflotionEffect;
        CreatHpBar(hpBarCanvers, Maxcount, monsterCount, monster);
        monsterCount += 1;

    }

    public void CreatHpBar(GameObject _hpBarCanvers, int _max,int _min, Monster _monster)
    {
        float monsterHeight = _monster.GetComponent<Collider>().bounds.size.y;

        GameObject go = Instantiate(_hpBarCanvers.gameObject, _monster.transform);

        RectTransform rect = go.GetComponent<RectTransform>();
        rect.localPosition = new Vector3(0, 0.5f, 0);

        float BaseHeight = 1.0f;
        float scaleMultiplier = monsterHeight / BaseHeight;
        rect.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1);


        HpBar hp = go.GetComponent<HpBar>();
        _monster.HpInIt(hp);
        hpData.Add(_min, go);

        hp.key = _min;
        hp.inIt(_monster);
    }

    public void Timer() //�̰� �ΰ��� �ð����� ��� ����
    {

        spownTimer += Time.deltaTime;
        if (spownTimer >= spownTime) 
        {
            spownTimer = 0.0f;
        }
    }

    private void Awake()
    {
        if (Shared.BattelMgr == null)
        {
            Shared.BattelMgr = this;
            //SceneMgr �̱���
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Resurrection() 
    {

    }
}
