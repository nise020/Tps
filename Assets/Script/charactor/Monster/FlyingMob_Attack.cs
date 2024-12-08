using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FlyingMob : Monster
{
    float moveTimer = 0.0f;
    float jumpStart = 0.0f;
    float jumpHight = 5.0f;
    float speed = 10.0f;
    bool targetCheack = false;
    [SerializeField] bool directAtkON = false;
    //protected override void targetOn(ref int _value)
    //{
    //    base.targetOn(ref _value);
    //}
    //public void DirectAttackSkill() //예외처리 필요
    //{
    //    if (directAtkON == false) { return; }
    //    //moveTimer += Time.deltaTime;
    //    if (targetCheack == false)
    //    {
    //        targetOn(ref number);
    //        if (playerObj[number] == null || coverObj[number] == null)
    //        {
    //            return;
    //        }
    //        targetCheack = true;
    //        Debug.Log($"{number}");
    //    }
    //    //Vector3 dir = (playerObj[number].transform.position - transform.position).normalized;
    //    Vector3 dir = (coverObj[number].transform.position - transform.position).normalized;
    //    transform.position += dir * speed * Time.deltaTime;
    //}
}
