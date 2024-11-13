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
    string PasswordKey;//������ ��й�ȣ
    IEnumerator Laoding;
    // Start is called before the first frame update
    void Start()
    {
        //LoadToJason("Json/passkey");
    }
    void LoadToJason(string DataName)//���� ������
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
        Debug.Log("���� ������ Ȯ�� �մϴ�");

        yield return new WaitForSeconds(3);

        Debug.Log($"{PasswordKey}���� ���� ������ Ȯ�εǾ����ϴ�.\n����� �κ�� �̵��˴ϴ�");

        shared.SceneMgr.chageScene(eScene.Lobby);
        yield return new WaitForSeconds(3);
    }
}
