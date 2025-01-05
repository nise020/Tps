using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battel_UI : Actor
{
    UnityEngine.Camera cam;
    [SerializeField] Image mainCursur;
    [SerializeField] Button autoBut;

    [SerializeField] List <Button> playerBtn;


    [SerializeField] Image gameTimerBar;//진행도 바
    [SerializeField] Text minutesImg;//제한시간 글씨(분)
    [SerializeField] Text secondsImg;//제한시간 글씨(초)

    int minutesTimer = 3;
    float secondsTime = 60.0f;
    private void Start()
    {
        //gameTimerBar.fillAmount = 0.0f;
    }
    private void Update()
    {
        //Timer();
    }

    public void Onclick() 
    {
        //Shared.BattelMgr.PLAYER[]
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
