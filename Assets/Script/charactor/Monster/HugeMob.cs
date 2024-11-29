using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HugeMob : Monster
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    void FixedUpdate()
    {
        groundOn_Off(ref groundCheck);
        //jumpAttack();
        //float gravity = Mathf.Abs(Physics.gravity.y);
        //moving();
        //StartCoroutine(MobAttackTimecheck());
    }
}
