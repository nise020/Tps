using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SphereMob : Monster
{
    private void Start()
    {
        cam = Camera.main;
        monsterType = MonsterType.Sphere;

        mobAnimator = GetComponent<Animator>();
        creatTabObj = Shared.BattelManager.creatTab;//¿ÀºêÁ§Æ® »ý¼º ÅÇ(ex.ÃÑ¾Ë)
        monsterRigid = GetComponent<Rigidbody>();
        monsterColl = GetComponent<Collider>();
        if (monsterColl == null)
        {
            monsterColl = GetComponentInChildren<Collider>();
        }
        AI.init(this, SKILL);
        AI.Type(monsterType);

        STATUS.MonsterState(monsterType);
        stateInIt();

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
