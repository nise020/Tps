using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Title : Actor
{
    [Header("TitleScene")]
    [SerializeField] GameObject TitleObj;
    [SerializeField] Button MembershipBut;
    [SerializeField] Button StartBut;
    string PasswordKey;//������ ��й�ȣ
    IEnumerator Laoding;
    // Start is called before the first frame update
    //void Start()
    //{
    //    LoadToJason("Json/passkey");
    //}
    void LoadToJason(string DataName)//���� ������
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
        shared.SceneMgr.savePasskey(out PasswordKey);
        //PasswordKey = ActKey;

        if (PasswordKey.Length < 0 || PasswordKey== "" || PasswordKey==null) { return; }
        shared.SceneMgr.GetPlayerPrefsStringKey(PasswordKey);
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
        Debug.Log("���� ������ Ȯ�� �մϴ�");

        yield return new WaitForSeconds(3);

        Debug.Log($"{PasswordKey}���� ���� ������ Ȯ�εǾ����ϴ�.\n����� �κ�� �̵��˴ϴ�");

        shared.SceneMgr.chageScene(eScene.Lobby);
        yield return new WaitForSeconds(3);
    }
}
