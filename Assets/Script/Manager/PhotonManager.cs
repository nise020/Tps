using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using AnimationOrTween;

public class PhotonManager : MonoBehaviourPunCallbacks    
{
    // �Ф��� �������� ���� �޴´�

    public PhotonView PhotonView;
    private void Awake()
    {
        PhotonNetwork.GameVersion = "1.0.0";// ��
        PhotonNetwork.SendRate = 20;//��Ŷ �ӵ�
        PhotonNetwork.SerializationRate = 10;//��ȣȭ �ӵ�

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
