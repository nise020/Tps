using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SphereMob : Monster
{
    protected override void Start()
    {
        eType = eMobType.Huge;
        base.Start();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
