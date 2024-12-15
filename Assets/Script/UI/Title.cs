using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Title : MonoBehaviour
{
    [Header("TitleScene")]
    [SerializeField] GameObject TitleObj;
    [SerializeField] Button MembershipBut;
    [SerializeField] Button StartBut;
    string PasswordKey;//유저의 비밀번호
    IEnumerator Laoding;
    // Start is called before the first frame update

    public void OnbtnTitle() //예시
    {
        Shared.SceneMgr.chageScene(eScene.Lobby);
    }
    //void Start()
    //{
    //    //LoadToJason("Json/passkey");
    //}
    void LoadToJason(string DataName)//아직 제작중
    {

        TextAsset PassKey = Resources.Load<TextAsset>(DataName);
        if (PassKey==null)
        {
            return;
        }
        SaveData Data = JsonUtility.FromJson<SaveData>(PassKey.text);
        //Debug.Log($"{Data.UserId}");
        PasswordKey = Data.IdData;
    }
    public void Initialize(string target)
    {
        //PasswordKey = ActKey;
    }
    public void BackUpId() 
    {
        Shared.SceneMgr.savePasskey(out PasswordKey);
        //PasswordKey = ActKey;

        if (PasswordKey.Length < 0 || PasswordKey== "" || PasswordKey==null) { return; }
        Shared.SceneMgr.GetPlayerPrefsStringKey(PasswordKey);
        //Laoding = Passwordcheck();
        StartCoroutine(Passwordcheck());
        Debug.Log($"PassKey={PasswordKey}");
    }
    public void LoginView()
    {
        //StartBut.interactable = true;
        Shared.SceneMgr.chageScene(eScene.Login);
    }
    IEnumerator Passwordcheck()
    {
        Debug.Log("계정 정보를 확인 합니다");

        yield return new WaitForSeconds(1);

        Debug.Log($"{PasswordKey}님의 계정 정보를 확인되었습니다.");
        yield return new WaitForSeconds(2);

        Shared.SceneMgr.chageScene(eScene.Loading);
    }
}
