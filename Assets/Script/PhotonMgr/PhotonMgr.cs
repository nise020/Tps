using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;//�Լ��� ����ȭ
using Photon.Realtime;//

public partial class PhotonMgr : MonoBehaviourPunCallbacks//������ ���� �׶� ó��
{
    //��������Ʈ ���
    //Q���
    public PhotonView PV;


    // Start is called before the first frame update
    private void Awake()
    {
        //��Ÿƽ ����
        DontDestroyOnLoad(this);

        PhotonNetwork.GameVersion = "1.0.0";//���� ���� ������ ����
        PhotonNetwork.SendRate = 20;//��� �Ӥ���
        PhotonNetwork.SerializationRate = 10;
        PhotonNetwork.ConnectUsingSettings();//���� �É�


    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
    }

    public override void OnConnectedToMaster()//���� ��
    {
        base.OnConnectedToMaster();

        PhotonNetwork.JoinLobby();//�κ�� ������
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("OnJoinedLobby");
    }

   public void OnLobby() 
   {
        PhotonNetwork.IsMessageQueueRunning = true;//Q���=�������

   }


}
