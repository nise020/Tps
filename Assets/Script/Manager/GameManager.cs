using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //SceneMgr ��ɺ��� ������ �ִ�


    public static GameManager Instanse;
    [Header("�ú�ĳ����")]
    [SerializeField] GameObject[] PlayerObj;//�ú� ��ȣ
    [SerializeField] Button[] PlayerButtons;//���� ���� ���� �ú�

    [Header("����")]
    [SerializeField] GameObject[] MobObj;

    [Header("������")]
    [SerializeField] GameObject AimObj;//���� ������Ʈ
    [SerializeField] TMP_Text BulletText;//�Ѿ� ��ź��
    int Playerbullet;
    public void onbtnTitle() 
    {
        SceneManager.LoadScene("Login");
        //shared.SceneMgr.chageSecen
    }

    private void Awake()
    {
        //shared.GameManager = this;
        //if (Instanse == null) 
        //{
        //    Instanse = this;
        //}
        //else 
        //{
        //    Destroy(gameObject);
        //    return;
        //}
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int iNum = 0; iNum < PlayerButtons.Length; iNum++) 
        {
            PlayerButtons[iNum].onClick.AddListener(cursureSoljer);
        }
        
        
    }
    void cursureSoljer() 
    {
        if(PlayerObj.Length > 0) 
        {

        }
    }
    void cursurAtteck()//�������� 
    {
        cCursur cCursur = AimObj.GetComponent<cCursur>();
        Bulletcount(cCursur, PlayerObj[0]);//0�� �ӽ�
    }
    void Bulletcount(cCursur value, GameObject PlayerObj) 
    {
        cSoljer Soljer = PlayerObj.GetComponent<cSoljer>();
        if (value.shoot == true)
        {
            Soljer.bullet -= 1;
            value.shoot = false;
            Playerbullet = Soljer.bullet;
            if (Playerbullet <= 0)
            {
                Playerbullet = 0;
            }
            BulletText.text = Playerbullet.ToString();
        
        }
    }
    void Update()
    {
        for (int iNum = 0; iNum < PlayerButtons.Length; iNum++) 
        {
            
        }
        cursurAtteck();

    }
}
