using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Monster : charactor
{
    //[SerializeField] protected GameObject[] player;
    private void Start()
    {
        //StartCoroutine(MobAttackTimecheck());
        //nomalAttack();
        //targetNumber();
    }


    void FixedUpdate()
    {
        MobAttackTimecheck();
        //StartCoroutine(MobAttackTimecheck());
    }
}
