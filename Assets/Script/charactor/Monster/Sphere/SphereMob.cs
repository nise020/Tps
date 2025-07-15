using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SphereMob : Monster
{
    protected override void Awake()
    {
        base.Awake();
        monsterStateData.MonsterType = MonsterType.Sphere;
        RenderType = ObjectRenderType.Mesh;
        Id = 102;
    }
    protected override void Start()
    {
        base.Start();
        //RootTransform = transform.Find(ModelName.Model.ToString());
        Compomentinit();
        FindRootBodyObject();

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

}
