using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;//함수를 동기화
using Photon.Realtime;//

public partial class PhotonMgr : MonoBehaviourPunCallbacks//응답이 오면 그때 처리
{
    //딜리게이트 방식
    //Q방식
    public PhotonView PV;


    // Start is called before the first frame update
    private void Awake()
    {
        //스타틱 연결
        DontDestroyOnLoad(this);

        PhotonNetwork.GameVersion = "1.0.0";//같은 버전 끼리만 가능
        PhotonNetwork.SendRate = 20;//통신 속ㄷ도
        PhotonNetwork.SerializationRate = 10;
        PhotonNetwork.ConnectUsingSettings();//연결 시돟


    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
    }

    public override void OnConnectedToMaster()//서버 방
    {
        base.OnConnectedToMaster();

        PhotonNetwork.JoinLobby();//로비로 보낸다
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("OnJoinedLobby");
    }

   public void OnLobby() 
   {
        PhotonNetwork.IsMessageQueueRunning = true;//Q방식=순서대로

   }


}
