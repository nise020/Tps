using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DefoltMob : Monster
{
    // Start is called before the first frame update
    protected override void Start()
    {
        eType = eMobType.Defolt;
        base.Start();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

}