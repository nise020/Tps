using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BattelManager : MonoBehaviour
{
    //�ѱ� �� �����
    [SerializeField, Tooltip("�÷��̾��")] public List<Soljer> PLAYER;
    [SerializeField, Tooltip("���� ����")] public List<bool> AttackSearch;
    [SerializeField, Tooltip("����")] List< Monster> MONSTEROBJ;
    [SerializeField, Tooltip("���买")] List<GameObject> COVER;
    [SerializeField, Tooltip("Stage Level�� ���� Stage ���� ��ġ")] List<GameObject> STAGE;

    [Header("Defolt ���� ����")]
    [SerializeField, Tooltip("Defolt ���� ����")] List<GameObject> DefoltSpowne;
    [Tooltip("Defolt ���� ���� ����")] List<GameObject> DefoltSpones;

    [Header("Flying ���� ����")]
    [SerializeField, Tooltip("Flying ���� ����")] List<GameObject> FlyingSpowne;
    [Tooltip("Flying ���� ���� ����")] List<GameObject> FlyingSpones;

    [Header("Huge ���� ����")]
    [SerializeField, Tooltip("Huge ���� ����")] List<GameObject> HugeSpowne;
    [Tooltip("Huge ���� ���� ����")] List<GameObject> HugeSpones;

    public Transform creatTab;
    public int targetNum;
    //Vector3 targetPos;
    [SerializeField, Tooltip("������ ������ ����")] int Maxcount = 1;
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
    public void Timer() //�̰� �ΰ��� �ð����� ��� ����
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
