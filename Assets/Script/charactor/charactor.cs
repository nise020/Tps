using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Charactor : Actor
{
    [Tooltip("���Ͱ� ������ ����� �Ѿ�,��ô���� �޾Ƽ� ���")] 
    public Transform chaTargetTrs { get; protected set; }
    public Collider chaTargetColl { get; protected set; }
    //protected abstract void nomalAttack();//���� ����Ŭ����
    //�ڽ��� ������ ������ �ϴ� ���
    //private Dictionary<int, Action<Collider>> collisionHandlers;

    
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
            Destroy(myColl.gameObject);
        }
    }


    /// <summary>
    /// �Ϲ� ����
    /// </summary>
    //protected virtual void nomalAttack() { }
    //�ڽĵ��� ��� �Ҽ��� ���Ҽ��� �ִ� ���
    
}
