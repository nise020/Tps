using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public partial class PhotonMgr : MonoBehaviourPunCallbacks
{
    [PunRPC]//Photon server ����ȭ
    void OnSkill(bool _check , int _skill) 
    {

    }


    public void SendSkill() 
    {
        PV.RPC("OnSkill", RpcTarget.All, true, 1);
        //true, 1 ���ڰ� Ŭ������ �־ ����
        //RpcTarget.All ���� ���� ����
        //.All�� �ƴϸ� ���� ���� �ʴ´�
        //p2p ������� ���� ���������� �̷��� ����� ����
        //�Ϲ����� ���ӿ����� OnSkill�� Ÿ �������� ó���ϰ�
        //SendSkill()�� �������� ó����

        //���������� ����� ���ú긦 ���� ó����
    }






}
