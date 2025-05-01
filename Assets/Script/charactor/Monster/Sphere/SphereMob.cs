using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SphereMob : Monster
{
    private void Awake()
    {
        monsterType = MonsterType.Sphere;
        RenderType = ObjectRenderType.Mesh;
    }
    protected override void Start()
    {
        base.Start();
        //cam = UnityEngine.Camera.main;
        //mobAnimator = GetComponent<Animator>();
        //creatTabObj = Shared.GameManager.creatTab;//¿ÀºêÁ§Æ® »ý¼º ÅÇ(ex.ÃÑ¾Ë)
        ////monsterRigid = GetComponent<Rigidbody>();
        ////monsterColl = GetComponent<Collider>();
        ////if (monsterColl == null)
        ////{
        ////    monsterColl = GetComponentInChildren<Collider>();
        ////}
        //AI.init(this, SKILL);
        //AI.Type(monsterType);

        ////FindMeshBodyObject();

        //STATUS.MonsterState(monsterType);
        //stateInIt();

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    //protected override void OnTriggerEnter(Collider other)
    //{
    //    base.OnTriggerEnter(other);
    //}
}
