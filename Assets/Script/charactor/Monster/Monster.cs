using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Monster : charactor
{
    //[SerializeField] protected GameObject[] player;
    private void Start()
    {
        mobRigid = GetComponent<Rigidbody>();
        mobColl = GetComponent<Collider>();
        boxColl = GetComponentInChildren<BoxCollider>();//¹ß
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
