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
        public byte Type;
        public int Skill;
        public int State;
        public string Prefabs;
        public string Img;
        public int Name;
        public int Dec;//Ό³Έν
    }

    public Dictionary<int, Info> Dictionary = new Dictionary<int, Info>();

    public Info Get(int _Id)
    {
        if (Dictionary.ContainsKey(_Id))
            return Dictionary[_Id];
        return null;
    }
    public void Init_Binary(string _Name)
    {
        Load_Binary<Dictionary<int, Info>>(_Name, ref Dictionary);
    }

    public void Save_Binary(string _Name)
    {
        Save_Binary(_Name, Dictionary);
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
            Dictionary.Add(info.Id, info);
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
