using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Actor : MonoBehaviour
{
    protected Camera cam;
    //protected State STATE = new State();
    
    protected ObjectType objType = ObjectType.None;//���� ���͸� ����

    public void TypeInit(ObjectType _objType) 
    {
        objType = _objType;
    }

    //������Ʈ
    //���ҽ� ����� start�� ��� �ؾ� �ϴ���?

}
