using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public partial class State
{
    //player
    //gun
    //moneter
    public void init(ObjectType _obj) 
    {
        playerState();
        Debug.Log($"charactor={_obj}\n" +
            $",monster={monster}\n" +
            $",MaxHP={MaxHP}\n" +
            $",Attack={Attack}\n" +
            $",Defense={Defense}\n" +
            $",Movespeed{Speed}");
    }
    ObjectType objType;
    MonsterType monster = MonsterType.Defolt;
    GunType gun = global::GunType.None;
    public float hP;//�������� ü��
    public float cheHP;//üũ�� ü��

    public float MaxHP;//�ִ�ü��
    public int Attack;//���ݷ�
    public int Defense;//����
    public float Speed;//�̵��ӵ�
    public void InitStateType(ObjectType _eNum)
    {
        objType = _eNum;
    }
    public void monsterType(MonsterType _eNum)
    {
        monster = _eNum;
    }
    public void gunType(GunType _eNum)
    {
        gun = _eNum;
    }




    private void playerState() 
    {
        MaxHP = 100;
        Attack = 30;
        Defense = 30;
        Speed = 10;
    }
    public void MonsterState(GunType _eNum)
    {
        switch (_eNum)
        {
            case GunType.AR:
                Attack = 30;
                Defense = 30;
                break;
            case GunType.SG:
                Attack = 30;
                Defense = 30;
                break;
            case GunType.SR:
                Attack = 30;
                Speed = 10;
                break;
        }
        Debug.Log($"\n" +
            $",monster={_eNum}\n" +
            $",Attack={Attack}\n" +
            $",Movespeed{Speed}");
    }
    public void MonsterState(MonsterType _monster)
    {
        switch (_monster)
        {
            case MonsterType.Spider:
                MaxHP = 30;
                Attack = 30;
                Defense = 30;
                Speed = 3;
                break;
            case MonsterType.Dron:
                MaxHP = 20;
                Attack = 30;
                Defense = 30;
                Speed = 5;
                break;
            case MonsterType.Sphere:
                MaxHP = 50;
                Attack = 30;
                Defense = 30;
                Speed = 10;
                break;
        }
        Debug.Log($"\n" +
            $",monster={_monster}\n" +
            $",MaxHP={MaxHP}\n" +
            $",Attack={Attack}\n" +
            $",Defense={Defense}\n" +
            $",Movespeed{Speed}");
    }
    public void Hit()
    {

    }
    public void NowHp()//��ŸƮ���� �ѹ��� ���� 
    {
        hP = MaxHP;
        cheHP = hP;
    }
    public void CheckHp()//cheHP�� �켱������ �Ҹ�
    {
        if (hP == cheHP) { return; }
        //�ڷ�ƾ ���� ���� ����
        if (hP != cheHP && hP >= 0)
        {
            hP = cheHP;
        }
    }
}
