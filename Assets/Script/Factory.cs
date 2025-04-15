using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Factory 
{
    public static Charactor CreateCharactor(ObjectType _type) 
    {
        Charactor charactor = null;//���̺�� ��ü��
        switch (_type) //�̴�� ����ϸ� �������̺� ��� �� ����
        {
            case ObjectType.Player:
                charactor = new Player();//�� �κ��� �ٲ�� �ȴ�
                break;
            case ObjectType.Monster:
                charactor = new Monster();//�� �κ��� �ٲ�� �ȴ�
                break;
        }
        return charactor;
        //�������� �����ϴ� �뵵�ε� �����ϴ�
        //ex.���Ͱ� �ش� �������� ���� �ϰ� �ִٰ� ������ ������
    }
}
