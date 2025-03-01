using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public partial class HpBar : MonoBehaviour 
{
    public int key = 0;
    Charactor Charactor;
    int hpValue = 0;

    [SerializeField] Image imgHp;
    [SerializeField] Image imgEffect;

    [SerializeField, Range(0.1f, 10f)] float effectTime = 1;//0은 나눌수 없으니 주의
    //Vector3 posiTion = new Vector3(0,0.5f,0);

    public Vector3 offset;
    private Camera mainCam;
    private RectTransform rectTransform;
    Canvas canvas;
    public void inIt(Charactor charactor) 
    {
        Charactor = charactor;
        HpImage(Charactor);
    }
    public void HpImage(Charactor charactor) 
    {
        //charactor.
    }

    private void Start()
    {
        initHp();
        mainCam = Camera.main;
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }
    private void LateUpdate()
    {
        checkFillAmount();
        chasePlayer();
        chekedPlayerDestroy();
    }

    private void initHp()
    {
        imgHp.fillAmount = 1;
        imgEffect.fillAmount = 1;
        //posiTion = Shared.BattelMgr.monsterData[key].transform.position;
    }

    private void checkFillAmount()
    {
        if (imgHp.fillAmount == imgEffect.fillAmount)
        {
            return;
        }
        if (imgHp.fillAmount < imgEffect.fillAmount)
        {
            imgEffect.fillAmount -= (Time.deltaTime / effectTime);
            if (imgHp.fillAmount > imgEffect.fillAmount)
            {
                imgEffect.fillAmount = imgHp.fillAmount;
            }
        }
        else if (imgHp.fillAmount > imgEffect.fillAmount)
        {
            imgEffect.fillAmount = imgHp.fillAmount;
        }
    }
    public void SetHp(float _maxHp, float _curHp)//0~1
    {
        imgHp.fillAmount = _curHp / _maxHp;
    }

    private void chasePlayer()
    {
        if (Shared.BattelMgr.GetMonsterPosition(key, out Vector3 pos) == true)
        {
            //Vector3 screenPosition = mainCam.WorldToScreenPoint(pos + new Vector3(0, 2.0f, 0));
            //rectTransform.anchoredPosition = screenPosition;
            transform.LookAt(transform.position + mainCam.transform.forward);






            //Vector3 screenPosition = mainCam.WorldToScreenPoint(pos + new Vector3(0, 2.0f, 0));
            //rectTransform.anchoredPosition = screenPosition;
            //rectTransform.LookAt(rectTransform.position + mainCam.transform.rotation * Vector3.forward,
            //                mainCam.transform.rotation * Vector3.up);
        }
        else { return; }
    }

    private void chekedPlayerDestroy()
    {
        if (imgEffect.fillAmount == 0.1f)
        {
            gameObject.SetActive(false);
        }
    }
}
