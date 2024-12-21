using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Charactor : Actor
{
    [Tooltip("���Ͱ� ������ ����� �Ѿ�,��ô���� �޾Ƽ� ���")] 

    //protected abstract void nomalAttack();//���� ����Ŭ����
    //�ڽ��� ������ ������ �ϴ� ���

    protected virtual void OnTriggerEnter(Collider other)//�߰� �ʿ�
    {
        Collider myColl = gameObject.GetComponent<Collider>();
        if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Monster))//������ ���
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
        else if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Soljer)) //�÷��̾� �� ���
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
    

    /// <summary>
    /// �Ϲ� ����
    /// </summary>
    //protected virtual void nomalAttack() { }
    //�ڽĵ��� ��� �Ҽ��� ���Ҽ��� �ִ� ���

}
