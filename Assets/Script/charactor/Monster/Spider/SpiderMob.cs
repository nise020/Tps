using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SpiderMob : Monster
{
    protected override void Awake()
    {
        base.Awake();
        Id   = 101;
        monsterStateData.MonsterType = MonsterType.Spider;
        RenderType = ObjectRenderType.Skin;
    }
    protected override void Start()
    {
        base.Start();
        Compomentinit();
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
