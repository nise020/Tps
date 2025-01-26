using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract partial class Monster : Charactor
{
    protected virtual void Start()
    {
        //startPos = gameObject.transform.position;
        mobanimator = GetComponent<Animator>();
        NowHp();
        creatTabObj = Shared.BattelMgr.creatTab;//¿ÀºêÁ§Æ® »ý¼º ÅÇ(ex.ÃÑ¾Ë)
        mobRigid = GetComponent<Rigidbody>();
        mobColl = GetComponent<Collider>();
        boxColl = GetComponentInChildren<BoxCollider>();//¹ß
        AI.init(this, SKILL);
        AI.Type(eType);
    }
    private void FixedUpdate()
    {
        if (AI == null) { return; }
        AI.State();
        CheckHp();
    }
}
