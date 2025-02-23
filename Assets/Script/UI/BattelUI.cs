using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class BattelUI : MonoBehaviour
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

    [SerializeField] GameObject hpBar;
    [SerializeField] GameObject creatTab;
    public Dictionary<int, GameObject> hpData = new Dictionary<int, GameObject>();

    int minutesTimer = 3;
    float secondsTime = 60.0f;

    private void Awake()
    {
        if (Shared.BattelUI == null)
        {
            Shared.BattelUI = this;
            //SceneMgr �̱���
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //stack ���� �ʿ�
    //Que
    public void CreatHpBar(GameObject _hpBarCanvers, int _max,Monster _monster) 
    {
        for(int i = 0; i < _max; i++) 
        {
            GameObject go = Instantiate(hpBar,transform.position,Quaternion.identity, _hpBarCanvers.transform);
            HpBar hp = go.GetComponent<HpBar>();
            hpData.Add(i, go);
            hp.key = i;
            hp.inIt(_monster);
          
        }
    }
    private void Start()
    {
        //gameTimerBar.fillAmount = 0.0f;
        cam = UnityEngine.Camera.main;
        CursurRect = GetComponent<RectTransform>();
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
