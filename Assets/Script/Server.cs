using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
    string Http = "http://58.78.211.182:3000/";//서버
    //3000<--통로
    //12/22 한정으로 사용 가능
    //https<--보안됨
    string ConnectUrl = "process/dbconnect";
    string DisConnectUrl = "process/dbdisconnect";
    string UserSelectUrl = "process/userselect";//유저 데이터
    public void OnBtnConnect() 
    {
        StartCoroutine(DBPost(Http+ConnectUrl,"dev"));
    }

    IEnumerator DBPost(string Url, string Num)
    {
        WWWForm form = new WWWForm();
        form.AddField("num", Num);

        UnityWebRequest www = UnityWebRequest.Post(Url,form);

        yield return www.SendWebRequest();//데이터를 받으면 아래 처리

        Debug.Log(www.downloadHandler.text);
        JSONNode node = JSONNode.Parse(www.downloadHandler.text);
        for (int i = 0; i< node.Count; i++) 
        {

        }
    }
}


