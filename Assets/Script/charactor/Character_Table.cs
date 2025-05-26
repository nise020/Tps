using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character : Actor
{
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
        //Debug.Log($"{gameObject}={_info.State}\\");
    }
  
    protected void stateInIt()
    {
        maxHP = (int)STATE.StateValueLoad(StatusType.MaxHP);
        atkValue = STATE.StateValueLoad(StatusType.Power);
        speedValue = STATE.StateValueLoad(StatusType.Speed);
        defVAlue = STATE.StateValueLoad(StatusType.Defens);
        CritRateValue = STATE.StateValueLoad(StatusType.CritRate);
        CritDamageValue = STATE.StateValueLoad(StatusType.CritDamage);

        hP = maxHP;
        cheHP = maxHP;
        //Debug.Log($"{gameObject},hP = {hP},maxHP ={maxHP}");
    }
}
