using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class SceneManager : MonoBehaviour
{ 
    Scene Scene = Scene.Title;

    int IntKey;
    public string PassKey;
    //UserData userData;
    bool check;
    IEnumerator Laoding;
    private void Awake()
    {
        if (Shared.SceneManager == null)
        {
            Shared.SceneManager = this;
            //SceneMgr �̱���
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
        //chageScene(eScene.Title);
    }
    private void Start()
    {
        //Shared.InutTableMgr();
        //Table_Charactor.Info info = Shared.TableMgr.Character.Get(1);

    }
    public void savePasskey(out string _value) 
    {
        _value = PassKey;
    }

    private void gameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif
    }

    private void gameStart()
    {
        Shared.FaidInOut.ActiveFade(true, () =>
        {
            Shared.SceneManager.chageScene((Scene.Battle));//�ӽ�
            Shared.FaidInOut.ActiveFade(false, null);
        });
    }

}
