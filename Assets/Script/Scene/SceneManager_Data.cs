using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public partial class SceneManager : MonoBehaviour
{
    [SerializeField]//제이슨 파일 등록
    public void dataSave() 
    {
       string saveFilePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
    }
    public void dataLoad()
    {

    }
}
