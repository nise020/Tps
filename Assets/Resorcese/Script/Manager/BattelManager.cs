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
        if (shared.BattelMgr == null)
        {
            shared.BattelMgr = this;
            //SceneMgr �̱���
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //chageScene(eScene.Title);
    }
    //public Vector3 targetNumber(Vector3 _pos)
    //{
    //    targetNumber(_pos);
    //    return _pos; //number = player[targetNum].transform.position;
    //}

}
