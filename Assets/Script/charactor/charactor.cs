using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract partial class Charactor : Actor
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

    protected int id;
    protected byte type;
    protected int skill;
    protected int state;
    protected string prefabs;
    protected string img;
    //protected int name;
    protected int dec;//����

    protected virtual void Start()
    {
        FindBodyObjectType(RenderType);
        FindWeaponObject(LayerName.Weapon);
    }
    public void Init(Table_Charactor.Info _info) 
    {
        id = _info.Id;
        type = _info.Type;
        skill = _info.Skill;
        state = _info.State;
        img = _info.Img;
        prefabs = _info.Prefabs;
        //name = _info.Name;
        dec = _info.Dec;//����
    }

    public Transform BodyObjectLoad() 
    {
        return charactorModelTrs;
    }

    protected virtual void OnTriggerEnter(Collider other)//����ȭ �ʿ�
    {
        //Collider myColl = gameObject.GetComponent<Collider>();
        //if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerName.Monster))//������ ���
        //{
        //    if (other.gameObject.layer == Delivery.LayerNameEnum(LayerName.Player))
        //    {
        //        //Attack();
        //    }
        //    else if (other.gameObject.layer == Delivery.LayerNameEnum(LayerName.Bullet))//�ǰ�
        //    {
        //        checkHp(other);
        //    }
        //}
        //else if (myColl.gameObject.layer == Delivery.LayerNameEnum(LayerName.Player))//�÷��̾� �� ���
        //{
        //    if (other.gameObject.layer == Delivery.LayerNameEnum(LayerName.Monster))//�ǰ�
        //    {
        //        checkHp(other);
        //    }
        //}
    }


    protected void footRayCheck() //�߷±���
    {
        //init
        //update
        //Renderring
    }
    //protected GameObject FindSkinBodyTypeObject(BodyType _type)
    //{
    //    //GameObject [] bodyObj = GetComponentsInChildren<GameObject>();
    //    //foreach (GameObject obj in bodyObj) 
    //    //{
    //    //    int Layer = LayerMask.NameToLayer(_type.ToString());
    //    //    if (Layer == obj.layer) 
    //    //    {
    //    //        return obj;
    //    //    }
    //    //}
    //    //Debug.Log($"gameObject = null");
    //    //return null;
    //}
    protected void FindRootBodyObject()
    {
        Transform[] body = GetComponentsInChildren<Transform>();
        foreach (Transform rootObj in body)
        {
            int layer = LayerMask.NameToLayer(LayerName.RootBody.ToString());
            if (layer == rootObj.gameObject.layer)
            {
                RootTrransform = rootObj.transform;
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
    protected virtual void move(CharctorStateEnum _value,Vector3 _pos) 
    {

    }
    protected virtual void move(CharctorStateEnum _value, Player _player)
    {

    }
    protected virtual void attack(CharctorStateEnum _state, CharactorJobEnum _job) 
    {

    }
    protected virtual void commonskillAttack1(CharactorJobEnum _type) 
    {

    }
    protected virtual void commonskillAttack2(CharactorJobEnum _type)
    {

    }
    public Status StateLoad()
    {
        Status state = STATUS;
        return state;
    }

}
