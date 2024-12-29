using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;//함수를 동기화
using Photon.Realtime;//
using TMPro;

public partial class PhotonMgr : MonoBehaviourPunCallbacks//응답이 오면 그때 처리
{
    public void CreatLobbyRoom(string _Room)
    {
        if (_Room == null) return;
        PhotonNetwork.CreateRoom(_Room);
    }

    public void RandomLobbyRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public void JoinLobbyRoom(string _Room)
    {
        if (_Room == null) return;
        PhotonNetwork.JoinRoom(_Room);
    }

    public void LeaveRoom(bool _com) 
    {
        PhotonNetwork.LeaveRoom(_com); 
    }

    public void LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }

    public void SecreatLobbyRoom(string _Room, byte _Secreat, byte _MaxPlayer)
    {
        if (_Room == null) return;

        bool open = _Secreat > 0 ? false : true;

        RoomOptions Options = new RoomOptions()
        {
            IsVisible = open,
            MaxPlayers = _MaxPlayer,
        };

        if (Options == null) return;

        PhotonNetwork.JoinOrCreateRoom(_Room, Options, null);
        
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList) 
    {
        base.OnRoomListUpdate(roomList);
        foreach (RoomInfo room in roomList) 
        {

        }
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
    }
    [PunRPC]
    public void SendRoomEntry()
    {
        PV.RPC("LobbyRoomEntry", RpcTarget.All,true);
        //LobbyRoomEntry 호출,true 전달
        //:true는 예시
    }
    public void LobbyRoomEnter(bool _Entry) 
    {
        
    }
    [PunRPC]
    public void SendRoomReady()
    {
        PV.RPC("LobbyRoomReady", RpcTarget.All);
    }
    [PunRPC]
    public void SendStartInGame()
    {
        PV.RPC("SendStartInGame", RpcTarget.All);
    }
}

