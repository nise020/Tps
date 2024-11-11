using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class SceneMgr : MonoBehaviour
{ 
    eScene Scene = eScene.Title;
    [Header("TitleScene")]
    [SerializeField] GameObject TitleObj;
    [SerializeField] Button MembershipBut;
    [Header("LoginScene")]
    [SerializeField] GameObject LoginObj;
    [SerializeField] Text IdText;
    [SerializeField] Button LoginBut;
    [SerializeField] GameObject InPutObj;
    [Header("LobbyScene")]
    [SerializeField] GameObject LobbyObj;
    [SerializeField] Button TitleLoadBut;
    [Header("Title")]
    [Header("Title")]
    int IntKey;
    public string PassKey;
    //UserData userData;
    bool check;
    IEnumerator Laoding;
    private void Awake()
    {
        if (shared.SceneMgr == null)
        {
            shared.SceneMgr = this;
            //SceneMgr ΩÃ±€≈Ê
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
        //chageScene(eScene.Title);
    }

    public void savePasskey(out string value) 
    {
        value = PassKey;
    }

    public bool InputFildcheck(bool value) 
    {
        if (IdText.text.Length == 0)
        {
            value = false;
        }
        else 
        {
            value = true; 
        }
        return value;
    } 

}
