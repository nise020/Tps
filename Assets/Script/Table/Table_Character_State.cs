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
        public int MaxHP;   // ĳ���� Ÿ�� (enum: Player, Monster, etc.)
        public int Power;    // �� SkillTable�� Id
        public int Defense;   // �� StatTable�� Id (�⺻ �ɷ�ġ ���� ��)
        public int Speed;  // �� ���ҽ� �ε�� ������ ���
        public int CritRate; // �� UI�� ����� �̹��� ���fabs;
        public int CritDamage;     // �� StringTable�� Id;
    }
 
    public Dictionary<int, Info> StateDictionary = new Dictionary<int, Info>();

    public Info Get(int _Id)
    {
        if (StateDictionary.ContainsKey(_Id))
            return StateDictionary[_Id];
        Debug.LogError($"Id ={_Id}�� �ش��ϴ� State�� ���� �����ϴ�");
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
