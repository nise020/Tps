using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table_Character : Table_Base
{
    [Serializable]
    public class Info
    {
        public int Id;     
        public byte Type;   // 캐릭터 타입 (enum: Player, Monster, etc.)
        public int Skill;    // → SkillTable의 Id
        public int State;   // → StatTable의 Id (기본 능력치 정보 등)
        public string Prefabs;  // → 리소스 로드용 프리팹 경로
        public string Img; // → UI에 사용할 이미지 경로fabs;
        public int Name;     // → StringTable의 Id;
        public int Dec;  // → StringTable의 설명 텍스트 Id
    }

    public Dictionary<int, Info> CharacterDictionary = new Dictionary<int, Info>();

    public Info Get(int _Id)
    {
        if (CharacterDictionary.ContainsKey(_Id))
            return CharacterDictionary[_Id];
        return null;
    }
    public void Init_Binary(string _Name)
    {
        Load_Binary<Dictionary<int, Info>>(_Name, ref CharacterDictionary);
    }

    public void Save_Binary(string _Name)
    {
        Save_Binary(_Name, CharacterDictionary);
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
            CharacterDictionary.Add(info.Id, info);
        }
    }
    protected bool Read(CSVReader _Reader, Info _info, int _Row, int _Col)
    {
        if (_Reader.reset_row(_Row, _Col) == false) return false;
        _Reader.get(_Row, ref _info.Id);
        _Reader.get(_Row, ref _info.Type);
        _Reader.get(_Row, ref _info.Skill);
        _Reader.get(_Row, ref _info.State);
        _Reader.get(_Row, ref _info.Prefabs);
        _Reader.get(_Row, ref _info.Img);
        _Reader.get(_Row, ref _info.Name);
        _Reader.get(_Row, ref _info.Dec);


        return true;
    }
}
