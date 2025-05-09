using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Factory
{
    //Util
    //����
    public static Charactor CreateCharactor(int _value, ObjectType _type) 
    {
        Charactor charactor = null;//���̺�� ��ü��

        Shared.InutTableMgr();
        var info = Shared.TableManager.Character.Get(_value);
        if (info != null)
        {
            //charactor.Init(info);
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

    public static Item CreateItem(int _value,ItemType _type)
    {
        Item item = null;//���̺�� ��ü��
        //����

        Shared.InutTableMgr();
        var iteminfo = Shared.TableManager.Item.Get(_value);
        if (iteminfo != null)
        {
            item.Init(iteminfo, _type);
        }
        else
        {
            Debug.LogWarning($"ObjectType {_type}�� �ش��ϴ� ���̺� ������ �����ϴ�.");
        }

        switch ((ItemTableType)iteminfo.Type)
        {
            case ItemTableType.None:
                //var weaponInfo = Shared.TableManager.Weapon.Get(_value);
                // ���� ����, Init(itemInfo, weaponInfo)
                break;
            default:
                Debug.LogWarning($"�� �� ���� ������ Ÿ��: {iteminfo.Type}");
                break;
        }

        switch (_type) //�̴�� ����ϸ� �������̺� ��� �� ����
        {
            case ItemType.Weapon:
                item = new Weapon();//�� �κ��� �ٲ�� �ȴ�
                break;
        }

        //Table_Charactor.Info info = Shared.TableManager.Character.Get(1);
        //Shared.InutTableMgr();


        return item;


        //�������� �����ϴ� �뵵�ε� �����ϴ�
        //ex.���Ͱ� �ش� �������� ���� �ϰ� �ִٰ� ������ ������
    }
}
