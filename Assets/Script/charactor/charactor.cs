using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Charactor : Actor
{
    [Tooltip("몬스터가 공격할 대상을 총알,투척물이 받아서 사용")] 

    //protected abstract void nomalAttack();//순수 가상클래스
    //자식이 무조건 만들어야 하는 기능

    protected virtual void OnTriggerEnter(Collider other)//추가 필요
    {
        Collider myColl = gameObject.GetComponent<Collider>();
        if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Monster))//몬스터일 경우
        {
            if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Soljer) ||
                other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Cover))
            {
                Destroy(myColl.gameObject);
            }
            else if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Bullet))
            {

            }
        }
        else if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Soljer)) //플레이어 일 경우
        {
            //Destroy(myColl.gameObject);
        }
        else if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Bullet)) //플레이어 총알 일경우
        {
            if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.BackGround) ||
                other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Monster))
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
