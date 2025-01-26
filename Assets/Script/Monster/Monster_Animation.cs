using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : Charactor
{
    string idleAnim = ($"{MobAnim.Idle}");
    string walkAnim = ($"{MobAnim.Walk}");
    string attackAnim = ($"{MobAnim.Attack}");
    string serchAnim = ($"{MobAnim.Serch}");
    string dilrayAnim = ($"{MobAnim.AttackDilray}");

    Animator animator;

    public void PointMove(string _value)
    {
        Debug.Log($"PointMove");
        if (_value == "test")
        {
            mobanimator.SetInteger(walkAnim, 1);
        }
        else
        {
            mobanimator.SetInteger(walkAnim, 0);
        }
    }
}
