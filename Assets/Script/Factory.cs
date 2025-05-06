using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Factory 
{
    public static Charactor CreateCharactor(ObjectType _type) 
    {
        Charactor charactor = null;//테이블로 대체시


        Shared.InutTableMgr();
        var info = Shared.TableManager.Character.Get((int)_type);
        if (info != null)
        {
            charactor.Init(info);
        }
        else
        {
            Debug.LogWarning($"ObjectType {_type}에 해당하는 테이블 정보가 없습니다.");
        }


        switch (_type) //이대로 사용하면 모노비헤이비어를 상속 못 받음
        {
            case ObjectType.Player:
                charactor = new Player();//이 부분이 바뀌게 된다
                break;
            case ObjectType.Monster:
                charactor = new Monster();//이 부분이 바뀌게 된다
                break;
        }

        //Table_Charactor.Info info = Shared.TableManager.Character.Get(1);
        //Shared.InutTableMgr();
        

        return charactor;


        //아이템을 생성하는 용도로도 가능하다
        //ex.몬스터가 해당 아이템을 보유 하고 있다가 죽으면 떨군다
    }
}
