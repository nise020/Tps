using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class SceneMgr : MonoBehaviour
{ 
    eScene Scene = eScene.Title;

    int IntKey;
    public string PassKey;
    //UserData userData;
    bool check;
    IEnumerator Laoding;
    private void Awake()
    {
        if (Shared.SceneMgr == null)
        {
            Shared.SceneMgr = this;
            //SceneMgr �̱���
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
        //chageScene(eScene.Title);
    }

    public void savePasskey(out string _value) 
    {
        _value = PassKey;
    }



}