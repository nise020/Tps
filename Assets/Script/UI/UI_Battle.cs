
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

    private void Start()
    {
        CursurRect = GetComponent<RectTransform>();
        GameEvents.OnExitRange += AddPrompt;
        GameEvents.OnEnterRange += RemovePrompt;
    }
    private void OnDestroy()
    {
        GameEvents.OnEnterRange -= AddPrompt;
        GameEvents.OnExitRange -= RemovePrompt;
    }

    void AddPrompt(Item item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
            CreatePromptUI(item); // 실제 안내 메시지 생성
        }
    }

    void RemovePrompt(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            RemovePromptUI(item); // 해당 메시지 제거
        }
    }

    void CreatePromptUI(Item item)
    {
        // item.item.name, item.item.icon 등 table에서 불러와 UI 생성
    }

    void RemovePromptUI(Item item)
    {
        // 해당 item에 대응되는 UI 오브젝트 제거
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







   
    public void Timer()
    {
        secondsTime -= Time.deltaTime;

        string minits = minutesTimer.ToString();
        string seconds = ((int)secondsTime).ToString();

        minutesImg.text = minits;
        secondsImg.text = seconds;
    }

   

    
    

   

    
}
