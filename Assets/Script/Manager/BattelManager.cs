using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattelManager : MonoBehaviour
{
    [SerializeField, Tooltip("플레이어블")] public GameObject[] player;
    [SerializeField, Tooltip("몬스터")] GameObject[] Monster;
    [SerializeField, Tooltip("엄페물")] GameObject[] Concealment;
    public int targetNum;
    Vector3 targetPos;
    private void Awake()
    {
        if (Shared.BattelMgr == null)
        {
            Shared.BattelMgr = this;
            //SceneMgr 싱글톤
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //chageScene(eScene.Title);
    }
    Charactor Factory(eMobType _e)//팩토리 패턴 
    {
        Charactor c = null;
        switch (_e)
        {
            case eMobType.Flying:
                c = new FlyingMob();
                break;
            case eMobType.Huge:
                c = new HugeMob();
                break;
            case eMobType.Defolt:
                c = new DefoltMob();
                break;
        }
        return c;
    }

}
