using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class charactor : Actor
{
    [Tooltip("몬스터가 공격할 대상을 총알,투척물이 받아서 사용")] 
    public Transform ActTargetTrs { get; protected set; }
    //protected abstract void nomalAttack();//순수 가상클래스
    //자식이 무조건 만들어야 하는 기능


    /// <summary>
    /// 일반 공격
    /// </summary>
    protected virtual void nomalAttack() { }
    //자식들이 사용 할수도 안할수도 있는 기능
    private void OnTriggerEnter(Collider other)
    {
        other = gameObject.GetComponent<Collider>();
    }
}
