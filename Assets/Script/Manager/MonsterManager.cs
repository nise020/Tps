using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MonsterManager : MonoBehaviour
{
    [Header("Spown")]
    [SerializeField] List<GameObject> STAGE;
    [SerializeField, Tooltip("스폰할 못스터 숫자")] int Maxcount = 1;
    public Dictionary<int, Monster> monsterData = new Dictionary<int, Monster>();
    
    int stageLevel = 0;
    int mincount = 0;
    ObjectType objType = ObjectType.None;

    [Header("Monster")]
    [SerializeField] SpiderMob SpiderMob;
    [SerializeField] DronMob dronMob;
    [SerializeField] SphereMob sphereMob;
    [SerializeField] GameObject hpBarCanvers;
    [SerializeField] HpBar hpBarObj;
    [SerializeField] GameObject exflotionEffect;
    public List<Monster> MonsterList = new List<Monster>();
    public Dictionary<int, HpBar> hpData = new Dictionary<int, HpBar>();
    int monsterCount = 0;

    //[Header("CreatTab")]
    [SerializeField] GameObject creatTabObj;
    public Transform creatTab;
    //Item Item = new Item();

    //몬스터를 딕션어리로 관리
    //몬스터의 거리를 측정할 리스트 구현해서 관리
    private void Awake()
    {
        if (Shared.MonsterManager == null)
        {
            Shared.MonsterManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
        creatTab = Shared.GameManager.CreatTransform();
    }

    private void Start()
    {
        //creatTab = Shared.GameManager.CreatTransform();
        //CreatMonsterObject();
    }
    public void CreatMonsterObject()
    {
        //monster
        spownListArrange(STAGE[stageLevel]);
    }
    public Monster monsterSearch(GameObject _playerObj,float _radius) 
    {
        if (MonsterList.Count == 0) 
        {
            Debug.LogError($"MonsterList.Count ={MonsterList.Count}");
            return null;
        }
        Monster nearest = null;
        float minDist = _radius;// * _radius;

        for (int i = 0; i < MonsterList.Count; i++) 
        {
            Monster monster = MonsterList[i];

            if (monster.ConditionLoad()) 
            {
                float dist = Vector3.Distance(monster.BodyObjectLoad().position, _playerObj.transform.position);//.sqrMagnitude;
                if (dist <= minDist)
                {
                    //minDist = dist;
                    nearest = monster;
                    break;
                }
            }
        }

        return nearest;

    }
    public List<Monster> monsterListSearch(GameObject _playerObj, float _radius)
    {
        List<Monster> monsters = new List< Monster >();

        if (MonsterList.Count == 0)
        {
            Debug.LogError($"MonsterList.Count ={MonsterList.Count}");
            return null;
        }

        float minDist = _radius;// * _radius;

        for (int i = 0; i < MonsterList.Count; i++)
        {
            Monster monster = MonsterList[i];

            if (monster.ConditionLoad())
            {
                float dist = Vector3.Distance(monster.BodyObjectLoad().position, _playerObj.transform.position);//.sqrMagnitude;
                if (dist <= minDist)
                {
                    //minDist = dist;
                    monsters.Add(monster);
                    //break;
                }
            }
        }

        return monsters;

    }
    private void spownListArrange(GameObject _stage)
    {
        foreach (Transform spawnPoint in _stage.transform)
        {
            LayerMask Layer = spawnPoint.gameObject.layer;
            string layerName = LayerMask.LayerToName(Layer);

            spawnByType(layerName, spawnPoint.position, mincount, Maxcount, objType);
        }

    }
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
        monsterData.Add(monsterCount, monster);

        monster.KeyUpdate(monsterCount);

        monster.creatTab(creatTab);

        Shared.BattelManager.AddDataToCharcterList(ObjectType.Monster, monster);
        Shared.ItemManager.ItemDataAddToMonster(monster);
        //monster.Type;


        MonsterList.Add(monster);
        monsterCount += 1;

        HpBarValue(Maxcount, monsterCount, monster);//<-

    }
    public void HpBarValue(int _max, int _min, Monster _monster)
    {
        HpBar hpBar = _monster.GetComponentInChildren<HpBar>();
        hpBar.CharactorInIt(_monster);

        _monster.HpInIt(hpBar);

        //hpData.Add(_min, hpBar);

        //hpBar.key = _min;
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
    public void Resurrection(int _number)
    {
        Monster monster = monsterData[_number];
        StartCoroutine(ResurrectionTimer(monster.gameObject, 3.0f));
        //Invoke("monsterData[_number].gameObject.SetActive(true)",10f);
    }
    private IEnumerator ResurrectionTimer(GameObject _obj, float _time)
    {
        yield return new WaitForSeconds(_time);
        Monster monster = _obj.GetComponent<Monster>();

        //Shared.ItemManager.ItemDataAdd(monster);
        monster.conditionUpdate(Condition.health);

        Transform monsterBody = monster.BodyObjectLoad();
        monsterBody.gameObject.SetActive(true);

        HpBar hpBar = monster.GetComponentInChildren<HpBar>();
        hpBar.init();
    }
    public void PlayerCameraUpdate() 
    {
        foreach (var(numder,hpbar) in hpData)
        {
            hpbar.PlayerUpdate();
        }
    }
}
