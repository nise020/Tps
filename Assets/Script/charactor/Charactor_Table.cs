using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Charactor : Actor
{
    [Header("Table/State")]
    protected State STATE = new State();
    protected int hP;//실제 체력
    protected int cheHP;//보여지는 체력
    protected int maxHP;//최대체력

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
    protected int dec;//설명

    protected float atkValue;//공격력
    protected float defVAlue;//방어력
    protected float speedValue;//이동속도
    protected float CritRateValue;
    protected float CritDamageValue;//이동속도

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
        type = _info.Type;//타입 테이블 연결 필요
        skill1 = _info.Skill1;
        skill2 = _info.Skill2;//skill 테이블 연결 필요
        ai = _info.Ai;//ai 테이블 연결 필요
        state = _info.State;
        img = _info.Img;
        prefabs = _info.Prefabs;
        //name = _info.Name;
        dec = _info.Dec;//설명
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
