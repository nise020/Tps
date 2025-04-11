using Newtonsoft.Json;
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
    //IEnumerator Laoding;
    string IDKey;
    string UserPassKey;

    public void LoginPrecces()
    {
        if ((IdText.text == "" || IdText.text.Length <= 0)&&
            (PasswordText.text == "" || PasswordText.text.Length <= 0)) return;
        StartCoroutine(CreatPassword());

    }
    IEnumerator CreatPassword()//계정 생성
    {
        LoginBut.interactable = false;
        Debug.Log("5초 후 로비 화면으로 이동합니다");
        IDKey = IdText.text;
        UserPassKey = PasswordText.text;
        Shared.SceneManager.SetPlayerPrefsStringKey(IDKey, UserPassKey);
        Shared.SceneManager.PassKey = UserPassKey;
        //Shared.SceneMgr.SaveFile(UserPassKey);
        //ActKey = UserPassKey;

        yield return new WaitForSeconds(5);

        //IDSave(IDKey, UserPassKey);
        Debug.Log($"UserPassKey={UserPassKey},IDKey={IDKey}");

        LoginBut.interactable = true;
        Shared.SceneManager.chageScene(Scene.Loading);


    }
    
}
