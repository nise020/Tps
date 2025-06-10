using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using AnimationOrTween;

public class PhotonManager : MonoBehaviourPunCallbacks    
{
    // ㅠㅗ톤 서버버에 제종 받는다

    public PhotonView PhotonView;
    private void Awake()
    {
        PhotonNetwork.GameVersion = "1.0.0";// 방
        PhotonNetwork.SendRate = 20;//패킷 속도
        PhotonNetwork.SerializationRate = 10;//암호화 속도

        PhotonNetwork.ConnectUsingSettings();




    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
    }
    //public override void OnDisconnecte
    //{
    //    //PhotonNetwork.

    //}
    //public void onLo
}
