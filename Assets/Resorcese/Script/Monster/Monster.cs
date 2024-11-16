using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Monster : Actor
{
    //[SerializeField] protected GameObject[] player;
    private void Start()
    {
        //StartCoroutine(MobAttackTimecheck());
        nomalAttack();
        //targetNumber();
    }

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    StartCoroutine(MobAttackTimecheck());
    //}
    void FixedUpdate()
    {
        MobAttackTimecheck();
        //StartCoroutine(MobAttackTimecheck());
    }
}
