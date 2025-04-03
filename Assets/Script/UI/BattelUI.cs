
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
    PlayerjobEnum playerType;
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
        CharctorStateEnum charctorState = CharctorStateEnum.Player;
        playerType = PlayerjobEnum.Warrior;
        Warrior warrior = Shared.BattelManager.WARRIOR;
        Gunner gunner = Shared.BattelManager.GUNNER;

        if (warrior.playerEnumCheck(playerType) == true)
        {
            //PlayerControllState controllState = PlayerControllState.On;
            warrior.playerControllCheck(charctorState);
            PlayerCameraCheck(warrior, charctorState);

            playerType = PlayerjobEnum.Gunner;
            if (gunner.playerEnumCheck(playerType) == true)
            {
                charctorState = CharctorStateEnum.Npc;
                AnotherPlayerReset(gunner, playerType, charctorState);
            }
        }
    }
    public void CharactorControllButten2()//gunner
    {
        CharctorStateEnum charctorState = CharctorStateEnum.Player;
        playerType = PlayerjobEnum.Gunner;
        Warrior warrior = Shared.BattelManager.WARRIOR;
        Gunner gunner = Shared.BattelManager.GUNNER;

        if (gunner.playerEnumCheck(playerType) == true)
        {
            //PlayerControllState controllState = PlayerControllState.On;
            gunner.playerControllCheck(charctorState);
            PlayerCameraCheck(gunner, charctorState);

            playerType = PlayerjobEnum.Warrior;
            if (warrior.playerEnumCheck(playerType) == true) 
            {
                charctorState = CharctorStateEnum.Npc;
                AnotherPlayerReset(warrior, playerType, charctorState);
            }
        }
    }
    public void AnotherPlayerReset(Player _player, PlayerjobEnum _type, CharctorStateEnum _check) 
    {
        //_player.playerTypeInite(out _type);//load
        _player.ClearAllAnimation(_type);//Animation reset
        PlayerCameraCheck(_player, _check);//Camera On_Off
        _player.playerControllCheck(_check);//Controll Off
    }
    UnityEngine.Camera cam;
    [SerializeField] Image mainCursur;
    [SerializeField] Button autoBut;

    [SerializeField] List <Button> playerChangeBtn;

    public Image amiCursur;
    RectTransform CursurRect;

    [SerializeField] Image gameTimerBar;//���൵ ��
    [SerializeField] Text minutesImg;//���ѽð� �۾�(��)
    [SerializeField] Text secondsImg;//���ѽð� �۾�(��)

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
            //SceneMgr �̱���
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //stack ���� �ʿ�
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
