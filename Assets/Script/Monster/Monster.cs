using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract partial class Monster : Charactor
{
    protected override void Start()//Actor�� �̵�
    {
        //startPos = gameObject.transform.position;
        mobAnimator = GetComponent<Animator>();
        NowHp();
        creatTabObj = Shared.BattelMgr.creatTab;//������Ʈ ���� ��(ex.�Ѿ�)
        mobRigid = GetComponent<Rigidbody>();
        mobColl = GetComponent<Collider>();
        boxColl = GetComponentInChildren<BoxCollider>();//��
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
