using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table_Character_State : Table_Base
{
    [Serializable]
    public class Info
    {
        public int Id;
        public int MaxHP;   // 캐릭터 타입 (enum: Player, Monster, etc.)
        public int Power;    // → SkillTable의 Id
        public int Defense;   // → StatTable의 Id (기본 능력치 정보 등)
        public int Speed;  // → 리소스 로드용 프리팹 경로
        public int CritRate; // → UI에 사용할 이미지 경로fabs;
        public int CritDamage;     // → StringTable의 Id;
    }
 
    public Dictionary<int, Info> StateDictionary = new Dictionary<int, Info>();

    public Info Get(int _Id)
    {
        if (StateDictionary.ContainsKey(_Id))
            return StateDictionary[_Id];
        Debug.LogError($"Id ={_Id}의 해당하는 State의 값이 없습니다");
        return null;
    }
    public void Init_Binary(string _Name)
    {
        Load_Binary<Dictionary<int, Info>>(_Name, ref StateDictionary);
    }

    public void Save_Binary(string _Name)
    {
        Save_Binary(_Name, StateDictionary);
    }

    public void Init_Csv(string _Name, int StartRoe, int _StartCol)
    {
        CSVReader reader = GetCSVReader(_Name);
        if (reader == null) { return; }

        for (int row = StartRoe; row < reader.row; ++row)
        {
            Info info = new Info();

            if (Read(reader, info, row, _StartCol) == false)
                break;
            StateDictionary.Add(info.Id, info);
        }
    }
    protected bool Read(CSVReader _Reader, Info _info, int _Row, int _Col)
    {
        if (_Reader.reset_row(_Row, _Col) == false) return false;
        _Reader.get(_Row, ref _info.Id);
        _Reader.get(_Row, ref _info.MaxHP);
        _Reader.get(_Row, ref _info.Power);
        _Reader.get(_Row, ref _info.Defense);
        _Reader.get(_Row, ref _info.Speed);
        _Reader.get(_Row, ref _info.CritRate);
        _Reader.get(_Row, ref _info.CritDamage);


        return true;
    }
}
