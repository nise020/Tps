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
    Vector3 posiTion = Vector3.zero;

    public Vector3 offset;
    private Camera cam;
    private RectTransform rectTransform;
    public void inIt(Charactor charactor) 
    {
        Charactor = charactor;
        HpImage(Charactor);
    }
    public void HpImage(Charactor charactor) 
    {
        //charactor.
    }
    private void Awake()
    {
        cam = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        initHp();
    }
    private void Update()
    {
       // hpPosiTion();

        checkFillAmount();
        chasePlayer();
        chekedPlayerDestroy();

    }

    private void initHp()
    {
        imgHp.fillAmount = 1;
        imgEffect.fillAmount = 1;
        posiTion = Shared.BattelMgr.monsterData[key].transform.position;
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
    private void chasePlayer()
    {
        if (Shared.BattelMgr.GetMonsterPosition(key,out Vector3 pos) == true)
        {
            pos.y =+ 0.7f;
            rectTransform.position = pos;
        }
    }

    private void chekedPlayerDestroy()
    {
        if (imgEffect.fillAmount == 0.1f)
        {
            gameObject.SetActive(false);
        }
    }
}
