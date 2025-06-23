using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character : Actor
{
    protected void InfoLoad()
    {
        Shared.InutTableMgr();
        var info = Shared.TableManager.Character.Get((int)CharacterTabelData[CharacterTabelType.Id]);
        if (info == null)
        {
            Debug.LogError($"{gameObject}.info = null");
        }
        else
        {
            Init(info);
            STATE.init(this, (int)CharacterTabelData[CharacterTabelType.State]);
            stateInIt();
        }
    }

    protected void Init(Table_Character.Info _info)
    {
        //id = _info.Id;
        CharacterTabelData.Add(CharacterTabelType.Id, _info.Id);
        CharacterTabelData.Add(CharacterTabelType.Type, _info.Type);
        CharacterTabelData.Add(CharacterTabelType.Skill1, _info.Skill1);
        CharacterTabelData.Add(CharacterTabelType.Skill2, _info.Skill2);
        CharacterTabelData.Add(CharacterTabelType.Ai, _info.State);
        CharacterTabelData.Add(CharacterTabelType.Dec, _info.Dec);

        CharacterTabelTextData.Add(CharacterTabelType.Img, _info.Img);
        CharacterTabelTextData.Add(CharacterTabelType.Prefabs, _info.Prefabs);


        //type = _info.Type;//타입 테이블 연결 필요
        //skill1 = _info.Skill1;
        //skill2 = _info.Skill2;//skill 테이블 연결 필요
        //ai = _info.Ai;//ai 테이블 연결 필요
        //state = _info.State;
        //img = _info.Img;
        //prefabs = _info.Prefabs;
        ////name = _info.Name;
        //dec = _info.Dec;//설명
        //Debug.Log($"{gameObject}={_info.State}\\");
    }
  
    protected void stateInIt()//Reset
    {
        float maxHP = (int)STATE.StateValueLoad(StatusType.MaxHP);
        StatusData.Add(StatusType.MaxHP, maxHP);

        float hp = (int)STATE.StateValueLoad(StatusType.MaxHP);
        StatusData.Add(StatusType.HP, hp);

        float atkValue = STATE.StateValueLoad(StatusType.Power);
        StatusData.Add(StatusType.Power, atkValue);

        float speedValue = STATE.StateValueLoad(StatusType.Speed);
        StatusData.Add(StatusType.Speed, speedValue);

        float defVAlue = STATE.StateValueLoad(StatusType.Defens);
        StatusData.Add(StatusType.Defens, defVAlue);

        float CritRateValue = STATE.StateValueLoad(StatusType.CritRate);
        StatusData.Add(StatusType.CritRate, CritRateValue);

        float CritDamageValue = STATE.StateValueLoad(StatusType.CritDamage);
        StatusData.Add(StatusType.CritDamage, CritDamageValue);
    }
}
