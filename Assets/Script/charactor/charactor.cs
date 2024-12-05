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
        if (myColl.gameObject.layer == LayerMask.NameToLayer("Monster"))//������ ���
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player") ||
                 other.gameObject.layer == LayerMask.NameToLayer("Cover"))
            {
                Destroy(myColl.gameObject);
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
            {

            }
        }
        else if (myColl.gameObject.layer == LayerMask.NameToLayer("MobBullet"))//�� �Ѿ��� ���
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player") ||
                 other.gameObject.layer == LayerMask.NameToLayer("Cover"))
            {
                Destroy(myColl.gameObject);
            }
        }
        else if (myColl.gameObject.layer == LayerMask.NameToLayer("Player")) //�÷��̾� �� ���
        {
            //Destroy(myColl.gameObject);
        }
        else if (myColl.gameObject.layer == LayerMask.NameToLayer("Bullet")) //�÷��̾� �Ѿ� �ϰ��
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("BackGround")||
                other.gameObject.layer == LayerMask.NameToLayer("Monster"))
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
