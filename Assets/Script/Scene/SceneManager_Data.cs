using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public partial class SceneManager : MonoBehaviour
{
    [SerializeField]//���̽� ���� ���
    public void dataSave() 
    {
       string saveFilePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
    }
    public void dataLoad()
    {

    }
}
