using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public partial class SceneMgr: MonoBehaviour
{
    string FilePath = "Test.txt";

    public void SaveFile(string _data) 
    {
        //주로 옵션 관련 같이 중요하지 않은 정보를 저장
        if (File.Exists(Application.dataPath + FilePath))
        {
            //Application.dataPath:에셋폴더 안에 생성
            StreamWriter se = File.CreateText(FilePath);

            se.WriteLine($"{_data}");//한줄씩 작성

            se.Close();//무조건 당아야 한다,메모리 누수 때문에
        }
        else 
        {
            StreamReader sr = File.OpenText(Application.dataPath + FilePath);

            string str1 = sr.ReadLine();

            sr.Close();
        }
        
    }
}
