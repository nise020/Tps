using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Monster : Charactor
{
    protected AiMonster AI;
    protected virtual void Start()
    {
        mobRigid = GetComponent<Rigidbody>();
        mobColl = GetComponent<Collider>();
        boxColl = GetComponentInChildren<BoxCollider>();//¹ß
        AI = new AiMonster(this, eType);
        //StartCoroutine(MobAttackTimecheck());
        //nomalAttack();
        //targetNumber();
    }
    void FixedUpdate()
    {
        if (AI == null) { return; }
        AI.state();

        
    }

    //void Update()
    //{
    //    if (Ai=null) { return; }
    //    //Ai.state();
    //    //MobAttackTimecheck();
    //    //StartCoroutine(MobAttackTimecheck());
    //}

}
