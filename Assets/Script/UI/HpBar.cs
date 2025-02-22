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

    [SerializeField, Range(0.1f, 10f)] float effectTime = 1;//0�� ������ ������ ����
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
            // 3D ���� ��ǥ�� 2D ����Ʈ ��ǥ�� ��ȯ
            Vector3 viewportPos = cam.WorldToViewportPoint(posiTion + offset);

            // ī�޶� ���͸� ���� �ִ��� Ȯ��
            bool isVisible = (viewportPos.z > 0 && viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1);

            // ȭ�� �ȿ� ������ ü�¹� Ȱ��ȭ, ȭ�� ���̸� ��Ȱ��ȭ
            rectTransform.gameObject.SetActive(isVisible);

            if (isVisible)
            {
                // UI ��ġ ������Ʈ
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
    private void chasePlayer()//��ġ ����
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
