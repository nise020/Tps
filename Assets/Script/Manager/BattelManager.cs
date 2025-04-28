using Photon.Realtime;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Networking;

public class BattelManager : MonoBehaviour
{
    public UI_Battle ui;
    //public List<MoveCamera> MOVECAM;
    public GameObject CamAim;

    public bool GameOver = false;
    ObjectType objType = ObjectType.None;

    [Header("Player")]
    //한글 꼭 지우기
    public Player PLAYER;
    public Warrior WARRIOR;
    public Gunner GUNNER;
    public bool PlayerAlive = false;
    public GameObject playerUpper;//상체
    public GameObject playerHand;//오른손
    PlayerControllState playerControll = PlayerControllState.Off;

    [Header("Gun")]
    public Gun GUN;
    public GameObject attackAim;
    [SerializeField] GameObject startPointObj;
    [SerializeField, Tooltip("공격 감지")] public List<bool> AttackSearch;
    [SerializeField] List< Monster> MONSTEROBJ;

    //Vector3 targetPos;
    int mincount = 0;
    int monsterCount = 0;
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
    public void Playerinit() 
    {

    }

    private void creatObject() 
    {
        //player
        CharctorStateEnum controll = CharctorStateEnum.Player;

        PLAYER.gameObject.transform.position = startPointObj.gameObject.transform.position;
        PLAYER.PlayerControllChange(controll);
        PlayerAlive = true;

        //playerCam.transform.position = PLAYER.transform.position;
        //MOVECAM.PlayerObj = PLAYER.gameObject;
        //Gun
        

        //monster
        //spownListArrange(STAGE[stageLevel]);

        //GameObject monster = Instantiate(GUN.gameObject, transform.position, Quaternion.identity, creatTab);
        
    }
   
    public void Timer() //이걸 인게임 시간으로 사용 예정
    {
        spownTimer += Time.deltaTime;
        if (spownTimer >= spownTime) 
        {
            spownTimer = 0.0f;
        }
    }

    private void Awake()
    {
        if (Shared.BattelManager == null)
        {
            Shared.BattelManager = this;
            //SceneMgr 싱글톤
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    

    public void DamageCheck(Charactor _attacker, Charactor _defender) 
    {
        float attaker = _attacker.StatusTypeLoad(StatusType.Power);
        float defenser = _defender.StatusTypeLoad(StatusType.HP);

        defenser = defenser - attaker;

        _defender.StatusUpLoad(defenser);

        //Status attackkerStatus = _attacker.StateLoad();
        //Status defenderStatus = _defender.StateLoad();

        //float value = defenderStatus.ViewHp - attackkerStatus.ViewAttack;
        //Debug.Log($"attackker = {_attacker}\n" +
              //    $"defender = {_defender}\n" +
                //  $"{_defender}HP ={defenderStatus.ViewHp}");
        //MonsterStatus.StatusInit(value);
        //_defender.StatusUpLoad(value);
        //vector Addforce
    }
    //public void DamageCheck(Monster _attackker, Player _defender)
    //{
    //    Status MonsterStatus = _attackker.StateLoad();
    //    Status playerStatus = _defender.StateLoad();

    //    float value = playerStatus.ViewHp - MonsterStatus.ViewAttack ;
    //    //playerStatus.StatusInit(value);
    //    _defender.StatusUpLoad(value);
    //}
}
