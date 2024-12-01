using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattelManager : MonoBehaviour
{
    [SerializeField, Tooltip("�÷��̾��")] public GameObject[] player;
    [SerializeField, Tooltip("����")] GameObject[] Monster;
    [SerializeField, Tooltip("���买")] GameObject[] Concealment;
    public int targetNum;
    Vector3 targetPos;
    private void Awake()
    {
        if (Shared.BattelMgr == null)
        {
            Shared.BattelMgr = this;
            //SceneMgr �̱���
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //chageScene(eScene.Title);
    }
    Charactor Factory(eMobType _e)//���丮 ���� 
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
