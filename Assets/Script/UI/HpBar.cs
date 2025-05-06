using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;
using static Unity.VisualScripting.Metadata;

public partial class HpBar : MonoBehaviour 
{
    public int key = 0;
    Charactor Charactor;
    Charactor PLAYERCHARACTOR;
    Camera playerCamera;
    int hpValue = 0;

    [SerializeField] GameObject FabHpBarObj;
    [SerializeField] GameObject DamageBarObj;
    [SerializeField] Image imgHp;
    [SerializeField] Image imgEffect;

    [SerializeField, Range(0.1f, 10f)] float effectTime = 1;
    //Vector3 posiTion = new Vector3(0,0.5f,0);

    public Vector3 offset;
    private UnityEngine.Camera mainCam;
    private RectTransform rectTransform;
    Canvas canvas;

    public Transform Place_1;
    public Transform Place_10;
    public Transform Place_100;
    public Transform Place_1000;
    //public Transform parentObject;
    private List<GameObject> numberImages_1 = new List<GameObject>();
    private List<GameObject> numberImages_10 = new List<GameObject>();
    private List<GameObject> numberImages_100 = new List<GameObject>();
    private List<GameObject> numberImages_1000 = new List<GameObject>();
    private void Awake()
    {
        Place_1 = DamageBarObj.transform.Find("1");
        Place_10 = DamageBarObj.transform.Find("10");
        Place_100 = DamageBarObj.transform.Find("100");
        Place_1000 = DamageBarObj.transform.Find("1000");
    }
    private void Start()
    {
        initHp();
        PlayerUpdate();

        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

       numberImages_1 = DamageTransformLoad(Place_1);
       numberImages_10 = DamageTransformLoad(Place_10);
       numberImages_100 = DamageTransformLoad(Place_100);
       numberImages_1000 = DamageTransformLoad(Place_100);

        numberImages_1 = Shared.AtlasManager.AtlasLoad(numberImages_1, AtlasType.Damage);
        numberImages_10 = Shared.AtlasManager.AtlasLoad(numberImages_10, AtlasType.Damage);
        numberImages_100 = Shared.AtlasManager.AtlasLoad(numberImages_100, AtlasType.Damage);
        numberImages_1000 = Shared.AtlasManager.AtlasLoad(numberImages_1000, AtlasType.Damage);
        
    }

    private List<GameObject> DamageTransformLoad(Transform _transform)
    {
        List<Transform> children = new List<Transform>();

        foreach (Transform child in _transform)
        {
            children.Add(child);
        }

        List<GameObject> result = children
            .OrderBy(child => int.Parse(child.name)) // 이름을 int로 변환 후 정렬
            .Select(child => child.gameObject)
            .ToList();

        foreach (var img in result)
        {
            Debug.Log("추가된 이미지: " + img.name);
        }

        return result;
    }
    public void inIt(Charactor charactor) 
    {
        Charactor = charactor;
        HpImage(Charactor);
    }
    public void HpImage(Charactor charactor) 
    {
        //charactor.
    }

    //private void FindHpUi()
    //{
    //    Image[] images = GetComponentsInChildren<Image>();
    //    foreach (Image image in images) 
    //    {
    //        int Layer = LayerMask.NameToLayer(LayerName.);
    //    }
    //}

    private void LateUpdate()
    {
        cameraInMonsterCheck();
        chasePlayer();
        checkFillAmount();
        chekedPlayerDestroy();
    }

    private void initHp()
    {
        imgHp.fillAmount = 1;
        imgEffect.fillAmount = 1;
        //posiTion = Shared.BattelMgr.monsterData[key].transform.position;
    }
    public void PlayerUpdate() 
    {
        PLAYERCHARACTOR = Shared.GameManager.PlayerLoad();
        playerCamera = PLAYERCHARACTOR.GetComponentInChildren<Camera>();
        mainCam = playerCamera;
    }

    protected void cameraInMonsterCheck()
    {
        Vector3 viewportPos = playerCamera.WorldToViewportPoint(gameObject.transform.position);

        bool isVisible = (viewportPos.z > 0 && viewportPos.x > 0 &&
                          viewportPos.x < 1 && viewportPos.y > 0 &&
                          viewportPos.y < 1);
        if (isVisible)
        {
            //hbBarCheck(true);
            FabHpBarObj.SetActive(true);
        }
        else
        {
            FabHpBarObj.SetActive(false);
            //hbBarCheck(false);
        }
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
    public void DamageImage(float _value)//0~1
    {
        
    }
    private void chasePlayer()
    {
        transform.LookAt(transform.position + mainCam.transform.forward);
        //if (Shared.MonsterManager.GetMonsterPosition(key, out Vector3 pos) == true)
        //{
        //    transform.LookAt(transform.position + mainCam.transform.forward);
        //}
        //else { return; }
    }

    private void chekedPlayerDestroy()
    {
        if (imgEffect.fillAmount == 0.1f)
        {
            gameObject.SetActive(false);
        }
    }
}
