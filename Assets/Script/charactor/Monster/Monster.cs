using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract partial class Monster : Charactor
{
    protected virtual void Start()
    {
        creatTabObj = Shared.BattelMgr.creatTab;//������Ʈ ���� ��(ex.�Ѿ�)
        mobRigid = GetComponent<Rigidbody>();
        mobColl = GetComponent<Collider>();
        boxColl = GetComponentInChildren<BoxCollider>();//��
        AI.init(this, SKILL);
        AI.Type(eType);
    }

}
