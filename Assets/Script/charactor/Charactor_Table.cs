using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Charactor : Actor
{
    [Header("Table/State")]
    protected State STATE = new State();
    protected int hP;//���� ü��
    protected int cheHP;//�������� ü��
    protected int maxHP;//�ִ�ü��

    [Header("Table/Character")]
    protected int id;
    protected byte type;
    protected int skill1;
    protected int skill2;
    protected int ai;
    protected int state;
    protected string prefabs;
    protected string img;
    protected new int name;
    protected int dec;//����

    protected float atkValue;//���ݷ�
    protected float defVAlue;//����
    protected float speedValue;//�̵��ӵ�
    protected float CritRateValue;
    protected float CritDamageValue;//�̵��ӵ�

    protected void InfoLoad()
    {
        Shared.InutTableMgr();
        var info = Shared.TableManager.Character.Get(id);
        if (info == null)
        {
            Debug.LogError($"{gameObject}.info = null");
        }
        else
        {
            Init(info);
            STATE.init(this, state);
            stateInIt();
        }
    }

    protected void Init(Table_Character.Info _info)
    {
        id = _info.Id;
        type = _info.Type;//Ÿ�� ���̺� ���� �ʿ�
        skill1 = _info.Skill1;
        skill2 = _info.Skill2;//skill ���̺� ���� �ʿ�
        ai = _info.Ai;//ai ���̺� ���� �ʿ�
        state = _info.State;
        img = _info.Img;
        prefabs = _info.Prefabs;
        //name = _info.Name;
        dec = _info.Dec;//����
        Debug.Log($"{gameObject}={_info.State}\\");
    }

    protected void stateInIt()
    {
        hP = (int)STATE.StateValueLoad(StatusType.MaxHP);
        atkValue = STATE.StateValueLoad(StatusType.Power);
        speedValue = STATE.StateValueLoad(StatusType.Speed);
        defVAlue = STATE.StateValueLoad(StatusType.Defens);
        CritRateValue = STATE.StateValueLoad(StatusType.CritRate);
        CritDamageValue = STATE.StateValueLoad(StatusType.CritDamage);

        cheHP = hP;
        maxHP = hP;

    }
}
