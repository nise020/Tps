using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BattelManager : MonoBehaviour
{
    //�ѱ� �� �����
    [SerializeField] public List<Soljer> PLAYER;
    [SerializeField, Tooltip("���� ����")] public List<bool> AttackSearch;
    [SerializeField] List< Monster> MONSTEROBJ;

    [SerializeField] DefoltMob defoltMob;// = Resources.Load<DefoltMob>("Fab_DefoltMob");
    [SerializeField] FlyingMob flyingMob;// = Resources.Load<FlyingMob>("Fab_FlyMob");
    [SerializeField] HugeMob hugeMob;// = Resources.Load<HugeMob>("Fab_HugMob");

    [SerializeField, Tooltip("���买")] List<GameObject> COVER;
    [SerializeField, Tooltip("Stage Level�� ���� Stage ���� ��ġ")] List<GameObject> STAGE;

    [Header("Defolt ���� ����")]
    [SerializeField] List<GameObject> StageSpowneObj;//SpowneObjectList

    //[Tooltip("Defolt ���� ���� ����")] List<GameObject> DefoltSpones;//Stage Lavel

    //[Header("Flying ���� ����")]
    //[SerializeField] List<GameObject> FlyingSpowne;//SpowneObjectList,Find("Fab_")
    //[Tooltip("Flying ���� ���� ����")] List<GameObject> FlyingSpones;//Stage Lavel

    //[Header("Huge ���� ����")]
    //[SerializeField] List<GameObject> HugeSpowne;//SpowneObjectList,Find("Fab_")
    //[Tooltip("Huge ���� ���� ����")] List<GameObject> HugeSpones;//Stage Lavel

    public Transform creatTab;
    public int targetNum;
    //Vector3 targetPos;
    [SerializeField, Tooltip("������ ������ ����")] int Maxcount = 1;
    int Mincount = 0;
    int Monster_Count = 0;
    Dictionary<int, Charactor> CHARACTORDATA = new Dictionary<int, Charactor>();
    public Dictionary<int, GameObject> monster_Data = new Dictionary<int, GameObject>();
    float spownTimer = 0.0f;
    float spownTime = 1.0f;
    int MobId = 0;
    int stageLevel = 0;
    private void Start()
    {
        //DefoltSpones.InsertRange(0, DefoltSpowne);
        //FlyingSpones.InsertRange(0, FlyingSpowne);
        //HugeSpones.InsertRange(0, HugeSpowne);
    }
    private void Update()
    {
        Timer();
    }
    private void SpownListArrange() 
    {
        GameObject stage = STAGE[stageLevel];//�������� �ҷ�����

        foreach (Transform spawnPoint in stage.transform)
        {
            LayerMask Layer = spawnPoint.gameObject.layer;
            string layerName = LayerMask.LayerToName(Layer);

            spawnByType(layerName, spawnPoint.position);
        }
    }
    private void spawnByType(string _layername, Vector3 _spawnTrs)
    {
        if (Mincount >= Maxcount) { return; }

        Mincount += 1;

        int count1 = MONSTEROBJ.Count;
        count1 = Random.Range(0, count1);
        int Level = STAGE.Count;
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
                GO = hugeMob.gameObject;
                break;
        }

        GameObject go = Instantiate(GO, _spawnTrs, Quaternion.identity, creatTab);

        Monster monster = GO.GetComponent<Monster>();
        monster_Data.Add(Monster_Count, GO);
        monster.mobKey = Monster_Count;
        Monster_Count += 1;

    }

    public void Timer() //�̰� �ΰ��� �ð����� ��� ����
    {
        spownTimer += Time.deltaTime;
        if (spownTimer >= spownTime) 
        {
            spownTimer = 0.0f;
            SpownListArrange();

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
        //chageScene(eScene.Title);
    }

    //IEnumerator Timer()
    //{

    //    yield return new WaitForSeconds(1);

    //}


    //Charactor Factory(eMobType _e)//���丮 ���� 
    //{
    //    Charactor c = null;
    //    switch (_e)
    //    {
    //        case eMobType.Flying:
    //            c = new FlyingMob();
    //            break;
    //        case eMobType.Huge:
    //            c = new HugeMob();
    //            break;
    //        case eMobType.Defolt:
    //            c = new DefoltMob();
    //            break;
    //    }
    //    return c;
    //}

}
