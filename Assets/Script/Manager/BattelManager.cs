using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BattelManager : MonoBehaviour
{
    //한글 꼭 지우기
    [SerializeField, Tooltip("플레이어블")] public List<Soljer> PLAYER;
    [SerializeField, Tooltip("공격 감지")] public List<bool> AttackSearch;
    [SerializeField, Tooltip("몬스터")] List< Monster> MONSTEROBJ;
    [SerializeField, Tooltip("엄페물")] List<GameObject> COVER;
    [SerializeField, Tooltip("Stage Level에 따른 Stage 스폰 위치")] List<GameObject> STAGE;

    [Header("Defolt 생성 지점")]
    [SerializeField, Tooltip("Defolt 생성 지점")] List<GameObject> DefoltSpowne;
    [Tooltip("Defolt 생성 지점 모음")] List<GameObject> DefoltSpones;

    [Header("Flying 생성 지점")]
    [SerializeField, Tooltip("Flying 생성 지점")] List<GameObject> FlyingSpowne;
    [Tooltip("Flying 생성 지점 모음")] List<GameObject> FlyingSpones;

    [Header("Huge 생성 지점")]
    [SerializeField, Tooltip("Huge 생성 지점")] List<GameObject> HugeSpowne;
    [Tooltip("Huge 생성 지점 모음")] List<GameObject> HugeSpones;

    public Transform creatTab;
    public int targetNum;
    //Vector3 targetPos;
    [SerializeField, Tooltip("스폰할 못스터 숫자")] int Maxcount = 1;
    int Mincount = 0;
    int Monster_Count = 0;
    Dictionary<int, Charactor> CHARACTORDATA = new Dictionary<int, Charactor>();
    Dictionary<int, GameObject> monster_Data = new Dictionary<int, GameObject>();
    float spownTimer = 0.0f;
    float spownTime = 1.0f;
    int MobId = 0;
    int stageLevel = 1;

    private void Update()
    {
        Timer();
    }
    //public static void UserData(UnityWebRequest _www)
    //{
    //    //JSONNode.Parse(_www)
    //}
    private void Stage() 
    {
        switch (stageLevel) 
        {
            case 1:
                creatMob();
                break;
            case 2:
                creatMob();
                break;
            case 3:
                creatMob();
                break;
        }
    }
    public void Timer() //이걸 인게임 시간으로 사용 예정
    {
        spownTimer += Time.deltaTime;
        if (spownTimer >= spownTime) 
        {
            spownTimer = 0.0f;
            Stage();
            creatMob();
        }

    }
    private void creatMob()
    {
        if (Mincount >= Maxcount) { return; }

        Monster_Count += 1;
        Mincount += 1;

        int count1 = MONSTEROBJ.Count;
        count1 = Random.Range(0, count1);
        int count2 = 0;
        int Level = STAGE.Count;
        GameObject GO = null;
        switch (MONSTEROBJ[count1].eType) 
        {
            case eMobType.Defolt:
                count2 = 1;
                 GO = Instantiate(MONSTEROBJ[count1].gameObject, DefoltSpowne[count2].transform.position, Quaternion.identity, creatTab);
                break;
            case eMobType.Flying:
                count2 = 2;
                GO = Instantiate(MONSTEROBJ[count1].gameObject, FlyingSpowne[count2].transform.position, Quaternion.identity, creatTab);
                break;
            case eMobType.Huge:
                count2 = 3;
                GO = Instantiate(MONSTEROBJ[count1].gameObject, HugeSpowne[count2].transform.position, Quaternion.identity, creatTab);
                break;
        }
        Monster monster = GO.GetComponent<Monster>();
        monster_Data.Add(Monster_Count, GO);
        monster.mobKey = Monster_Count;



        //HugeSpowne[0].gameObject = gameObject.Find("d");




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

    //IEnumerator Timer()
    //{

    //    yield return new WaitForSeconds(1);

    //}


    //Charactor Factory(eMobType _e)//팩토리 패턴 
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
