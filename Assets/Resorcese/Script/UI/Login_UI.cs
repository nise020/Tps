//using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Login : MonoBehaviour
{
    [Header("LoginScene")]
    [SerializeField] GameObject LoginObj;
    [SerializeField] Text IdText;
    [SerializeField] Text PasswordText;
    [SerializeField] Button LoginBut;
    IEnumerator Laoding;
    string IDKey;
    string UserPassKey;
    // Start is called before the first frame update

    public void LoginPrecces()
    {
        if ((IdText.text == "" || IdText.text.Length <= 0)&&
            (PasswordText.text == "" || PasswordText.text.Length <= 0)) return;
        Laoding = CreatPassword();
        StartCoroutine(Laoding);

    }
    IEnumerator CreatPassword()//���� ����
    {
        LoginBut.interactable = false;
        Debug.Log("5�� �� �κ� ȭ������ �̵��մϴ�");
        IDKey = IdText.text;
        UserPassKey = PasswordText.text;
        shared.SceneMgr.SetPlayerPrefsStringKey(IDKey, UserPassKey);

        yield return new WaitForSeconds(5);

        shared.SceneMgr.PassKey = UserPassKey;
        //IDSave(IDKey, UserPassKey);
        Debug.Log($"UserPassKey={UserPassKey},IDKey={IDKey}");

        LoginBut.interactable = true;
        shared.SceneMgr.chageScene(eScene.Lobby);


    }
    //public void IDSave(string _key,string _Pass) 
    //{
    //    //List<UserData> saveData = JsonConvert.DeserializeObject<List<UserData>>(PlayerPrefs.GetString(SaveData.PasswordData));
    //    int count = saveData.Count;
    //    for (int iNum = 0; iNum < count; iNum++)
    //    {
    //        if (saveData[iNum] != null)
    //        {
    //            return;
    //        }
    //        saveData[iNum].UserKey = _key;
    //        saveData[iNum].UserId = _Pass;
    //    }
    //}
}
