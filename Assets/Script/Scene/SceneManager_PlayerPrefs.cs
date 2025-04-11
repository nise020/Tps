using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SceneManager : MonoBehaviour
{
    //계정 만들기
    //런쳐파일
    public void SetPlayerPrefslntKey(string _Key, int _Value)
    {
        PlayerPrefs.SetInt(_Key, _Value);
        PlayerPrefs.Save();
    }
    public int GetPlayerPrefslntKey(string _Key)
    {
        return PlayerPrefs.GetInt(_Key);
    }

    public void SetPlayerPrefsfloatKey(string _Key, float _Value)
    {
        PlayerPrefs.SetFloat(_Key, _Value);
        PlayerPrefs.Save();
    }
    public float GetPlayerPrefsfloatKey(string _Key)
    {
        return PlayerPrefs.GetFloat(_Key);
    }

    public void SetPlayerPrefsStringKey(string _Key, string _Value)
    {
        PlayerPrefs.SetString(_Key, _Value);
        PlayerPrefs.Save();
    }
    public string GetPlayerPrefsStringKey(string _Key)
    {
        return PlayerPrefs.GetString($"{_Key}");
    }
}
