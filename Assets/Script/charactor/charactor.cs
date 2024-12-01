using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Charactor : Actor
{
    [Tooltip("���Ͱ� ������ ����� �Ѿ�,��ô���� �޾Ƽ� ���")] 
    public Transform chaTargetTrs { get; protected set; }
    public Vector3 chaTargetPos { get; protected set; }//�ӽ� �÷��̾��
    public Collider chaTargetColl { get; protected set; }
    //protected abstract void nomalAttack();//���� ����Ŭ����
    //�ڽ��� ������ ������ �ϴ� ���

    
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
    /// �Ϲ� ����
    /// </summary>
    //protected virtual void nomalAttack() { }
    //�ڽĵ��� ��� �Ҽ��� ���Ҽ��� �ִ� ���
    
}
