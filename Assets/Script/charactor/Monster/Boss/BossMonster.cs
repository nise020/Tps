using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Monster
{
    protected override void Awake()
    {
        base.Awake();
        monsterStateData.MonsterType = MonsterType.Boss;
        RenderType = ObjectRenderType.Mesh;
        id = 102;
    }
    protected override void Start()
    {
        base.Start();
        RootTransform = transform.Find(ModelName.Model.ToString());
        FindRootBodyObject();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
