using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //SceneMgr 기능별로 나눌수 있다


    public static GameManager Instanse;
    [Header("플블캐릭터")]
    [SerializeField] GameObject[] PlayerObj;//플블 번호
    [SerializeField] Button[] PlayerButtons;//현재 조작 중인 플블

    [Header("몬스터")]
    [SerializeField] GameObject[] MobObj;

    [Header("조준점")]
    [SerializeField] GameObject AimObj;//명중 오브젝트
    [SerializeField] TMP_Text BulletText;//총알 잔탄수
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
    void cursurAtteck()//수동공격 
    {
        cCursur cCursur = AimObj.GetComponent<cCursur>();
        Bulletcount(cCursur, PlayerObj[0]);//0은 임시
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
