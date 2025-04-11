using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class SceneManager : MonoBehaviour
{
    public void chageScene(Scene _e, bool _Loading = false)
    {
        if (Scene == _e)
            return;

        Scene = _e;

        switch (_e)
        {
            case Scene.Title:
                //SceneManager.LoadScene("Title");
                UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName.Title.ToString());
                break;
            case Scene.Login:
                //SceneManager.LoadScene("Login");
                UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName.Login.ToString());
                break;
            case Scene.Lobby:
                //SceneManager.LoadScene("Lobby");
                UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName.Lobby.ToString());
                break;
            case Scene.Battle:
                //SceneManager.LoadScene("Battle");
                UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName.Battle.ToString());
                break;
        }
    }


}
