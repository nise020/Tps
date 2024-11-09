using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattelManager : MonoBehaviour
{
    [SerializeField, Tooltip("플레이어블")] GameObject[] player;
    [SerializeField, Tooltip("몬스터")] GameObject[] Monster;
    [SerializeField, Tooltip("엄페물")] GameObject[] Concealment;
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
