using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BattelManager : MonoBehaviour
{
    [SerializeField, Tooltip("플레이어블")] public List<GameObject> player;
    [SerializeField, Tooltip("몬스터")] List< GameObject> MONSTER;
    [SerializeField, Tooltip("엄페물")] List<GameObject> Concealment;
    [SerializeField, Tooltip("스폰 지점")] List<GameObject> spownObj;
    public Transform creatTab;
    public int targetNum;
    Vector3 targetPos;
    public int Maxcount = 1;
    public int Mincount = 0;
    public int Monster_Count = 0;
    Dictionary<int, Charactor> CHARACTORDATA = new Dictionary<int, Charactor>();
    Dictionary<int, GameObject> monster_Data = new Dictionary<int, GameObject>();
    float spownTimer = 0.0f;
    float spownTime = 1.0f;
    int MobId = 0;
    private void Update()
    {
        Timer();
    }
    public void Timer() 
    {
        spownTimer += Time.deltaTime;
        if (spownTimer >= spownTime) 
        {
            creatMob();
        }

    }
    private void creatMob()
    {
        if (Mincount >= Maxcount) { return; }

        Monster_Count += 1;

        int count1 = MONSTER.Count;
        count1 = Random.Range(0, count1);

        int count2 = spownObj.Count;
        count2 = Random.Range(0, count2);

        GameObject go = Instantiate(MONSTER[count1], spownObj[count2].transform.position,Quaternion.identity,creatTab);
        Monster monster = go.GetComponent<Monster>();
        monster_Data.Add(Monster_Count, go);
        monster.mobKey = Monster_Count;
        Mincount += 1;
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
