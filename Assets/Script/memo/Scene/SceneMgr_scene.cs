using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class SceneMgr : MonoBehaviour
{
    
    public void chageSecen(eScene _e, bool _Loading = false)
    {
        if (Scene == _e)
            return;

        Scene = _e;

        switch (_e)
        {
            case eScene.Title:
                SceneManager.LoadScene("Title");
                break;
            case eScene.Login:
                SceneManager.LoadScene("Login");
                break;
            case eScene.Lobby:
                SceneManager.LoadScene("Lobby");
                break;
            case eScene.Battle:
                SceneManager.LoadScene("Battle");
                break;
        }
    }


}
