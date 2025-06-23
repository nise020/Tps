using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.UI.CanvasScaler;

public abstract partial class Character : Actor
{
    //델리게이트
    //지연이 걸리는 부분에 사용

    //Action: 패치관련
    //Funtion
    //Task: ADK,동기화,싱크(async)
    //Const


    //대리자
    //system.Func<object,bool> UpteAction = null;
    //UpteAction = 함수연결
    //system.Action<bool> FinishAction = null;
    //FinishAction = 함수연결
    //delegaet void CallBack();
    //CallBack callback = null;
    //callback = 함수연결
    //async -> await // 비동기
    //변수 호출시 연결된 함시 실행

    

    //캐릭터
    //스텟 사용
    //clone 오브젝트 적극 사용 
    //protected int ID;//자신의 ID

    


    protected virtual void Start()
    {
        FindBodyObject();
        InfoLoad();

    }

    public Transform BodyObjectLoad() 
    {
        if (charactorModelTrs == null)
        {
            FindBodyObject();
            return charactorModelTrs;
        }
        else
        {
            return charactorModelTrs;
        }

    }
    public virtual bool DamageEventCheck() {return false;}
    public virtual void DamageEventUpdate(DamageEvent _event) {}



    //protected void footRayCheck() //중력구현
    //{
    //    //init
    //    //update
    //    //Renderring
    //}

    protected void FindRootBodyObject()
    {
        Transform[] body = GetComponentsInChildren<Transform>();
        foreach (Transform rootObj in body)
        {
            int layer = LayerMask.NameToLayer(LayerName.RootBody.ToString());
            if (layer == rootObj.gameObject.layer)
            {
                RootTransform = rootObj.transform;
            }
        }
    }

    //protected void FindMeshBodyObject()
    //{
    //    MeshRenderer skin = GetComponentInChildren<MeshRenderer>();
    //    charactorModelTrs = skin.transform.parent;
    //    Debug.Log($"{gameObject}\ncharactorModelTrs = {charactorModelTrs}");
    //}
    protected virtual void search() {}
    protected virtual void moveAnimation(MonsterWalkState _state) {}
    protected virtual void attackAnimation(MonsterAttackState _state) {}
    protected virtual void move(PlayerModeState _value,Vector3 _pos) 
    {

    }
    protected virtual void move(PlayerModeState _value, Player _player)
    {

    }
    protected virtual void attack(PlayerModeState _state, PlayerType _job) 
    {

    }
    protected virtual void skillAttack_common1(PlayerType _type) 
    {

    }
    protected virtual void skillAttack_common2(PlayerType _type)
    {

    }
    public State StateLoad()
    {
        State state = STATE;
        return state;
    }

}
