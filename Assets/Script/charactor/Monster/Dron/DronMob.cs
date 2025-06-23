using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DronMob : Monster
{
    protected override void Awake()
    {
        base.Awake();
        monsterStateData.MonsterType = MonsterType.Dron;
        RenderType = ObjectRenderType.Mesh;
        CharacterTabelData[CharacterTabelType.Id] = 103;
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

}
