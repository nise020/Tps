using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattelManager : MonoBehaviour
{
    [SerializeField, Tooltip("플레이어블")] public GameObject[] player;
    [SerializeField, Tooltip("몬스터")] GameObject[] Monster;
    [SerializeField, Tooltip("엄페물")] GameObject[] Concealment;
    private void Awake()
    {
        if (shared.BattelMgr == null)
        {
            shared.BattelMgr = this;
            //SceneMgr 싱글톤
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //chageScene(eScene.Title);
    }
    
    
}
