using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract partial class Monster : Charactor
{
    protected virtual void Start()
    {
        mobRigid = GetComponent<Rigidbody>();
        mobColl = GetComponent<Collider>();
        boxColl = GetComponentInChildren<BoxCollider>();//¹ß
        AI.init(this, SKILL);
        AI.Type(eType);
    }
    void FixedUpdate()
    {
        if (AI == null) { return; }
        AI.State(ref aIState);
    }

}
