
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public partial class UI_Battle : UiBase
{
    PlayerCamera MOVECAMERA;
    CharactorJobEnum playerType;
    //PlayerControll playerControll;

    private void Start()
    {
        CursurRect = GetComponent<RectTransform>();
    }


    private void Update()
    {
        while (Shared.InputManager.UiKeyinPutQueData.Count > 0)//key
        {
            KeyCode type = Shared.InputManager.UiKeyinPutQueData.Dequeue();
            switch (type)
            {
                case KeyCode.Alpha1:
                    CharactorControllButten1();
                    break;
                case KeyCode.Alpha2:
                    CharactorControllButten2();
                    break;
            }
        }
    }


    public void CharactorControllButten1()//warrior 
    {

        Player warrior = Shared.GameManager.PlayerDataLoad(CharactorJobEnum.Warrior);
        Player gunner = Shared.GameManager.PlayerDataLoad(CharactorJobEnum.Gunner);

        if (warrior.CharactorEnumCheck(CharactorJobEnum.Warrior) == true)
        {
            Shared.GameManager.CharctorContoll(warrior, CharctorStateEnum.Player);
            warrior.ClearAllAnimation(CharactorJobEnum.Warrior);
            PlayerCameraCheck(warrior, CharctorStateEnum.Player);

            if (gunner.CharactorEnumCheck(CharactorJobEnum.Gunner) == true)
            {
                AnotherPlayerReset(gunner, playerType, CharctorStateEnum.Npc);
            }
        }
    }
    public void CharactorControllButten2()//gunner
    {
        Player warrior = Shared.GameManager.PlayerDataLoad(CharactorJobEnum.Warrior);
        Player gunner = Shared.GameManager.PlayerDataLoad(CharactorJobEnum.Gunner);
        if (gunner.CharactorEnumCheck(CharactorJobEnum.Gunner) == true)
        {
            Shared.GameManager.CharctorContoll(gunner, CharctorStateEnum.Player);
            gunner.ClearAllAnimation(CharactorJobEnum.Gunner);
            PlayerCameraCheck(gunner, CharctorStateEnum.Player);

            if (warrior.CharactorEnumCheck(CharactorJobEnum.Warrior) == true)
            {
                AnotherPlayerReset(warrior, playerType, CharctorStateEnum.Npc);
            }
        }
    }
    public void PlayerCameraCheck(Player _player, CharctorStateEnum _check)
    {
        _player.init(out MOVECAMERA);
        Camera camera = MOVECAMERA.gameObject.GetComponent<Camera>();
        if (_check == CharctorStateEnum.Player)
        {
            MOVECAMERA.gameObject.SetActive(true);
            camera = Camera.main;
            Shared.CameraManager.CameraChange(camera);
            Shared.MonsterManager.PlayerCameraUpdate();
        }
        else //npc
        {
            MOVECAMERA.gameObject.SetActive(false);
        }
        //MOVECAMERA.gameObject.transform.SetParent(_player.transform);
    }
    public void AnotherPlayerReset(Player _player, CharactorJobEnum _type, CharctorStateEnum _check)
    {
        //_player.playerTypeInite(out _type);//Load
        _player.ClearAllAnimation(_type);//Animation reset
        PlayerCameraCheck(_player, _check);//Camera On_Off
        Shared.GameManager.CharctorContoll(_player, CharctorStateEnum.Npc);//Controll Off
    }
    public void Timer()
    {
        secondsTime -= Time.deltaTime;

        string minits = minutesTimer.ToString();
        string seconds = ((int)secondsTime).ToString();

        minutesImg.text = minits;
        secondsImg.text = seconds;
    }

   

    public void AutoBuutten()//ing~
    {
        Shared.GameManager.PlayerbleDataLoad(out Dictionary<int, Player> _value);
    }
    UnityEngine.Camera cam;
    [SerializeField] Image mainCursur;
    [SerializeField] Button autoBut;

    [SerializeField] List <Button> playerChangeBtn;

    public Image amiCursur;
    RectTransform CursurRect;

    [SerializeField] Image gameTimerBar;//ÁøÇàµµ ¹Ù
    [SerializeField] Text minutesImg;//Á¦ÇÑ½Ã°£ ±Û¾¾(ºÐ)
    [SerializeField] Text secondsImg;//Á¦ÇÑ½Ã°£ ±Û¾¾(ÃÊ)

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
            //SceneMgr ½Ì±ÛÅæ
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
}
