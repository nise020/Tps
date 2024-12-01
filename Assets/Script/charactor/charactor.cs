using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Charactor : Actor
{
    [Tooltip("몬스터가 공격할 대상을 총알,투척물이 받아서 사용")] 
    public Transform chaTargetTrs { get; protected set; }
    public Vector3 chaTargetPos { get; protected set; }//임시 플레이어용
    public Collider chaTargetColl { get; protected set; }
    //protected abstract void nomalAttack();//순수 가상클래스
    //자식이 무조건 만들어야 하는 기능

    
    protected virtual void OnTriggerEnter(Collider other)
    {
        Collider myColl = gameObject.GetComponent<Collider>();
        if (myColl.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player") ||
                 other.gameObject.layer == LayerMask.NameToLayer("Cover"))
            {
                Destroy(myColl.gameObject);
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
            {

            }
            else { return; }
        }
        else if (myColl.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            //Destroy(myColl.gameObject);
        }
        else if (myColl.gameObject.layer == LayerMask.NameToLayer("Bullet")) 
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("BackGround"))
            {
                Destroy(myColl.gameObject);
            }
        }
    }


    /// <summary>
    /// 일반 공격
    /// </summary>
    //protected virtual void nomalAttack() { }
    //자식들이 사용 할수도 안할수도 있는 기능
    
}
