using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character
{



    
    public void PlayerTypeInite(out PlayerType _type)
    {
        _type = playerStateData.PlayerType;
    }
    public void PlayerControllChange(PlayerModeState _type)
    {
        playerStateData.ModeState = _type;
        Debug.Log($"{gameObject}/{playerStateData.ModeState} = {_type}"); 
    }

    public void PlayerControllChack(out PlayerModeState _type)
    {
        _type = playerStateData.ModeState;
    }
    public bool CharactorEnumCheck(PlayerType _player)
    {
        if (_player == playerStateData.PlayerType)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected override void death()
    {
        base.death();
        gameObject.SetActive(false);
    }
    public override int StatusTypeLoad(StatusType _type)
    {
        int value = 0;
        switch (_type)
        {
            case StatusType.HP:
                value = (int)hP;
                break;
            case StatusType.Power:
                value = AttackComboStateCheck();
                break;
            case StatusType.Speed:
                value = (int)speedValue;
                break;
            case StatusType.Defens:
                value = (int)defVAlue;
                break;
            case StatusType.CritDamage:
                value = (int)CritRateValue;
                break;
            case StatusType.CritRate:
                value = (int)CritDamageValue;
                break;
        }
        return value;
    }

    protected int AttackComboStateCheck()
    {
        int stateValue = 0;
        if (playerStateData.AttackState == PlayerAttackState.Attack_On) 
        {
            stateValue = (int)atkValue;
        }
        else if (playerStateData.AttackState == PlayerAttackState.Attack_Combo) 
        {
            float combo = atkValue * 1.5f;
            stateValue = (int)combo;
        }
        return stateValue;
    }
}
