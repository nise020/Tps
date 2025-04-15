using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SpiderMob : Monster
{
    // Start is called before the first frame update
    private void Start()
    {
        cam = UnityEngine.Camera.main;
        monsterType = MonsterType.Spider;
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
