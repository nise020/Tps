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
    public void init(CharactorType charactor) 
    {
        playerState();
        Debug.Log($"charactor={charactor}\n" +
            $",monster={monster}\n" +
            $",MaxHP={MaxHP}\n" +
            $",Attack={Attack}\n" +
            $",Defense={Defense}\n" +
            $",Movespeed{Movespeed}");
    }
    CharactorType charactor;
    MonsterType monster = MonsterType.Defolt;

    public float hP;//보여지는 체력
    public float cheHP;//체크할 체력

    public float MaxHP;//최대체력
    public int Attack;//공격력
    public int Defense;//방어력
    public float Movespeed;//이동속도
    public void charactorType(CharactorType _eNum)
    {
        charactor = _eNum;
    }
    public void monsterType(MonsterType _eNum)
    {
        monster = _eNum;
    }

    private void playerState() 
    {
        MaxHP = 100;
        Attack = 30;
        Defense = 30;
        Movespeed = 10;
    }
    public void monsterState(MonsterType _monster)
    {
        switch (_monster)
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
        Debug.Log($"\n" +
            $",monster={_monster}\n" +
            $",MaxHP={MaxHP}\n" +
            $",Attack={Attack}\n" +
            $",Defense={Defense}\n" +
            $",Movespeed{Movespeed}");
    }
    public void Hit()
    {

    }
    public void NowHp()//스타트에서 한번만 실행 
    {
        hP = MaxHP;
        cheHP = hP;
    }
    public void CheckHp()//cheHP가 우선적으로 소모
    {
        if (hP == cheHP) { return; }
        //코루틴 으로 수정 예정
        if (hP != cheHP && hP >= 0)
        {
            hP = cheHP;
        }
    }
}
