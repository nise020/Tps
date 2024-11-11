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
    string PassKey;//������ ��й�ȣ
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
        Debug.Log("���� ������ Ȯ�� �մϴ�");

        yield return new WaitForSeconds(3);

        Debug.Log($"{PassKey}���� ���� ������ Ȯ�εǾ����ϴ�.\n����� �κ�� �̵��˴ϴ�");

        shared.SceneMgr.chageScene(eScene.Lobby);
        yield return new WaitForSeconds(3);
    }
}
