using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Lobby : MonoBehaviour
{
    [Header("LobbyScene")]
    [SerializeField] GameObject LobbyObj;
    [SerializeField] Button TitleLoadBut;
    // Start is called before the first frame update
    void Start()
    {
        TitleLoadBut.onClick.AddListener(()=>shared.SceneMgr.chageScene(eScene.Title));//юс╫ц
    }


}
