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
        initHp();
    }
    private void Update()
    {
        hpPosiTion();

        checkFillAmount();
        chasePlayer();
        chekedPlayerDestroy();

    }

    private void hpPosiTion()
    {

        if (posiTion != null)
        {
            // 3D 월드 좌표를 2D 뷰포트 좌표로 변환
            Vector3 viewportPos = cam.WorldToViewportPoint(posiTion + offset);

            // 카메라가 몬스터를 보고 있는지 확인
            bool isVisible = (viewportPos.z > 0 && viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1);

            // 화면 안에 있으면 체력바 활성화, 화면 밖이면 비활성화
            rectTransform.gameObject.SetActive(isVisible);

            if (isVisible)
            {
                // UI 위치 업데이트
                rectTransform.anchorMin = viewportPos;
                rectTransform.anchorMax = viewportPos;
            }
        }
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
    private void chasePlayer()//위치 고정
    {
        if (Shared.BattelMgr.GetPlayerPosition(out Vector3 pos) == true)
        {
            pos.y -= 0.7f;
            transform.position = pos;// = pos - new Vector3(0,0.7f,0);
        }
    }

    private void chekedPlayerDestroy()
    {
        if (imgEffect.fillAmount == 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
