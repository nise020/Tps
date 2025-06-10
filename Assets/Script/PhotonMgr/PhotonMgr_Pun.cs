using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public partial class PhotonMgr : MonoBehaviourPunCallbacks
{
    [PunRPC]//Photon server 동기화
    void OnSkill(bool _check , int _skill) 
    {

    }


    public void SendSkill() 
    {
        PV.RPC("OnSkill", RpcTarget.All, true, 1);
        //true, 1 인자값 클래스를 넣어도 가능
        //RpcTarget.All 전부 값을 받음
        //.All이 아니면 나는 받지 않는다
        //p2p 방식으로 포톤 서버에서는 이러한 방식을 쓰고
        //일반적인 게임에서는 OnSkill를 타 계정에서 처리하고
        //SendSkill()를 내꺼에서 처리함

        //서버에서는 샌드와 리시브를 따로 처리함
    }






}
