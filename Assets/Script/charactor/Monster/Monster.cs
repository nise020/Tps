using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Monster : Charactor
{
    AiMonster Ai = new AiMonster();
    protected virtual void Start()
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
        if (Ai=null) { return; }
        //Ai.state();
        //MobAttackTimecheck();
        //StartCoroutine(MobAttackTimecheck());
    }
    private void Update()
    {
        
    }
}
