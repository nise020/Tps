using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Actor : MonoBehaviour
{
    protected Camera cam;
    //������Ʈ
    //���ҽ� ����� start�� ��� �ؾ� �ϴ���?
    protected virtual void Start()//Actor�� �̵�                              
    {
        //cam = gameObject.Find("MainCamera").GetComponent<Camera>;
        cam = Camera.main;
    }

    protected virtual void OnTriggerEnter(Collider other)//����ȭ �ʿ�
    {
        Collider myColl = gameObject.GetComponent<Collider>();
        if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Monster))//������ ���
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
        else if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Player))//�÷��̾� �� ���
        {
            if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Monster))
            {
                //Destroy(myColl.gameObject);
                gameObject.SetActive(false);
            }
        }
        else if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Bullet))//�÷��̾� �Ѿ� �ϰ��
        {
            if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Monster))
            {
                //Destroy(myColl.gameObject);
                gameObject.SetActive(false);
            }
        }
        else if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerTag.MobGranid))//���� �Ѿ� �ϰ��
        {
            if (other.gameObject.layer == Delivery.LayerNameEnum(LayerTag.Player))
            {
                gameObject.SetActive(false);
            }
        }

    }

}
