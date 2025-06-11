//using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    //SceneMgr 기능별로 나눌수 있다

    [SerializeField] Button[] PlayerButtons;//현재 조작 중인 플블
    Dictionary<Player, GameObject> PlayerData = new Dictionary<Player, GameObject>();
    int playerKey = 0;

    [Header("Player Charactor")]
    [SerializeField] Player PLAYER;
    [SerializeField] Gunner GUNNER;
    [SerializeField] Warrior WARRIOR;
    public List<Player> PlayerObj;//플블 번호
    [SerializeField] GameObject startPointObj;


    [Header("Monster")]
    [SerializeField] GameObject[] MobObj;
    int Playerbullet;
    [Header("Player Charactor")]
    [SerializeField] GameObject creatTabObj;
    public Transform creatTab;

    public void ResorsLoad() 
    {
        //GUNNER = Resources.Load($"");


    }
    private void Creatplayer() 
    {
        GameObject go = Instantiate(WARRIOR.gameObject, startPointObj.gameObject.transform.position,
            Quaternion.identity);
        Character charactor = go.GetComponent<Character>();
        //Charactor player = Factory.CreateCharactor(ObjectType.Player);
        //charactor = player;

    }
 
    //public void onbtnTitle() 
    //{
    //    //SceneManager.LoadScene("Login");
    //    //shared.SceneMgr.chageSecen
    //}
    private void Awake()
    {
        if (Shared.GameManager == null)
        {
            Shared.GameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //CharctorAdd(PlayerModeState.Npc, GUNNER);
        //CharctorAdd(PlayerModeState.Player, WARRIOR);
    }
    void Start()
    {
        CharctorAdd(PlayerModeState.Npc,GUNNER);
        CharctorAdd(PlayerModeState.Player,WARRIOR);
        FindPlayer();

        Shared.MonsterManager.CreatMonsterObject();

        PLAYER.gameObject.transform.position = startPointObj.gameObject.transform.position;
        playerKey = 0;

    }
    public Transform CreatTransform()
    {
        if (creatTab) 
        {
            return creatTab;
        }
        return null;
    }
    public void FindPlayer() 
    {
        foreach (KeyValuePair<Player, GameObject> playerData in PlayerData)
        {
            Player player = playerData.Key;
            player.PlayerControllChack(out PlayerModeState _type);
            if (_type == PlayerModeState.Player)
            {
                PLAYER = player;
                Camera camera = PLAYER.GetComponentInChildren<Camera>();
                Shared.CameraManager.CameraChange(camera);
                break;
            }
            //int count = playerData.Value;
        }
    }
    public Player PlayerLoad() 
    {
        return PLAYER;
    }
    public Player PlayerDataLoad(PlayerType _job)
    {
        if (_job ==PlayerType.Gunner) 
        {
            return GUNNER;
        }
        else if (_job == PlayerType.Warrior)
        {
            return WARRIOR;
        }
        return null;
    }
    public void PlayerLoad(out Player _player) 
    {
        if (PLAYER == null) 
        {
            _player = WARRIOR;//임시
            //Shared.UiManager.UI_BATTEL.CharactorControllButten1();
            return;
        }
        _player = PLAYER;
    }
    public Vector3 PlayerPos(Vector3 _pos)
    {
        _pos = PLAYER.gameObject.transform.position;
        return _pos;
    }
    public void CharctorContoll(Player _player, PlayerModeState _state) 
    {
        _player.PlayerControllChange(_state);//On,Off
        if (_state == PlayerModeState.Player) 
        {
            PLAYER = _player;         
        }
    }
    private void CharctorAdd(PlayerModeState _type, Player _player) 
    {
        _player.PlayerModeUpdate(_type);
        PlayerData.Add(_player,_player.gameObject);
        playerKey += 1;
        PlayerObj.Add(_player);
        Camera cam = _player.gameObject.GetComponentInChildren<Camera>();

        Shared.CameraManager.CameraAdd(cam);
        Shared.BattelManager.AddDataToCharcterList(ObjectType.Player, _player);
    }
    public void PlayerbleDataLoad(out Dictionary<Player, GameObject> _value) 
    {
        _value = PlayerData;   
    }
    // Start is called before the first frame update

    public Player playerSearch(GameObject _playerObj, float _radius)
    {
        if (PlayerObj.Count == 0)
        {
            Debug.LogError($"MonsterList.Count ={PlayerObj.Count}");
            return null;
        }
        Player nearest = null;
        float minDist = _radius;// * _radius;

        for (int i = 0; i < PlayerObj.Count; i++)
        {
            Player monster = PlayerObj[i];

            if (monster.ConditionLoad())//DeadCheck
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


}
