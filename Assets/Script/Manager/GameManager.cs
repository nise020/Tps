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
    Dictionary<Player,int> PlayerCount = new Dictionary<Player,int>();
    int playerKey = 0;

    [Header("Player Charactor")]
    Player PLAYER;
    [SerializeField] Gunner GUNNER;
    [SerializeField] Warrior WARRIOR;
    Player[] PlayerObj;//플블 번호



    [Header("Monster")]
    [SerializeField] GameObject[] MobObj;

 
    int Playerbullet;
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
    public void FindPlayer() 
    {
        int value = PlayerCount.Count;
        for (int i = 0; i < value; i++) 
        {
            //Player player = 

        }
    }

    public Vector3 PlayerPos(Vector3 _pos)
    {
        if (PLAYER == null)
        {
            return _pos;
            //FindPlayer();
        }
        _pos = PLAYER.gameObject.transform.position.normalized;
        _pos = new Vector3(_pos.x, 0.0f, _pos.z);
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
        _player.TypeInit(_type);
        PlayerCount.Add(_player, playerKey);
        playerKey += 1;
    }
    public void PlayerbleDataLoad(out Dictionary<Player, int> _value) 
    {
        _value = PlayerCount;   
    }
    // Start is called before the first frame update
    void Start()
    {
        CharctorTypeAdd(GUNNER, CharactorJobEnum.Gunner);
        CharctorTypeAdd(WARRIOR, CharactorJobEnum.Warrior);
        //FindPlayer();
    }

    
    
}
