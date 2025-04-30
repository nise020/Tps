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
    private UnityEngine.Camera mainCam;
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
        mainCam = UnityEngine.Camera.main;
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

    private void chasePlayer()//Main Camera 수정
    {
        if (Shared.MonsterManager.GetMonsterPosition(key, out Vector3 pos) == true)
        {
            if (mainCam != Camera.main) 
            {
                mainCam = Shared.CameraManager.MainCameraLoad();
            }
            transform.LookAt(transform.position + mainCam.transform.forward);
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
