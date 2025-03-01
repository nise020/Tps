using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Actor : MonoBehaviour
{
    protected Camera cam;
    //오브젝트
    //리소스 재사용시 start문 사용 해야 하느가?
    protected virtual void Start()//Actor에 이동                              
    {
        //cam = gameObject.Find("MainCamera").GetComponent<Camera>;
        cam = Camera.main;
    }

    protected virtual void OnTriggerEnter(Collider other)//세분화 필요
    {
        Collider myColl = gameObject.GetComponent<Collider>();
        if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Monster))//몬스터일 경우
        {
            if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Player))
            {
                //Destroy(myColl.gameObject);
                gameObject.SetActive(false);
            }
            else if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Bullet))
            {

            }
        }
        else if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Player))//플레이어 일 경우
        {
            if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Monster))
            {
                //Destroy(myColl.gameObject);
                gameObject.SetActive(false);
            }
        }
        else if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Bullet))//플레이어 총알 일경우
        {
            if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Monster))
            {
                //Destroy(myColl.gameObject);
                gameObject.SetActive(false);
            }
        }
        else if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.MobGranid))//몬스터 총알 일경우
        {
            if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Player))
            {
                gameObject.SetActive(false);
            }
        }

    }

}
