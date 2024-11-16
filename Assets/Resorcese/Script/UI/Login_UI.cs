using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Login : Actor
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
        StartCoroutine(CreatPassword());

    }
    IEnumerator CreatPassword()//���� ����
    {
        LoginBut.interactable = false;
        Debug.Log("5�� �� �κ� ȭ������ �̵��մϴ�");
        IDKey = IdText.text;
        UserPassKey = PasswordText.text;
        shared.SceneMgr.SetPlayerPrefsStringKey(IDKey, UserPassKey);
        shared.SceneMgr.PassKey = UserPassKey;
        //ActKey = UserPassKey;

        yield return new WaitForSeconds(5);

        //IDSave(IDKey, UserPassKey);
        Debug.Log($"UserPassKey={UserPassKey},IDKey={IDKey}");

        LoginBut.interactable = true;
        shared.SceneMgr.chageScene(eScene.Lobby);


    }
    
}
