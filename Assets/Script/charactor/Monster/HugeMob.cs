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
    void Update()
    {
        groundOn_Off(ref groundCheck);
        jumpSkill();
        base.nomalAttack();
        //float gravity = Mathf.Abs(Physics.gravity.y);
        //moving();
        //StartCoroutine(MobAttackTimecheck());
    }
}
