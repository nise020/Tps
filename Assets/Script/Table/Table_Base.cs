using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Xml.Linq;
//: 해킹 방지용 문자열 섞기

public class Table_Base
{
    string GetTablePath()
    {
#if UNITY_EDITOR
        return Application.dataPath;
#else
        return Application.persistentDataPath + "/Assets";
#endif
    }

    protected void Load_Binary<T>(string _Name, ref T _Obj)
    {
        var b = new BinaryFormatter();

        b.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;

        TextAsset asset = Resources.Load("Tabel_" + _Name) as TextAsset;

        Stream stream = new MemoryStream(asset.bytes);

        _Obj = (T)b.Deserialize(stream);

        stream.Close();
    }
    protected void Save_Binary(string _Name, object _Obj)
    {
        //string path = GetTablePath() + "\\Table\\Resources" + "Table_" + _Name + ".txt";
        string path = GetTablePath() + "\\Table\\Resources\\" + "Table_" + _Name + ".txt";
        var b = new BinaryFormatter();

        Stream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);

        b.Serialize(stream, _Obj);

        stream.Close();
        
    }
    protected CSVReader GetCSVReader(string _Name) 
    {
        string ext = ".csv";

        //string path = "D:\\UnityFile\\Tps\\Document\\";//집
        //string path = "C:\\Documents\\newTps\\Document\\";//학원D:\tps\Document
        //string path = GetTablePath()+ "/Document/";
        //:path = 저장위치를 직접 갖다여야함
        string path = "D:\\tps\\Document\\";//학원D:\tps\Document
        if (new FileStream(path + _Name + ext, FileMode.Open, FileAccess.Read, FileShare.ReadWrite) == null) 
        {
            path = "D:\\UnityFile\\Tps\\Document\\";//집
        }
        FileStream file = new FileStream(path + _Name + ext, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

        StreamReader stream = new StreamReader(file, System.Text.Encoding.UTF8);

        CSVReader reader = new CSVReader();

        reader.parse(stream.ReadToEnd(), false, 1);
        stream.Close();
        return reader;
    }
    //protected void Save_Binary(string _Name, object _Obj)
    //{
    //    string path = GetTablePath() + "/Table/Resources";
    //    Debug.Log($"Saving binary to: {path}");

    //    if (!Directory.Exists(path))
    //    {
    //        Debug.Log("Directory does not exist. Creating...");
    //        Directory.CreateDirectory(path);
    //    }

    //    path += "/Table_" + _Name + ".txt";

    //    try
    //    {
    //        var b = new BinaryFormatter();
    //        using (Stream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write))
    //        {
    //            b.Serialize(stream, _Obj);
    //            Debug.Log("File saved successfully.");
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError($"Failed to save file: {e.Message}");
    //    }
    //}

}
