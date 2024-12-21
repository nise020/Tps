using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HugeMob : Monster
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
    //void Update()
    //{
    //    groundCheck = groundOn_Off(groundCheck);
    //}
    void FixedUpdate()
    {
        if (AI == null) { return; }
        AI.State(ref aIState);
    }
}
