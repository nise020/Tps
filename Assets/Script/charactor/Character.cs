using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.UI.CanvasScaler;

public abstract partial class Character : Actor
{
    //��������Ʈ
    //������ �ɸ��� �κп� ���

    //Action: ��ġ����
    //Funtion
    //Task: ADK,����ȭ,��ũ(async)
    //Const


    //�븮��
    //system.Func<object,bool> UpteAction = null;
    //UpteAction = �Լ�����
    //system.Action<bool> FinishAction = null;
    //FinishAction = �Լ�����
    //delegaet void CallBack();
    //CallBack callback = null;
    //callback = �Լ�����
    //async -> await // �񵿱�
    //���� ȣ��� ����� �Խ� ����

    

    //ĳ����
    //���� ���
    //clone ������Ʈ ���� ��� 
    //protected int ID;//�ڽ��� ID

    


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



    //protected void footRayCheck() //�߷±���
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
