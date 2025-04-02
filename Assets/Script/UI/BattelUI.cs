
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public partial class BattelUI : MonoBehaviour
{
    MoveCamera MOVECAMERA;
    PlayerEnum playerType;
    PlayerControll playerControll;
    public void PlayerCameraCheck(Player _player, PlayerControll _check) 
    {
        _player.init(out MOVECAMERA);
        if (_check == PlayerControll.On)
        {
            MOVECAMERA.gameObject.SetActive(true);
        }
        else 
        {
            MOVECAMERA.gameObject.SetActive(false);
        }
        //MOVECAMERA.gameObject.transform.SetParent(_player.transform);
    }
    public void CharactorControllButten1()//warrior 
    {
        playerType = PlayerEnum.Warrior;
        Warrior warrior = Shared.BattelManager.WARRIOR;
        Gunner gunner = Shared.BattelManager.GUNNER;

        if (warrior.playerEnumCheck(playerType) == true)
        {
            playerControll = PlayerControll.On;
            warrior.playerControllCheck(playerControll);
            PlayerCameraCheck(warrior, playerControll);

            playerType = PlayerEnum.Gunner;
            if (gunner.playerEnumCheck(playerType) == true)
            {
                playerControll = PlayerControll.Off;
                AnotherPlayerReset(gunner, playerType, playerControll);
            }
        }
    }
    public void CharactorControllButten2()//gunner
    {
        playerType = PlayerEnum.Gunner;
        Warrior warrior = Shared.BattelManager.WARRIOR;
        Gunner gunner = Shared.BattelManager.GUNNER;

        if (gunner.playerEnumCheck(playerType) == true)
        {
            playerControll = PlayerControll.On;
            gunner.playerControllCheck(playerControll);
            PlayerCameraCheck(gunner, playerControll);

            playerType = PlayerEnum.Warrior;
            if (warrior.playerEnumCheck(playerType) == true) 
            {
                playerControll = PlayerControll.Off;
                AnotherPlayerReset(warrior, playerType, playerControll);
            }
        }
    }
    public void AnotherPlayerReset(Player _player, PlayerEnum _type, PlayerControll _check) 
    {
        _player.playerTypeInite(out _type);//load
        _player.ClearAllAnimation(_type);
        playerControll = PlayerControll.Off;
        PlayerCameraCheck(_player, _check);
        _player.playerControllCheck(_check);
    }
    UnityEngine.Camera cam;
    [SerializeField] Image mainCursur;
    [SerializeField] Button autoBut;

    [SerializeField] List <Button> playerChangeBtn;

    public Image amiCursur;
    RectTransform CursurRect;

    [SerializeField] Image gameTimerBar;//진행도 바
    [SerializeField] Text minutesImg;//제한시간 글씨(분)
    [SerializeField] Text secondsImg;//제한시간 글씨(초)

    [SerializeField] GameObject hpBar;
    [SerializeField] GameObject creatTab;
    public Dictionary<int, GameObject> hpData = new Dictionary<int, GameObject>();

    int minutesTimer = 3;
    float secondsTime = 60.0f;

    private void Awake()
    {
        if (Shared.BattelUI == null)
        {
            Shared.BattelUI = this;
            //SceneMgr 싱글톤
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //stack 구현 필요
    //Que
    //public void CreatHpBar(GameObject _hpBarCanvers, int _max,Monster _monster) 
    //{
    //    for(int i = 0; i < _max; i++) 
    //    {
    //        GameObject go = Instantiate(hpBar,transform.position,Quaternion.identity, _hpBarCanvers.transform);
    //        HpBar hp = go.GetComponent<HpBar>();
    //        hpData.Add(i, go);
    //        hp.key = i;
    //        hp.inIt(_monster);
          
    //    }
    //}
    private void Start()
    {
        //gameTimerBar.fillAmount = 0.0f;
        cam = UnityEngine.Camera.main;
        CursurRect = GetComponent<RectTransform>();
    }

    public void Timer()
    {
        secondsTime -= Time.deltaTime;

        string minits = minutesTimer.ToString();
        string seconds = ((int)secondsTime).ToString();

        minutesImg.text = minits;
        secondsImg.text = seconds;
    }
}
