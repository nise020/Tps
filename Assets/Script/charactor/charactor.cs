using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class charactor : Actor
{
    [Tooltip("���Ͱ� ������ ����� �Ѿ�,��ô���� �޾Ƽ� ���")] 
    public Transform ActTargetTrs { get; protected set; }
    //protected abstract void nomalAttack();//���� ����Ŭ����
    //�ڽ��� ������ ������ �ϴ� ���


    /// <summary>
    /// �Ϲ� ����
    /// </summary>
    protected virtual void nomalAttack() { }
    //�ڽĵ��� ��� �Ҽ��� ���Ҽ��� �ִ� ���
    private void OnTriggerEnter(Collider other)
    {
        other = gameObject.GetComponent<Collider>();
    }
}
