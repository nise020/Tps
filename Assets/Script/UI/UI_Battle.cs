
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public partial class UI_Battle : UiBase
{
    UnityEngine.Camera cam;
    [SerializeField] Image mainCursur;
    [SerializeField] Button autoBut;

    [SerializeField] List<Button> playerChangeBtn;

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

    List<Item> items = new List<Item>();

    [SerializeField] PlayerChangeButten player_Change_Butten1 = new PlayerChangeButten();
    [SerializeField] PlayerChangeButten player_Change_Butten2 = new PlayerChangeButten();

    private void Awake()
    {
        uiType = UiType.Menu;
    }

    protected override void Start()
    {
        base.Start();
        ButtenInit();
        CursurRect = GetComponent<RectTransform>();
    }
    //private void OnDestroy()
    //{
    //}

    
    private void Update()
    {
        while (Shared.InputManager.UiKeyinPutQueData.Count > 0)//key
        {
            KeyCode type = Shared.InputManager.UiKeyinPutQueData.Dequeue();
            switch (type)
            {
                case KeyCode.Alpha1:
                    player_Change_Butten1.CharactorControllButten();
                    break;
                case KeyCode.Alpha2:
                    player_Change_Butten2.CharactorControllButten();
                    break;
            }
        }
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
