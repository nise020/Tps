using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public partial class SceneMgr: MonoBehaviour
{
    string FilePath = "Test.txt";

    public void SaveFile(string _data) 
    {
        //�ַ� �ɼ� ���� ���� �߿����� ���� ������ ����
        if (File.Exists(Application.dataPath + FilePath))
        {
            //Application.dataPath:�������� �ȿ� ����
            StreamWriter se = File.CreateText(FilePath);

            se.WriteLine($"{_data}");//���پ� �ۼ�

            se.Close();//������ ��ƾ� �Ѵ�,�޸� ���� ������
        }
        else 
        {
            StreamReader sr = File.OpenText(Application.dataPath + FilePath);

            string str1 = sr.ReadLine();

            sr.Close();
        }
        
    }
}
