using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FlyingMob : Monster
{
    // Start is called before the first frame update
    protected override void Start()
    {
        eType = eMobType.Flying;
        base.Start();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

}
