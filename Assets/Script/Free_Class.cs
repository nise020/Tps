using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public static int acount = 3;
    public static string UserKey;
    public static string UserId;

}
public class SaveData
{
    public string IdData;
    public string PasswordData = "0";
    //public List<SaveData> saveDatas;//List ����
    //public List<int> dataCount;//List ����
    //public List<string> dataPassworld;//List ����
    //Add �� �� �ϳ��� ���
}
public class PositionMove
{
    public Vector3 position;
    public float time;
    public PositionMove(Vector3 _pos, float _time)
    {
        position = _pos;
        time = _time;   
    }
}

