
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public partial class UI_Battel : MonoBehaviour
{
    MoveCamera MOVECAMERA;
    CharactorJobEnum playerType;
    //PlayerControll playerControll;
    public void PlayerCameraCheck(Player _player, CharctorStateEnum _check) 
    {
        _player.init(out MOVECAMERA);
        if (_check == CharctorStateEnum.Player)
        {
            MOVECAMERA.gameObject.SetActive(true);
        }
        else //npc
        {
            MOVECAMERA.gameObject.SetActive(false);
        }
        //MOVECAMERA.gameObject.transform.SetParent(_player.transform);
    }
    public void CharactorControllButten1()//warrior 
    {
        Warrior warrior = Shared.BattelManager.WARRIOR;
        Gunner gunner = Shared.BattelManager.GUNNER;
        if (warrior.CharactorEnumCheck(CharactorJobEnum.Warrior) == true)
        {
            Shared.GameManager.CharctorContoll(warrior, CharctorStateEnum.Player);
            PlayerCameraCheck(warrior, CharctorStateEnum.Player);

            if (gunner.CharactorEnumCheck(CharactorJobEnum.Gunner) == true)
            {
                AnotherPlayerReset(gunner, playerType, CharctorStateEnum.Npc);
            }
        }
    }
    public void CharactorControllButten2()//gunner
    {
        Warrior warrior = Shared.BattelManager.WARRIOR;
        Gunner gunner = Shared.BattelManager.GUNNER;
        if (gunner.CharactorEnumCheck(CharactorJobEnum.Gunner) == true)
        {
            Shared.GameManager.CharctorContoll(gunner, CharctorStateEnum.Player);
            PlayerCameraCheck(gunner, CharctorStateEnum.Player);

            if (warrior.CharactorEnumCheck(CharactorJobEnum.Warrior) == true) 
            {
                AnotherPlayerReset(warrior, playerType, CharctorStateEnum.Npc);
            }
        }
    }
    public void AutoBuutten()//ing~
    {
        Shared.GameManager.PlayerbleDataLoad(out Dictionary<Player, int> _value);
    }
    public void AnotherPlayerReset(Player _player, CharactorJobEnum _type, CharctorStateEnum _check) 
    {
        //_player.playerTypeInite(out _type);//Load
        _player.ClearAllAnimation(_type);//Animation reset
        PlayerCameraCheck(_player, _check);//Camera On_Off
        Shared.GameManager.CharctorContoll(_player, CharctorStateEnum.Npc);//Controll Off
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
