using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Actor : MonoBehaviour
{
    //������Ʈ

    protected virtual void OnTriggerEnter(Collider other)//����ȭ �ʿ�
    {
        Collider myColl = gameObject.GetComponent<Collider>();
        if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Monster))//������ ���
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
        else if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Player)) //�÷��̾� �� ���
        {
            //Destroy(myColl.gameObject);
        }
        else if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Bullet)) //�÷��̾� �Ѿ� �ϰ��
        {
            if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.BackGround) ||
                other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Monster))
            {
                Destroy(myColl.gameObject);
            }
        }

    }

}
