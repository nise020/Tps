using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Monster : Charactor
{
    //[SerializeField] protected GameObject[] player;
    protected void Start()
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
