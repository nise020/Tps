using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Actor : MonoBehaviour
{
    protected Camera cam;
    protected Status STATUS = new Status();
    protected float atkValue;//���ݷ�
    protected float defVAlue;//����
    protected float speedValue;//�̵��ӵ�
    protected ObjectType objType = ObjectType.None;//���� ���͸� ����

    public void TypeInit(ObjectType _objType) 
    {
        objType = _objType;
    }

    //������Ʈ
    //���ҽ� ����� start�� ��� �ؾ� �ϴ���?

}
