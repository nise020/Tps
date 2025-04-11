using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    //몬스터를 딕션어리로 관리
    //몬스터의 거리를 측정할 리스트 구현해서 관리
    private void Awake()
    {
        if (Shared.MonsterManager == null)
        {
            Shared.MonsterManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        creatObject();
    }
    private void creatObject()
    {
        //player
        CharctorStateEnum controll = CharctorStateEnum.Player;

        //PLAYER.gameObject.transform.position = startPointObj.gameObject.transform.position;
        //PLAYER.PlayerControllChange(controll);
        //PlayerAlive = true;

        //playerCam.transform.position = PLAYER.transform.position;
        //MOVECAM.PlayerObj = PLAYER.gameObject;
        //Gun


        //monster
        spownListArrange(STAGE[stageLevel]);

        //GameObject monster = Instantiate(GUN.gameObject, transform.position, Quaternion.identity, creatTab);

    }
    [Header("Spown")]
    [SerializeField] List<GameObject> STAGE;
    [SerializeField, Tooltip("스폰할 못스터 숫자")] int Maxcount = 1;
    public Dictionary<int, GameObject> monsterData = new Dictionary<int, GameObject>();
    int stageLevel = 0;
    int mincount = 0;
    ObjectType objType = ObjectType.None;
    private void spownListArrange(GameObject _stage)
    {
        foreach (Transform spawnPoint in _stage.transform)
        {
            LayerMask Layer = spawnPoint.gameObject.layer;
            string layerName = LayerMask.LayerToName(Layer);

            spawnByType(layerName, spawnPoint.position, mincount, Maxcount, objType);
        }
    }
    [Header("Monster")]
    [SerializeField] SpiderMob SpiderMob;
    [SerializeField] DronMob dronMob;
    [SerializeField] SphereMob sphereMob;
    [SerializeField] GameObject hpBarCanvers;
    [SerializeField] HpBar hpBarObj;
    [SerializeField] GameObject exflotionEffect;
    public Dictionary<int, GameObject> hpData = new Dictionary<int, GameObject>();
    int monsterCount = 0;

    [Header("CreatTab")]
    public Transform creatTab;
    private void spawnByType(string _layername, Vector3 _spawnTrs, int _min, int _max, ObjectType _type)
    {
        if (_min >= _max) { return; }

        _min += 1;
        GameObject monsterType = null;
        switch (_layername) //수정 필요
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
        _type = ObjectType.Monster;

        Monster monster = go.GetComponent<Monster>();
        monsterData.Add(monsterCount, go);
        monster.mobIndex(monsterCount);
        monster.creatTab(creatTab);
        monster.TypeInit(_type);
        monster.BomEffect(exflotionEffect);
        HpBarValue(hpBarCanvers, Maxcount, monsterCount, monster);
        monsterCount += 1;

    }
    public void HpBarValue(GameObject _hpBarCanvers, int _max, int _min, Monster _monster)
    {
        HpBar hpBar = _monster.GetComponentInChildren<HpBar>();

        //RectTransform rect = hpBar.gameObject.GetComponent<RectTransform>();
        //rect.localPosition = new Vector3(0, 0.5f, 0);

        //float monsterHeight = _monster.GetComponent<Collider>().bounds.size.y;
        //float BaseHeight = 1.0f;
        //float scaleMultiplier = monsterHeight / BaseHeight;
        //rect.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1);


        //HpBar hp = _monster.GetComponent<HpBar>();
        _monster.HpInIt(hpBar);
        hpData.Add(_min, _monster.gameObject);

        hpBar.key = _min;
        hpBar.inIt(_monster);
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
}
