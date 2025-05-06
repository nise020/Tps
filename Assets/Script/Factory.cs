using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Factory 
{
    public static Charactor CreateCharactor(ObjectType _type) 
    {
        Charactor charactor = null;//���̺�� ��ü��


        Shared.InutTableMgr();
        var info = Shared.TableManager.Character.Get((int)_type);
        if (info != null)
        {
            charactor.Init(info);
        }
        else
        {
            Debug.LogWarning($"ObjectType {_type}�� �ش��ϴ� ���̺� ������ �����ϴ�.");
        }


        switch (_type) //�̴�� ����ϸ� �������̺� ��� �� ����
        {
            case ObjectType.Player:
                charactor = new Player();//�� �κ��� �ٲ�� �ȴ�
                break;
            case ObjectType.Monster:
                charactor = new Monster();//�� �κ��� �ٲ�� �ȴ�
                break;
        }

        //Table_Charactor.Info info = Shared.TableManager.Character.Get(1);
        //Shared.InutTableMgr();
        

        return charactor;


        //�������� �����ϴ� �뵵�ε� �����ϴ�
        //ex.���Ͱ� �ش� �������� ���� �ϰ� �ִٰ� ������ ������
    }
}
