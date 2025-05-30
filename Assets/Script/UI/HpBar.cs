using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public partial class HpBar : MonoBehaviour 
{
    public int key = 0;
    Character CHARACTER;
    Character PLAYERCHARACTOR;
    Camera playerCamera;
    ObjectType ObjectType = ObjectType.None;
    int hpValue = 0;

    [SerializeField] GameObject FabHpBarObj;
    [SerializeField] GameObject DamageBarObj;
    [SerializeField] Image hpImg;
    [SerializeField] Image hpLateImg;

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

    public Action<int> AttackDamageEvent;

    private void Awake()
    {
        Place_1 = DamageBarObj.transform.Find("1");
        Place_10 = DamageBarObj.transform.Find("10");
        Place_100 = DamageBarObj.transform.Find("100");
        Place_1000 = DamageBarObj.transform.Find("1000");
    }
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        DamageSetting();
    }

    private void DamageSetting() 
    {
        numberImages_1 = DamageTransformLoad(Place_1);
        numberImages_10 = DamageTransformLoad(Place_10);
        numberImages_100 = DamageTransformLoad(Place_100);
        numberImages_1000 = DamageTransformLoad(Place_100);

        numberImages_1 = Shared.AtlasManager.AtlasLoad_List(numberImages_1, AtlasType.Damage);
        numberImages_10 = Shared.AtlasManager.AtlasLoad_List(numberImages_10, AtlasType.Damage);
        numberImages_100 = Shared.AtlasManager.AtlasLoad_List(numberImages_100, AtlasType.Damage);
        numberImages_1000 = Shared.AtlasManager.AtlasLoad_List(numberImages_1000, AtlasType.Damage);

        Place_1.gameObject.SetActive(false);
        Place_10.gameObject.SetActive(false);
        Place_100.gameObject.SetActive(false);
        Place_1000.gameObject.SetActive(false);
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

        //foreach (var img in result)
        //{
        //    Debug.Log("추가된 이미지: " + img.name);
        //}

        return result;
    }
    public void DamageImageActive(int _value) 
    {
        int value = _value;

        Place_1.gameObject.SetActive(true);

        int digit_1 = _value % 10;
        numberImages_1[digit_1].SetActive(true);

        List<GameObject> result = new List<GameObject>();

        result.Add(numberImages_1[digit_1].gameObject);
        result.Add(Place_1.gameObject);

        if (value >= 10)
        {
            Place_10.gameObject.SetActive(true);

            int digit = (_value / 10) % 10;
            numberImages_10[digit].SetActive(true);

            result.Add(numberImages_10[digit_1].gameObject);
            result.Add(Place_10.gameObject);
        }
        if (value >= 100) 
        {
            Place_100.gameObject.SetActive(true);

            int digit = (_value / 100) % 10;
            numberImages_100[digit].SetActive(true);

            result.Add(numberImages_100[digit_1].gameObject);
            result.Add(Place_100.gameObject);
        }
        if (value >= 1000) 
        {
            Place_1000.gameObject.SetActive(true);

            int digit = (_value / 1000) % 10;
            numberImages_1000[digit].SetActive(true);

            result.Add(numberImages_1000[digit_1].gameObject);
            result.Add(Place_1000.gameObject);
        }
        //Invoke("imageHide", 3.0f);

        StartCoroutine(imageHide(result, 1.0f));
    }
    IEnumerator imageHide(List<GameObject> _lists,float _timer) 
    {
        yield return new WaitForSeconds(_timer);
        foreach (GameObject _list in _lists) 
        {
            _list.SetActive(false);
        }
    }
    public void CharactorInIt(Character charactor) //캐릭터 타입 로드 필요
    {
        CHARACTER = charactor;
        ObjectType = CHARACTER.TypeInit();
        CHARACTER.onHpChanged += SetHp;
        AttackDamageEvent += DamageImageActive;

        init();
        if (ObjectType != ObjectType.Player)
        {
            PlayerUpdate();        
        }

        //CHARACTER.onHpChanged += OnHpChanged;
        //HpImage(CHARACTER);
    }
    public void HpImage(Character charactor) 
    {
        //charactor.
    }

    private void LateUpdate()
    {
        if (ObjectType == ObjectType.Monster) 
        {
            cameraInMonsterCheck();
            chasePlayer();
            //checkFillAmount();
        }
        chekedPlayerDestroy();
    }

    public void init()
    {
        hpImg.fillAmount = 1;
        hpLateImg.fillAmount = 1;
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
        if (hpImg.fillAmount == hpLateImg.fillAmount)
        {
            return;
        }
        if (hpImg.fillAmount < hpLateImg.fillAmount)
        {
            hpLateImg.fillAmount -= (Time.deltaTime / effectTime);
            if (hpImg.fillAmount > hpLateImg.fillAmount)
            {
                hpLateImg.fillAmount = hpImg.fillAmount;
            }
        }
        else if (hpImg.fillAmount > hpLateImg.fillAmount)
        {
            hpLateImg.fillAmount = hpImg.fillAmount;
        }
    }
    private IEnumerator animateEffectBar()
    {
        while (hpLateImg.fillAmount > hpImg.fillAmount)
        {
            hpLateImg.fillAmount -= Time.deltaTime / effectTime;
            if (hpLateImg.fillAmount < hpImg.fillAmount)
            {
                hpLateImg.fillAmount = hpImg.fillAmount;
            }
            yield return null;
        }
    }
    public void SetHp(float _maxHp, float _curHp)//0~1
    {
        if (gameObject.activeSelf) 
        {
            hpImg.fillAmount = _curHp / _maxHp;
            StartCoroutine(animateEffectBar());
        }
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
        if (hpLateImg.fillAmount == 0.1f)
        {
            gameObject.SetActive(false);
        }
    }
}
