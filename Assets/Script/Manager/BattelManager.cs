using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattelManager : MonoBehaviour
{
    [SerializeField, Tooltip("�÷��̾��")] GameObject[] player;
    [SerializeField, Tooltip("����")] GameObject[] Monster;
    [SerializeField, Tooltip("���买")] GameObject[] Concealment;
    private void Awake()
    {
        shared.BattelMgr = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
