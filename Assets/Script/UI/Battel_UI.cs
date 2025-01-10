using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battel_UI : MonoBehaviour
{
    UnityEngine.Camera cam;
    [SerializeField] Image mainCursur;
    [SerializeField] Button autoBut;

    [SerializeField] List <Button> playerBtn;

    public Image amiCursur;
    RectTransform CursurRect;

    [SerializeField] Image gameTimerBar;//���൵ ��
    [SerializeField] Text minutesImg;//���ѽð� �۾�(��)
    [SerializeField] Text secondsImg;//���ѽð� �۾�(��)

    int minutesTimer = 3;
    float secondsTime = 60.0f;
    private void Start()
    {
        //gameTimerBar.fillAmount = 0.0f;
        cam = UnityEngine.Camera.main;
        CursurRect = GetComponent<RectTransform>();
    }
    private void Update()
    {
        //Timer();
    }
    private void LateUpdate()
    {

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
