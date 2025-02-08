using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract partial class Monster : Charactor
{
    protected override void Start()//Actor에 이동
    {
        //startPos = gameObject.transform.position;
        mobAnimator = GetComponent<Animator>();
        NowHp();
        creatTabObj = Shared.BattelMgr.creatTab;//오브젝트 생성 탭(ex.총알)
        mobRigid = GetComponent<Rigidbody>();
        mobColl = GetComponent<Collider>();
        boxColl = GetComponentInChildren<BoxCollider>();//발
        AI.init(this, SKILL);
        AI.Type(eType);
    }
    private void FixedUpdate()
    {
        if (AI == null) { return; }
        AI.State(ref aIState);
        CheckHp();
    }
}
