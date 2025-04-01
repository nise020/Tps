using Photon.Pun.Demo.SlotRacer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class BattelUI : MonoBehaviour
{
    PlayerEnum playerType;
    PlayerControll playerControll;
    public void CharactorControlButten1()//warrior 
    {
        GameObject go1 = Shared.BattelManager.WARRIOR.gameObject;
        Player player = go1.GetComponent<Player>();
        player.playerTypInite(playerType);
        if (playerType == PlayerEnum.Warrior)
        {
            playerControll = PlayerControll.On;
            player.playerOnOff(playerControll);
        }
    }
    public void CharactorControlButten2()//gunner
    {
        GameObject go = Shared.BattelManager.GUNNER.gameObject;
        Player player = go.GetComponent<Player>();
        player.playerTypInite(playerType);
        if (playerType == PlayerEnum.Gunner)
        {
            playerControll = PlayerControll.On;

            player.playerOnOff(playerControll);
        }
    }

    UnityEngine.Camera cam;
    [SerializeField] Image mainCursur;
    [SerializeField] Button autoBut;

    [SerializeField] List <Button> playerBtn;

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
