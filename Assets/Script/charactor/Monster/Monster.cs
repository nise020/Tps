using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Monster : Charactor
{
    protected AiMonster AI = new AiMonster();
    protected Monster_Skill SKILL = new Monster_Skill();
    public eMobType eType;
    protected virtual void Start()
    {
        mobRigid = GetComponent<Rigidbody>();
        mobColl = GetComponent<Collider>();
        boxColl = GetComponentInChildren<BoxCollider>();//¹ß
        AI.init(this);
        AI.Type(eType);
        //AI = new AiMonster(this, eType);
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
