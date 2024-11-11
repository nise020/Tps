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
    string PassKey;//유저의 비밀번호
    IEnumerator Laoding;
    // Start is called before the first frame update
    void Start()
    {
        //StartBut.interactable = false;
    }
    public void BackUpId() 
    {
        shared.SceneMgr.savePasskey(out PassKey);
        shared.SceneMgr.GetPlayerPrefsStringKey(PassKey);
        if (PassKey == null) { return; }
        Laoding = Passwordcheck();
        StartCoroutine(Laoding);
        Debug.Log($"PassKey={PassKey}");
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

        Debug.Log($"{PassKey}님의 계정 정보를 확인되었습니다.\n잠시후 로비로 이동됩니다");

        shared.SceneMgr.chageScene(eScene.Lobby);
        yield return new WaitForSeconds(3);
    }
}
