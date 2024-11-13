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
    void Start()
    {
        //LoadToJason("Json/passkey");
    }
    void LoadToJason(string DataName)//아직 제작중
    {

        TextAsset PassKey = null;
        PassKey = Resources.Load<TextAsset>(DataName);
        UserData Data = JsonUtility.FromJson<UserData>(DataName);
        Debug.Log($"{Data.UserId}");
        PasswordKey = Data.UserId;
    }
    public void BackUpId() 
    {
        shared.SceneMgr.savePasskey(out PasswordKey);
        shared.SceneMgr.GetPlayerPrefsStringKey(PasswordKey);
        if (PasswordKey == null) { return; }
        Laoding = Passwordcheck();
        StartCoroutine(Laoding);
        Debug.Log($"PassKey={PasswordKey}");
    }
    public void LoginView()
    {
        //StartBut.interactable = true;
        shared.SceneMgr.chageScene(eScene.Login);
    }
    IEnumerator Passwordcheck()
    {
        Debug.Log("계정 정보를 확인 합니다");

        yield return new WaitForSeconds(3);

        Debug.Log($"{PasswordKey}님의 계정 정보를 확인되었습니다.\n잠시후 로비로 이동됩니다");

        shared.SceneMgr.chageScene(eScene.Lobby);
        yield return new WaitForSeconds(3);
    }
}
