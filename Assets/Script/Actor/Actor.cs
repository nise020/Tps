using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Actor : MonoBehaviour
{
    //오브젝트

    protected virtual void OnTriggerEnter(Collider other)//세분화 필요
    {
        Collider myColl = gameObject.GetComponent<Collider>();
        if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Monster))//몬스터일 경우
        {
            if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Player) ||
                other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Cover))
            {
                Destroy(myColl.gameObject);
            }
            else if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Bullet))
            {

            }
        }
        else if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Player)) //플레이어 일 경우
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

}
