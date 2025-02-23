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
    public void init() 
    {
        stateCheck();
    }
    CharactorType charactor;
    MonsterType monster = MonsterType.Defolt;

    public float hP;//�������� ü��
    public float cheHP;//üũ�� ü��

    public float MaxHP;//�ִ�ü��
    public int Attack;//���ݷ�
    public int Defense;//����
    public float Movespeed;//�̵��ӵ�
    public void charactorType(CharactorType _eNum)
    {
        charactor = _eNum;
    }
    public void monsterType(MonsterType _eNum)
    {
        monster = _eNum;
    }
    public void stateCheck() 
    {
        switch (charactor) 
        {
            case CharactorType.None:
                break;
            case CharactorType.Player:
                playerState();
                break;
            case CharactorType.Monster:
                monsterState();
                break;
        }
    }

    private void playerState() 
    {
        MaxHP = 100;
        Attack = 30;
        Defense = 30;
        Movespeed = 10;
    }
    private void monsterState()
    {
        switch (monster)
        {
            case MonsterType.Spider:
                MaxHP = 30;
                Attack = 30;
                Defense = 30;
                Movespeed = 3;
                break;
            case MonsterType.Dron:
                MaxHP = 20;
                Attack = 30;
                Defense = 30;
                Movespeed = 5;
                break;
            case MonsterType.Sphere:
                MaxHP = 50;
                Attack = 30;
                Defense = 30;
                Movespeed = 10;
                break;
        }
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
