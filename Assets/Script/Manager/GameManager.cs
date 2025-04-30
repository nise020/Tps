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
    Dictionary<int,Player> PlayerCount = new Dictionary<int, Player>();
    int playerKey = 0;

    [Header("Player Charactor")]
    Player PLAYER;
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
 
    public void onbtnTitle() 
    {
        //SceneManager.LoadScene("Login");
        //shared.SceneMgr.chageSecen
    }
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
    }
    void Start()
    {
        //GameObject go = Instantiate(creatTabObj);
        //creatTab = go.transform;

        CharctorTypeAdd(GUNNER, CharactorJobEnum.Gunner);
        CharctorTypeAdd(WARRIOR, CharactorJobEnum.Warrior);
        FindPlayer();
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
        foreach (KeyValuePair<int, Player> playerData in PlayerCount)
        {
            Player player = playerData.Value;
            player.PlayerControllChack(out CharctorStateEnum _type);
            if (_type == CharctorStateEnum.Player) 
            {
                PLAYER = playerData.Value;
                break;
            }
            //int count = playerData.Value;
        }
    }
    public Player PlayerLoad() 
    {
        return PLAYER;
    }
    public Player PlayerDataLoad(CharactorJobEnum _job)
    {
        if (_job ==CharactorJobEnum.Gunner) 
        {
            return GUNNER;
        }
        else if (_job == CharactorJobEnum.Warrior)
        {
            return WARRIOR;
        }
        return null;
    }
    public void PlayerData(out Player _player) 
    {
        if (PLAYER == null) 
        {
            _player = WARRIOR;//임시
            Shared.BattelUI.CharactorControllButten1();
            return;
        }
        _player = PLAYER;
    }
    public Vector3 PlayerPos(Vector3 _pos)
    {
        _pos = PLAYER.gameObject.transform.position;
        return _pos;
    }
    public void CharctorContoll(Player _player, CharctorStateEnum _state) 
    {
        _player.PlayerControllChange(_state);//On,Off
        if (_state == CharctorStateEnum.Player) 
        {
            PLAYER = _player;         
        }
    }
    private void CharctorTypeAdd(Player _player, CharactorJobEnum _type) 
    {
        _player.TypeInit(_type, playerKey);
        PlayerCount.Add(playerKey, _player);
        playerKey += 1;
        PlayerObj.Add(_player);

        Camera cam = _player.gameObject.GetComponentInChildren<Camera>();

        Shared.CameraManager.CameraAdd(cam);
    }
    public void PlayerbleDataLoad(out Dictionary<int, Player> _value) 
    {
        _value = PlayerCount;   
    }
    // Start is called before the first frame update
    

    
    
}
